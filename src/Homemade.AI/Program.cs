using Homemade.AI.Services;
using Homemade.AI.Tools;

using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.AI;

using ModelContextProtocol.Client;

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

builder.Services.AddMcpServer()
    .WithHttpTransport()
    .WithTools<RecipesTool>();

builder.AddOllamaApiClient("ollama")
    .AddChatClient()
    .UseFunctionInvocation();

builder.Services.AddSearchClient();

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

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

app.MapMcp("/mcp");

// Configure the HTTP request pipeline.
app.MapGrpcService<ChatService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.MapDefaultEndpoints();
app.Run();