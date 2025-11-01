var builder = DistributedApplication.CreateBuilder(args);

builder.AddPostgres("postgres")
    .AddDatabase("recipes");

builder.AddKeycloak("keycloak");
    // .WithRealmImport("../../keycloak/realms");

builder.AddOllama("ollama")
    .AddModel("gemma3:1b");

builder.AddGarnet("cache");

builder.AddMailPit("mailpit");

builder.Build().Run();