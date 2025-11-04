using Homemade.AI.Client.Handlers;
using Homemade.AI.Grpc;

using OpenTelemetry.Trace;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Contains extension methods for <see cref="IServiceCollection" />.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the gRPC client for the Homemade search service.
    /// </summary>
    public static void AddAiClient(this IServiceCollection services)
    {
        services.AddHttpContextAccessor()
            .AddTransient<AuthorizationHandler>();

        services.AddGrpcClient<Chat.ChatClient>(client =>
            {
                client.Address = new Uri("https://ai");
            })
            .AddServiceDiscovery()
            .AddHttpMessageHandler<AuthorizationHandler>();

        services.AddOpenTelemetry()
            .WithTracing(static tracing => tracing.AddGrpcClientInstrumentation());
    }
}