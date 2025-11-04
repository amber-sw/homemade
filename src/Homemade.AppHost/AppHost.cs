using Homemade.AppHost.Extensions;

using Projects;

var builder = DistributedApplication.CreateBuilder(args);
builder.AddDockerComposeEnvironment("homemade");

var database = builder.AddPostgres("postgres")
    .AddDatabase("recipes");

var keycloak = builder.AddKeycloak("keycloak")
    .WithBindMount("../../keycloak/themes", "/opt/keycloak/themes")
    .WithRealmImport("../../keycloak/realms");

var ai = builder.AddOllama("ollama")
    .WithDataVolume()
    .AddModel("qwen3");

var cache = builder.AddGarnet("cache");

builder.AddMailPit("mailpit");

builder.AddNats("nats");

var migrations = builder.AddProject<Homemade_Migrations>("migrations")
    .WithReference(database)
    .WaitFor(database);

var search = builder.AddProject<Homemade_Search>("search")
    .WithHttpHealthCheck("/health")
    .WithProjectMount("SEARCH-INDEX-MOUNT", "/app/index", "../../index", "Mount volume for the search index to be stored")
    .WithReference(keycloak)
    .WithReference(database)
    .WaitForCompletion(migrations);

builder.AddProject<Homemade_Web>("web")
    .WithHttpHealthCheck("/health")
    .WithReference(keycloak)
    .WithReference(cache)
    .WithReference(search);

builder.AddProject<Homemade_AI>("ai")
    .WithHttpHealthCheck("/health")
    .WithReference(keycloak)
    .WithReference(ai)
    .WaitFor(ai)
    .WithEnvironment("ConnectionStrings__ollama", ai);

builder.Build().Run();