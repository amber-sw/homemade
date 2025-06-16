namespace homemade.Server.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<IngredientRecipe> IngredientRecipes { get; } = [];
        public List<Recipe> Recipes { get; } = [];
    }
}
