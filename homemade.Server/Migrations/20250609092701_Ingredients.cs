using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace homemade.Server.Migrations
{
    /// <inheritdoc />
    public partial class Ingredients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Instructions",
                table: "Recipes",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngredientsRecipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IngredientId = table.Column<int>(type: "INTEGER", nullable: false),
                    RecipeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientsRecipes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "IngredientsRecipes");

            migrationBuilder.DropColumn(
                name: "Instructions",
                table: "Recipes");
        }
    }
}
