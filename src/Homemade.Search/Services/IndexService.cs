using Homemade.Database;
using Homemade.Database.Entities;
using Homemade.Search.Mapping;

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
            .AsSplitQuery()
            .AsNoTracking()
            .AsAsyncEnumerable();

        await foreach (var recipe in recipes.WithCancellation(cancellationToken))
            writer.AddDocument(RecipeDocumentMapper.Map(recipe));

        logger.LogInformation("Index rebuild complete");
        writer.Commit();
    }
}