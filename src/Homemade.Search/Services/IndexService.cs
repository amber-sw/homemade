using Homemade.Database;
using Homemade.Database.Entities;

using Lucene.Net.Documents;
using Lucene.Net.Index;

using Microsoft.EntityFrameworkCore;

namespace Homemade.Search.Services;

/// <summary>
/// Handles (re-)building the search index.
/// </summary>
public sealed class IndexService(
    ILogger<IndexService> logger,
    [FromKeyedServices(nameof(Recipe))] IndexWriter writer,
    HomemadeContext context
)
{
    /// <summary>
    /// Rebuilds the entire search index from the database.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    public async Task BuildIndex(CancellationToken cancellationToken)
    {
        logger.LogDebug("Clearing index before rebuild");
        writer.DeleteAll();

        var recipes = context.Recipes
            .Include(r => r.Ingredients)
            .ThenInclude(ri => ri.Ingredient)
            .Include(r => r.Tags)
            .Include(r => r.Instructions)
            .AsNoTracking()
            .AsAsyncEnumerable();

        logger.LogInformation("Rebuilding index");
        await foreach (var recipe in recipes.WithCancellation(cancellationToken))
        {
            var document = new Document
            {
                new StringField("$Type", nameof(Recipe), Field.Store.NO),
                new Int64Field(nameof(Recipe.Id), recipe.Id, Field.Store.YES),
                new TextField(nameof(Recipe.Name), recipe.Name, Field.Store.YES),
                new StringField(nameof(Recipe.Icon), recipe.Icon, Field.Store.YES),
                new TextField(nameof(Recipe.Description), recipe.Description, Field.Store.YES),
                new Int32Field(nameof(Recipe.PreparationTime), (int)recipe.PreparationTime.TotalMinutes,
                    Field.Store.YES),
                new Int32Field(nameof(Recipe.CookingTime), (int)recipe.CookingTime.TotalMinutes, Field.Store.YES),
                new Int32Field(nameof(Recipe.Servings), recipe.Servings, Field.Store.YES),
                new StringField(nameof(Recipe.Difficulty), recipe.Difficulty.ToString(), Field.Store.YES),
                new TextField(nameof(Recipe.Notes), recipe.Notes ?? string.Empty, Field.Store.NO),
            };

            foreach (var tag in recipe.Tags)
                document.Add(new TextField($"{nameof(Recipe)}.{nameof(Recipe.Tags)}", tag.Name, Field.Store.YES));

            foreach (var recipeIngredient in recipe.Ingredients)
                document.Add(new TextField($"{nameof(Recipe)}.{nameof(Recipe.Ingredients)}",
                    recipeIngredient.Ingredient.Name, Field.Store.YES));

            foreach (var instruction in recipe.Instructions)
                document.Add(new TextField($"{nameof(Recipe)}.{nameof(Recipe.Instructions)}", instruction.Text,
                    Field.Store.NO));

            writer.AddDocument(document);
        }

        logger.LogInformation("Index rebuild complete");
        writer.Commit();
    }
}