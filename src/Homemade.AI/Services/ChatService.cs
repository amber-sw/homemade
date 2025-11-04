using System.Text;

using Grpc.Core;

using Homemade.AI.Grpc;
using Homemade.AI.Tools;

using Microsoft.Extensions.AI;

using ModelContextProtocol.Client;

using ChatResponse = Homemade.AI.Grpc.ChatResponse;

namespace Homemade.AI.Services;

/// <summary>
/// </summary>
public sealed class ChatService(IChatClient client, McpClient mcp) : Chat.ChatBase
{
    /// <inheritdoc />
    public override async Task SuggestRecipes(
        ChatRequest request,
        IServerStreamWriter<ChatResponse> stream,
        ServerCallContext context
    )
    {
        var options = new ChatOptions
        {
            Tools = [.. await mcp.ListToolsAsync()]
        };
        var messages = new List<ChatMessage>
        {
            new (ChatRole.User, request.Question)
        };

        var builder = new StringBuilder();
        await foreach (var response in client.GetStreamingResponseAsync(messages, options))
        {
            builder.Append(response.Text);
            await stream.WriteAsync(new ChatResponse
            {
                Message = builder.ToString()
            });
        }
    }
}