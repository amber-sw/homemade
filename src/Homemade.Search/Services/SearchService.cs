using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;

using Grpc.Core;

using Homemade.Database;

namespace Homemade.Search.Services;

/// <summary>
/// The gRPC service for searching recipes.
/// </summary>
public sealed class SearchService(
    HomemadeContext dbContext
) : RecipeSearch.RecipeSearchBase
{
    /// <inheritdoc />
    public override async Task Search(SearchRequest request, IServerStreamWriter<SearchReply> stream, ServerCallContext context)
    {
        var recipes = dbContext.Recipes.AsAsyncEnumerable();

        await foreach (var recipe in recipes.WithCancellation(context.CancellationToken))
        {
            var response = new SearchReply
            {
                Recipe = new Recipe
                {
                    Id = recipe.Id,
                    Name = recipe.Name,
                    Icon = recipe.Icon,
                    Description = recipe.Description,
                    PreparationTime = recipe.PreparationTime.ToDuration(),
                    CookingTime = recipe.CookingTime.ToDuration(),
                    Servings = recipe.Servings,
                    Difficulty = recipe.Difficulty switch
                    {
                        Database.Entities.RecipeDifficulty.Easy => RecipeDifficulty.Easy,
                        Database.Entities.RecipeDifficulty.Medium => RecipeDifficulty.Medium,
                        Database.Entities.RecipeDifficulty.Hard => RecipeDifficulty.Hard,
                        _ => RecipeDifficulty.Unspecified
                    },
                    Notes = recipe.Notes,
                    CreatedAt = recipe.CreatedAt.ToTimestamp(),
                    UpdatedAt = recipe.UpdatedAt?.ToTimestamp(),
                    Ingredients = {},
                    Instructions = {},
                    Tags = {}
                }
            };

            await stream.WriteAsync(response, context.CancellationToken);
        }
    }
}