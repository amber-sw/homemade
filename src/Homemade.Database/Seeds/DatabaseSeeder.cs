using Microsoft.EntityFrameworkCore;

namespace Homemade.Database.Seeds;

/// <summary>
/// Handles seeding the entire database.
/// This works as an entrypoint for all other seeders.
/// </summary>
public static class DatabaseSeeder
{
    /// <inheritdoc cref="DbContextOptionsBuilder.UseAsyncSeeding" />
    public static async Task SeedAsync(DbContext context, bool _, CancellationToken cancellationToken)
    {
        await RecipeSeeder.SeedAsync(context, _, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
    }
}