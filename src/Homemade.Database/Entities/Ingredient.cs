namespace Homemade.Database.Entities;

/// <summary>
/// Information of a specific ingredient that can be shared among recipes.
/// </summary>
public sealed class Ingredient
{
    /// <summary>
    /// Gets or sets the unique identifier for the recipe.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// The name of this ingredient.
    /// </summary>
    /// <example>Tomato</example>   
    [Required]
    [MaxLength(200)]
    public required string Name { get; set; }

    /// <summary>
    /// The plural form of the ingredient name.
    /// </summary>
    /// <example>Tomatoes</example>
    [MaxLength(200)]
    public string? Plural { get; set; }
}