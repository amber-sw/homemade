using Grpc.Core;

using ModelContextProtocol.Client;

namespace Homemade.AI.Extensions;

/// <summary>
/// Contains extension methods for <see cref="ServerCallContext" />.
/// </summary>
public static class ServerCallContextExtensions
{
    /// <summary>
    /// Creates a <see cref="McpClient" /> that calls this application's MCP endpoint.
    /// </summary>
    public static async Task<McpClient> CreateMcpClient(this ServerCallContext context, CancellationToken cancellationToken = default)
    {
        var request = context.GetHttpContext().Request;

        return await McpClient.CreateAsync(new HttpClientTransport(
            new HttpClientTransportOptions
            {
                Endpoint = new Uri($"{request.Scheme}://{request.Host}/mcp")
            }
        ), cancellationToken: cancellationToken);
    }
}