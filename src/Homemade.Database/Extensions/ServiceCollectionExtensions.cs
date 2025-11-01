using Microsoft.Extensions.Hosting;

namespace Homemade.Database.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddHomemadeDatabase(this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<HomemadeContext>("recipes");
    }
}