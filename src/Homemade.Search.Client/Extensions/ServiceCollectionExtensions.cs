using Homemade.Search.Client.Handlers;
using Homemade.Search.Grpc;

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
    public static void AddSearchClient(this IServiceCollection services)
    {
        services.AddHttpContextAccessor()
            .AddTransient<AuthorizationHandler>();

        services.AddGrpcClient<RecipeSearch.RecipeSearchClient>(client =>
            {
                client.Address = new Uri("https://search");
            })
            // .ConfigurePrimaryHttpMessageHandler(() =>
            // {
            //     var handler = new SocketsHttpHandler
            //     {
            //         EnableMultipleHttp2Connections = true
            //     };
            //     return handler;
            // })
            .AddServiceDiscovery()
            .AddHttpMessageHandler<AuthorizationHandler>();

        services.AddOpenTelemetry()
            .WithTracing(static tracing => tracing.AddGrpcClientInstrumentation());
    }
}