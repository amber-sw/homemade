namespace homemade.Server.Models
{
    public class IngredientRecipe
    {
        public int Id { get; set; }
        public Ingredient Ingredient { get; set; }
        public Recipe Recipe { get; set; }
        public string Quantity { get; set; }

    }
}
