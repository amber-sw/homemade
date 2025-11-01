namespace Homemade.Database.Entities;

/// <summary>
/// Represents a recipe in the system.
/// </summary>
public sealed class Recipe
{
    /// <summary>
    /// Gets or sets the unique identifier for the recipe.
    /// </summary>
    [Key]
    public required long Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the recipe.
    /// </summary>
    [Required]
    [MaxLength(200)]
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the emoji icon representing the recipe.
    /// </summary>
    [Required]
    [MaxLength(10)]
    public required string Icon { get; set; }

    /// <summary>
    /// Gets or sets the description of the recipe.
    /// </summary>
    [Required]
    [MaxLength(1000)]
    public required string Description { get; set; }

    /// <summary>
    /// Gets or sets the preparation time.
    /// </summary>
    [Required]
    public required TimeSpan PreparationTime { get; set; }

    /// <summary>
    /// Gets or sets the cooking time.
    /// </summary>
    [Required]
    public required TimeSpan CookingTime { get; set; }

    /// <summary>
    /// Gets or sets the number of servings the recipe yields.
    /// </summary>
    [Required]
    public required int Servings { get; set; }

    /// <summary>
    /// Gets or sets the difficulty level of the recipe.
    /// </summary>
    [Required]
    public required RecipeDifficulty Difficulty { get; set; }

    /// <summary>
    /// Gets or sets optional notes or tips for the recipe.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the recipe was created.
    /// </summary>
    [Required]
    public required DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the recipe was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the list of ingredients for this recipe.
    /// </summary>
    public List<RecipeIngredient> Ingredients { get; set; } = [];

    /// <summary>
    /// Gets or sets the list of instructions for this recipe.
    /// </summary>
    public List<Instruction> Instructions { get; set; } = [];

    /// <summary>
    /// Gets or sets the tags associated with this recipe.
    /// </summary>
    public List<Tag> Tags { get; set; } = [];
}

/// <summary>
/// Represents the difficulty level of a recipe.
/// </summary>
public enum RecipeDifficulty
{
    /// <summary>
    /// Easy difficulty - suitable for beginners.
    /// </summary>
    Easy,

    /// <summary>
    /// Medium difficulty - requires some cooking experience.
    /// </summary>
    Medium,

    /// <summary>
    /// Hard difficulty - requires advanced cooking skills.
    /// </summary>
    Hard
}