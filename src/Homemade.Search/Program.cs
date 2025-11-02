using Homemade.Database.Entities;
using Homemade.Search.Configuration;
using Homemade.Search.Grpc;
using Homemade.Search.Services;

using Lucene.Net.Store;

using Lucent.Configuration;

using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

// Configure Kestrel to support both HTTP/1.1 (for health checks) and HTTP/2 (for gRPC)
builder.WebHost.ConfigureKestrel(options =>
{
    options.ConfigureEndpointDefaults(listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
    });
});

builder.AddHomemadeDatabase();

builder.Services.AddAuthentication()
    .AddKeycloakJwtBearer(
        serviceName: "keycloak",
        realm: "Homemade",
        configureOptions: options =>
        {
            options.Audience = "homemade.api";

            // For development only - disable HTTPS metadata validation
            // In production, use explicit Authority configuration instead
            if (builder.Environment.IsDevelopment())
            {
                options.RequireHttpsMetadata = false;
            }
        });

builder.Services.AddAuthorizationBuilder();

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddNamedLucentIndex(nameof(Recipe));
builder.Services.AddScoped<IndexService>();
builder.Services.AddSingleton<IConfigureOptions<IndexConfiguration>, IndexConfigurator>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await scope.ServiceProvider.GetRequiredService<IndexService>().BuildIndex(CancellationToken.None);
}

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
// TODO: enforce authorization
app.MapGrpcService<SearchService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();