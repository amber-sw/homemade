namespace homemade.Server.Models
{
    public class IngredientRecipe
    {
        public int RecipesId { get; set; }
        public int IngredientsId { get; set; }
        public Ingredient Ingredient { get; set; } = null!;
        public Recipe Recipe { get; set; } = null!;
        public string Quantity { get; set; }

    }
}
