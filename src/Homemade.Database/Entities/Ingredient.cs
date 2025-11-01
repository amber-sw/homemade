namespace Homemade.Database.Entities;

/// <summary>
/// Information of a specific ingredient that can be shared among recipes.
/// </summary>
public sealed class Ingredient
{
    /// <summary>
    /// Gets or sets the unique identifier for the recipe.
    /// </summary>
    public required long Id { get; set; }

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
    [Required]
    [MaxLength(200)]
    public required string Plural { get; set; }

    /// <summary>
    /// The unit of measurement for this ingredient.
    /// </summary>
    /// <example>kg, slices, leafs</example>
    [Required]
    [MaxLength(100)]
    public required string Unit { get; set; }
}