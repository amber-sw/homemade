using Homemade.Database;
using Homemade.Database.Extensions;
using Homemade.Migrations.Hosted;

var builder = Host.CreateApplicationBuilder(args);
builder.AddServiceDefaults();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource("MigrationsWorker"));

builder.AddHomemadeDatabase();
builder.Services.AddHostedService<MigrationsWorker<HomemadeContext>>();

var host = builder.Build();
host.Run();