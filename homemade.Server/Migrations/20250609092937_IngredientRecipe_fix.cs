using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace homemade.Server.Migrations
{
    /// <inheritdoc />
    public partial class IngredientRecipe_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_IngredientsRecipes_IngredientId",
                table: "IngredientsRecipes",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsRecipes_RecipeId",
                table: "IngredientsRecipes",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientsRecipes_Ingredients_IngredientId",
                table: "IngredientsRecipes",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientsRecipes_Recipes_RecipeId",
                table: "IngredientsRecipes",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientsRecipes_Ingredients_IngredientId",
                table: "IngredientsRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientsRecipes_Recipes_RecipeId",
                table: "IngredientsRecipes");

            migrationBuilder.DropIndex(
                name: "IX_IngredientsRecipes_IngredientId",
                table: "IngredientsRecipes");

            migrationBuilder.DropIndex(
                name: "IX_IngredientsRecipes_RecipeId",
                table: "IngredientsRecipes");
        }
    }
}
