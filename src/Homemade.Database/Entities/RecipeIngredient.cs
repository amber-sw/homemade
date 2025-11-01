namespace Homemade.Database.Entities;

/// <summary>
/// Represents the amount of an ingredient in a recipe.
/// </summary>
public sealed class RecipeIngredient
{
    /// <summary>
    /// The primary key for the recipe ingredient.
    /// </summary>
    [Key]
    public long Id { get; set; }

    /// <summary>
    /// The recipe that this entity relates to.
    /// </summary>
    [Required] public Recipe Recipe { get; set; } = null!;

    /// <summary>
    /// The ingredient.
    /// </summary>
    [Required]
    public required Ingredient Ingredient { get; set; }

    /// <summary>
    /// The amount of the ingredient needed for this recipe.
    /// </summary>
    /// <remarks>Expressed in <see cref="Ingredient.Unit" />.</remarks>
    [Required]
    public required double Amount { get; set; }
}