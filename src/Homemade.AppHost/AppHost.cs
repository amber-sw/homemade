using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var database = builder.AddPostgres("postgres")
    .AddDatabase("recipes");

var keycloak = builder.AddKeycloak("keycloak");
    // .WithRealmImport("../../keycloak/realms");

builder.AddOllama("ollama")
    .WithDataVolume()
    .AddModel("gemma3:1b");

var cache = builder.AddGarnet("cache");

builder.AddMailPit("mailpit");

builder.AddProject<Homemade_Migrations>("migrations")
    .WithReference(database)
    .WaitFor(database);

builder.AddProject<Homemade_Web>("web")
    .WithHttpHealthCheck("/health")
    .WithReference(keycloak)
    .WithReference(cache);

builder.Build().Run();