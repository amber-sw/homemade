using Homemade.Database.Entities;

using Lucene.Net.Documents;

namespace Homemade.Search.Mapping;

/// <summary>
/// Handles mapping <see cref="Recipe" /> to <see cref="Document" />.
/// </summary>
public static class RecipeDocumentMapper
{
    /// <summary>
    /// Map <see cref="Recipe" /> to <see cref="Document" />.
    /// </summary>
    public static Document Map(Recipe recipe)
    {
        var document = new Document
        {
            new Int64Field(nameof(Recipe.Id), recipe.Id, Field.Store.YES),
            new TextField(nameof(Recipe.Name), recipe.Name, Field.Store.YES),
            new StringField(nameof(Recipe.Icon), recipe.Icon, Field.Store.YES),
            new TextField(nameof(Recipe.Description), recipe.Description, Field.Store.YES),
            new Int32Field(nameof(Recipe.PreparationTime), (int)recipe.PreparationTime.TotalMinutes,
                Field.Store.YES),
            new Int32Field(nameof(Recipe.CookingTime), (int)recipe.CookingTime.TotalMinutes, Field.Store.YES),
            new Int32Field(nameof(Recipe.Servings), recipe.Servings, Field.Store.YES),
            new StringField(nameof(Recipe.Difficulty), recipe.Difficulty.ToString(), Field.Store.YES),
            new TextField(nameof(Recipe.Notes), recipe.Notes ?? string.Empty, Field.Store.NO),
        };

        foreach (var tag in recipe.Tags)
            document.Add(new TextField(nameof(Recipe.Tags), tag.Name, Field.Store.YES));

        foreach (var recipeIngredient in recipe.Ingredients)
            document.Add(new TextField(nameof(Recipe.Ingredients), recipeIngredient.Ingredient.Name, Field.Store.NO));

        foreach (var instruction in recipe.Instructions)
            document.Add(new TextField(nameof(Recipe.Instructions), instruction.Text, Field.Store.NO));

        return document;
    }
}