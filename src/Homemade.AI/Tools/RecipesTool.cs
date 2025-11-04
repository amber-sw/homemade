using System.ComponentModel;

using Homemade.Grpc;

using ModelContextProtocol.Server;

namespace Homemade.AI.Tools;

/// <summary>
/// Exposes functionality for searching recipes to LLM.
/// </summary>
[McpServerToolType]
public sealed class RecipesTool
{
    /// <summary>
    /// Find recipes in the system.
    /// </summary>
    [McpServerTool, Description("Search recipes available in the system")]
    public static async Task<IEnumerable<RecipeResult>> FindRecipes(
        McpServer server,
        [Description("Search parameters to find recipes")] RecipeQuery query
    )
    {
        await using var scope = server.Services!.CreateAsyncScope();
        var client = scope.ServiceProvider.GetRequiredService<RecipeSearch.RecipeSearchClient>();

        var response = await client.SearchAsync(query);
        return response.Recipes;
    }
}