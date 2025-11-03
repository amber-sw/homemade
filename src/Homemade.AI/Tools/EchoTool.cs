using System.ComponentModel;

using ModelContextProtocol.Server;

namespace Homemade.AI.Tools;

/// <summary>
/// 
/// </summary>
[McpServerToolType]
public sealed class EchoTool
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="server"></param>
    /// <param name="client"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    [McpServerTool]
    public static Task<string> Echo(McpServer server, HttpClient client, [Description("The message to echo.")] string message)
    {
        return Task.FromResult(message);
    }
}