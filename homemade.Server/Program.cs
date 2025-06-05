using homemade.Server.Data;
using homemade.Server.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<RecipeContext>();

var app = builder.Build();

//app.UseDefaultFiles();
//app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/recipes", (RecipeContext db) =>
{
    var recipes = db.Recipes;

    return Results.Ok(recipes);
});

app.MapGet("/recipes/{id}", (int id, RecipeContext db) =>
{
    var recipes = db.Recipes;
    var recipe = recipes.SingleOrDefault(r => r.Id == id);

    if(recipe == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(recipe);
});

app.MapPost("/recipes", (RecipeContext db, Recipe recipe) =>
{
    db.Recipes.Add(recipe);
    db.SaveChanges();
    return Results.Created($"/recipes/{recipe.Id}", recipe);
});

app.MapPut("/recipes/{id}", (int id, Recipe recipe, RecipeContext db) =>
{
    db.Entry(recipe).State = EntityState.Modified;
    db.SaveChanges();
    return Results.Ok(recipe);
});

app.MapDelete("/recipes/{id}", (int id, RecipeContext db) =>
{
    var recipeEntity = db.Recipes.SingleOrDefault(r => r.Id == id);
    if (recipeEntity == null)
    {
        return Results.NotFound();
    }

    db.Recipes.Remove(recipeEntity);
    db.SaveChanges();
    return Results.NoContent();
});

app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.MapFallbackToFile("/index.html");

app.Run();
