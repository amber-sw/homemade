using System.Diagnostics;

using Microsoft.EntityFrameworkCore;

namespace Homemade.Migrations.Hosted;

/// <summary>
/// A <see cref="BackgroundService" /> that executes migrations for <typeparamref name="TContext" />.
/// </summary>
public sealed class MigrationsWorker<TContext>(
    ILogger<MigrationsWorker<TContext>> logger,
    IServiceProvider serviceProvider,
    IHostApplicationLifetime applicationLifetime
) : BackgroundService where TContext : DbContext
{
    private static readonly ActivitySource Source = new("MigrationsWorker");

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var name = typeof(TContext).Name;
        logger.LogTrace("Starting migrations worker for {Context}", name);
        await using var scope = serviceProvider.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<TContext>();

        // ReSharper disable once ExplicitCallerInfoArgument
        using var activity = Source.StartActivity($"{name}.{nameof(ExecuteAsync)}");
        await RunMigrationAsync(context, cancellationToken);

        applicationLifetime.StopApplication();
    }

    /// <summary>
    /// If there are unapplied migrations they will be applied.
    /// </summary>
    private static async Task RunMigrationAsync(TContext context, CancellationToken cancellationToken)
    {
        using var activity = Source.StartActivity();

        var strategy = context.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(context, static async (dbContext, ct) =>
        {
            await dbContext.Database.MigrateAsync(ct);
        }, cancellationToken);
    }
}