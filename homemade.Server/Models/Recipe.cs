namespace homemade.Server.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public required List<string> Instructions { get; set; }
        public List<IngredientRecipe> IngredientRecipes { get; } = [];
        public List<Ingredient> Ingredients { get; } = [];

    }
}
