using Google.Protobuf.WellKnownTypes;

using Grpc.Core;

using Homemade.Search.Grpc.Models;

using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents.Extensions;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Util;

namespace Homemade.Search.Grpc;

/// <summary>
/// The gRPC service for searching recipes.
/// </summary>
public sealed class SearchService(
    [FromKeyedServices(nameof(Recipe))] IndexSearcher searcher
) : RecipeSearch.RecipeSearchBase
{
    /// <inheritdoc />
    public override async Task Search(SearchRequest request, IServerStreamWriter<SearchReply> stream,
        ServerCallContext context)
    {
        var query = new QueryParser(LuceneVersion.LUCENE_48, nameof(Recipe.Name),
            new StandardAnalyzer(LuceneVersion.LUCENE_48)).Parse(request.SearchText);
        var topDocs = searcher.Search(query, 100);

        foreach (var scoreDoc in topDocs.ScoreDocs)
        {
            var document = searcher.Doc(scoreDoc.Doc);

            var response = new SearchReply
            {
                Recipe = new Recipe
                {
                    Id = document.GetField(nameof(Recipe.Id)).GetInt64ValueOrDefault(),
                    Name = document.GetField(nameof(Recipe.Name)).GetStringValue(),
                    Icon = string.Empty,
                    Description = string.Empty,
                    PreparationTime = Duration.FromTimeSpan(TimeSpan.Zero),
                    CookingTime = Duration.FromTimeSpan(TimeSpan.Zero),
                    Servings = 1,
                    Difficulty = RecipeDifficulty.Unspecified,
                    Notes = string.Empty,
                    CreatedAt = Timestamp.FromDateTime(DateTime.UtcNow),
                    UpdatedAt = Timestamp.FromDateTime(DateTime.UtcNow),
                    Ingredients = { },
                    Instructions = { },
                    Tags = { }
                }
            };

            await stream.WriteAsync(response, context.CancellationToken);
        }
    }
}