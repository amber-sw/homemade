using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var database = builder.AddPostgres("postgres")
    .AddDatabase("recipes");

var keycloak = builder.AddKeycloak("keycloak")
    .WithBindMount("../../keycloak/themes", "/opt/keycloak/themes")
    .WithRealmImport("../../keycloak/realms");

builder.AddOllama("ollama")
    .WithDataVolume()
    .AddModel("gemma3:1b");

var cache = builder.AddGarnet("cache");

builder.AddMailPit("mailpit");

var migrations = builder.AddProject<Homemade_Migrations>("migrations")
    .WithReference(database)
    .WaitFor(database);

var search = builder.AddProject<Homemade_Search>("search")
    .WithHttpHealthCheck("/health")
    .WithReference(keycloak)
    .WithReference(database)
    .WaitForCompletion(migrations);

builder.AddProject<Homemade_Web>("web")
    .WithHttpHealthCheck("/health")
    .WithReference(keycloak)
    .WithReference(cache)
    .WithReference(search);

builder.Build().Run();