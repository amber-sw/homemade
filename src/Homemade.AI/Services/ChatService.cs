using Grpc.Core;

using Homemade.AI.Grpc;

using Microsoft.Extensions.AI;

using ChatResponse = Homemade.AI.Grpc.ChatResponse;

namespace Homemade.AI.Services;

/// <summary>
/// </summary>
/// <param name="client"></param>
public class ChatService(
    IChatClient client
) : Chat.ChatBase
{
    /// <inheritdoc />
    public override async Task<ChatResponse> SayHello(ChatRequest request, ServerCallContext context)
    {
        var options = new ChatOptions
        {
            Tools = []
        };

        var response = await client.GetResponseAsync([
            new ChatMessage(ChatRole.User, request.Question)
        ], options: options);
        return new ChatResponse { Message = response.Text };
    }
}