using Grpc.Core;

using Homemade.Database.Entities;

using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents.Extensions;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Util;

namespace Homemade.Search.Grpc;

/// <summary>
/// The gRPC service for searching recipes.
/// </summary>
public sealed class SearchService(
    ILogger<SearchService> logger,
    [FromKeyedServices(nameof(Recipe))] IndexSearcher searcher
) : RecipeSearch.RecipeSearchBase
{
    private static readonly QueryParser _parser = new QueryParser(LuceneVersion.LUCENE_48, nameof(Recipe.Name),
        new StandardAnalyzer(LuceneVersion.LUCENE_48));

    /// <inheritdoc />
    public override async Task Search(
        SearchQuery request,
        IServerStreamWriter<SearchResult> stream,
        ServerCallContext context
    )
    {
        var query = _parser.Parse(request.SearchText);
        logger.LogInformation("Searching for {SearchText}", query);
        var topDocs = searcher.Search(query, 100);
        foreach (var scoreDoc in topDocs.ScoreDocs)
        {
            var document = searcher.Doc(scoreDoc.Doc);

            var response = new SearchResult
            {
                Count = topDocs.TotalHits,
                PageCount = 0,
                PageSize = 0,
                RecipeId = document.GetField(nameof(Recipe.Id)).GetInt64ValueOrDefault(),
                RecipeName = document.Get(nameof(Recipe.Name)),
                RecipeIcon = document.Get(nameof(Recipe.Icon)),
                Tags = { },
                TotalTimeMinutes = 0,
                Servings = 0,
                Difficulty = RecipeDifficulty.Easy,
                IngredientCount = 0,
                Facets = { }
            };

            await stream.WriteAsync(response, context.CancellationToken);
        }
    }
}