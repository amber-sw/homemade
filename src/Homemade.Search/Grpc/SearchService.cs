using Grpc.Core;

using Homemade.Database.Entities;

using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents.Extensions;
using Lucene.Net.Facet;
using Lucene.Net.Facet.Taxonomy;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Util;

using Lucent.Configuration;

using Microsoft.Extensions.Options;

namespace Homemade.Search.Grpc;

/// <summary>
/// The gRPC service for searching recipes.
/// </summary>
public sealed class SearchService(
    ILogger<SearchService> logger,
    IOptionsSnapshot<IndexConfiguration> configuration,
    [FromKeyedServices(nameof(Recipe))] IndexSearcher searcher,
    [FromKeyedServices(nameof(Recipe))] TaxonomyReader taxonomyReader
) : RecipeSearch.RecipeSearchBase
{
    private static readonly QueryParser _parser = new(
        LuceneVersion.LUCENE_48,
        nameof(Recipe.Name),
        new StandardAnalyzer(LuceneVersion.LUCENE_48)
    );

    /// <inheritdoc />
    public override async Task Search(
        SearchQuery request,
        IServerStreamWriter<SearchResult> stream,
        ServerCallContext context
    )
    {
        var pageSize = Math.Max(request.PageSize, 1);
        var skip = Math.Max(request.Page * pageSize, 0);
        var take = skip + pageSize;

        var query = _parser.Parse(request.SearchText);
        logger.LogInformation("Searching for {SearchText}", query);

        // Set up facet collector
        var facetsCollector = new FacetsCollector();
        var topDocs = FacetsCollector.Search(searcher, query, take, facetsCollector);

        // Get facet results
        var facets = new FastTaxonomyFacetCounts(
            taxonomyReader,
            configuration.Get(nameof(Recipe)).FacetsConfig,
            facetsCollector
        );
        var tagsFacets = facets.GetTopChildren(take, nameof(Recipe.Tags));
        var ingredientsFacets = facets.GetTopChildren(take, nameof(RecipeIngredient.Ingredient));

        var pageCount = (uint)Math.Ceiling((double)topDocs.TotalHits / pageSize);

        // Stream results with facets in the first result
        var isFirstResult = true;
        foreach (var scoreDoc in topDocs.ScoreDocs.Skip(skip).Take(pageSize))
        {
            var document = searcher.Doc(scoreDoc.Doc);

            var response = new SearchResult
            {
                Count = topDocs.TotalHits,
                PageCount = pageCount,
                PageSize = (uint)pageSize,
                RecipeId = document.GetField(nameof(Recipe.Id)).GetInt64ValueOrDefault(),
                RecipeName = document.Get(nameof(Recipe.Name)),
                RecipeIcon = document.Get(nameof(Recipe.Icon)),
                Tags = { },
                TotalTimeMinutes = 0,
                Servings = document.GetField(nameof(Recipe.Servings)).GetInt32ValueOrDefault(),
                Difficulty = RecipeDifficulty.Easy,
                IngredientCount = 0
            };

            // Add facets only to the first result
            if (isFirstResult)
            {
                var facetsData = new Facets();

                if (tagsFacets != null)
                {
                    foreach (var labelValue in tagsFacets.LabelValues)
                    {
                        facetsData.Tags.Add(new Facet
                        {
                            Value = labelValue.Label,
                            Count = (int)labelValue.Value
                        });
                    }
                }

                if (ingredientsFacets != null)
                {
                    foreach (var labelValue in ingredientsFacets.LabelValues)
                    {
                        facetsData.Ingredients.Add(new Facet
                        {
                            Value = labelValue.Label,
                            Count = (int)labelValue.Value
                        });
                    }
                }

                response.Facets = facetsData;
                isFirstResult = false;
            }

            await stream.WriteAsync(response, context.CancellationToken);
        }
    }
}