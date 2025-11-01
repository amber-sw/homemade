using Homemade.Database.Entities;

using Microsoft.EntityFrameworkCore;

namespace Homemade.Database;

/// <summary>
/// The <see cref="DbContext" /> for the Homemade database.
/// </summary>
public sealed class HomemadeContext(
    DbContextOptions<HomemadeContext> options
) : DbContext(options)
{
    /// <summary>
    /// The recipes in the database.
    /// </summary>
    public required DbSet<Recipe> Recipes { get; set; }

    /// <summary>
    /// The ingredients in the database.   
    /// </summary>
    public required DbSet<Ingredient> Ingredients { get; set; }

    /// <summary>
    /// The tags in the database.
    /// </summary>
    public required DbSet<Tag> Tags { get; set; }
}