using Homemade.Database;
using Homemade.Database.Seeds;

using Microsoft.EntityFrameworkCore;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Hosting;

/// <summary>
/// Contains extension methods for adding Homemade database services to an <see cref="IHostApplicationBuilder" />.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the <see cref="DbContext" /> and repositories for the Homemade database.
    /// </summary>
    public static void AddHomemadeDatabase(this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<HomemadeContext>("recipes", null, options =>
        {
            options.UseAsyncSeeding(DatabaseSeeder.SeedAsync);
        });
    }
}