using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;

using Grpc.Core;

using Homemade.Database.Entities;
using Homemade.Grpc;
using Homemade.Grpc.Shared;

using Lucene.Net.Analysis.Standard;
using Lucene.Net.Facet;
using Lucene.Net.Facet.Taxonomy;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Util;

using Lucent.Configuration;

using Microsoft.Extensions.Options;

using RecipeDifficulty = Homemade.Grpc.RecipeDifficulty;

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
    private readonly FacetsCollector _collector = new();
    private static readonly QueryParser _parser = new(
        LuceneVersion.LUCENE_48,
        nameof(Recipe.Name),
        new StandardAnalyzer(LuceneVersion.LUCENE_48)
    );

    /// <inheritdoc />
    public override Task<RecipeResults> Search(RecipeQuery request, ServerCallContext context)
    {
        var pageSize = Math.Max(request.Pagination?.PageSize ?? 0, 1);
        var skip = Math.Max((request.Pagination?.Page ?? 0) * pageSize, 0);
        var take = skip + pageSize;

        var query = string.IsNullOrWhiteSpace(request.SearchText)
            ? new MatchAllDocsQuery()
            : _parser.Parse(request.SearchText);
        logger.LogInformation("Searching for {SearchText}", query);

        // Set up facet collector
        var topDocs = FacetsCollector.Search(searcher, query, take, _collector);

        // Get facet results
        var facets = new FastTaxonomyFacetCounts(
            taxonomyReader,
            configuration.Get(nameof(Recipe)).FacetsConfig,
            _collector
        );
        var tagsFacets = facets.GetTopChildren(take, nameof(Recipe.Tags));
        var ingredientsFacets = facets.GetTopChildren(take, nameof(RecipeIngredient.Ingredient));

        var pageCount = (int)Math.Ceiling((double)topDocs.TotalHits / pageSize);

        var result = new RecipeResults
        {
            Pagination = new Pagination { Count = topDocs.TotalHits, PageCount = pageCount, PageSize = pageSize, },
        };

        result.Tags.AddRange(tagsFacets?.LabelValues.Select(facet => new FacetValue
        {
            Name = facet.Label,
            Count = (int)facet.Value
        }) ?? []);

        result.Ingredients.AddRange(ingredientsFacets?.LabelValues.Select(facet => new FacetValue
        {
            Name = facet.Label,
            Count = (int)facet.Value
        }) ?? []);

        foreach (var scoreDoc in topDocs.ScoreDocs)
        {
            var document = searcher.Doc(scoreDoc.Doc);
            var recipe = new RecipeResult
            {
                Id = document.GetField(nameof(Recipe.Id)).GetInt64Value() ?? -1,
                Name = document.GetField(nameof(Recipe.Name)).GetStringValue(),
                Icon = document.GetField(nameof(Recipe.Icon)).GetStringValue(),
                TotalTimeMinutes = new Duration(),
                Servings = document.GetField(nameof(Recipe.Servings)).GetInt32Value() ?? -1,
                Difficulty = RecipeDifficulty.Unknown,
                IngredientCount = -1,
            };

            foreach (var tag in document.GetValues(nameof(Recipe.Tags)))
                recipe.Tags.Add(tag);

            result.Recipes.Add(recipe);
        }

        return Task.FromResult(result);
    }

    /// <inheritdoc />
    public override async Task LiveSearch(IAsyncStreamReader<RecipeQuery> requestStream, IServerStreamWriter<RecipeResults> responseStream, ServerCallContext context)
    {
        await foreach (var query in requestStream.ReadAllAsync(context.CancellationToken))
        {
            var result = await Search(query, context);

            await responseStream.WriteAsync(result, context.CancellationToken);
        }
    }
}