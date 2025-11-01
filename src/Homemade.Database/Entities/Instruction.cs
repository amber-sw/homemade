namespace Homemade.Database.Entities;

/// <summary>
/// Represents a step in a recipe's instructions.
/// </summary>
public sealed class Instruction
{
    /// <summary>
    /// Gets or sets the unique identifier for the recipe instruction.
    /// </summary>
    [Key]
    public required long Id { get; set; }

    /// <summary>
    /// Gets or sets the instruction text for this step.
    /// </summary>
    [Required]
    [MaxLength(2000)]
    public required string Text { get; set; }

    /// <summary>
    /// Gets or sets the order in which this instruction should be displayed.
    /// </summary>
    [Required]
    public required int Order { get; set; }

    /// <summary>
    /// Gets or sets the recipe this instruction belongs to.
    /// </summary>
    [Required]
    public required Recipe Recipe { get; set; }
}