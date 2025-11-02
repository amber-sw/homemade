using Homemade.Database;
using Homemade.Database.Entities;
using Homemade.Search.Mapping;

using Lucene.Net.Facet.Taxonomy;
using Lucene.Net.Index;

using Lucent.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Homemade.Search.Services;

/// <summary>
/// Handles (re-)building the search index.
/// </summary>
public sealed class IndexService(
    ILogger<IndexService> logger,
    IOptionsSnapshot<IndexConfiguration> configuration,
    [FromKeyedServices(nameof(Recipe))] IndexWriter writer,
    [FromKeyedServices(nameof(Recipe))] ITaxonomyWriter taxonomyWriter,
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
        {
            var document = RecipeDocumentMapper.Map(recipe);
            document = configuration.Get(nameof(Recipe)).FacetsConfig?.Build(taxonomyWriter, document) ?? document;

            writer.AddDocument(document);
        }

        logger.LogInformation("Index rebuild complete");
        writer.Commit();
    }
}