using homemade.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace homemade.Server.Data
{
    public class RecipeContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlite("DataSource=homemadeDB; Cache=Shared");
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<IngredientRecipe> IngredientsRecipes { get; set; }
    }
}
