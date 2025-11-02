using Homemade.Database.Entities;

using Microsoft.EntityFrameworkCore;

namespace Homemade.Database.Seeds;

/// <summary>
/// Handles seeding the recipe table and related entities.
/// </summary>
public static class RecipeSeeder
{
    /// <inheritdoc cref="DbContextOptionsBuilder.UseAsyncSeeding" />
    public static async Task SeedAsync(DbContext context, bool _, CancellationToken cancellationToken)
    {
        // Create shared ingredient instances
        var ingredients = CreateIngredients();

        // Create shared tag instances
        var tags = CreateTags();

        await context.Set<Recipe>()
            .AddRangeAsync([
                SpaghettiCarbonara(ingredients, tags),
                ClassicMargheritaPizza(ingredients, tags),
                ChickenTikkaMasala(ingredients, tags),
                BeefBourguignon(ingredients, tags),
                ThaiGreenCurry(ingredients, tags),
                ClassicCaesarSalad(ingredients, tags),
                ChocolateBrownies(ingredients, tags),
                TomatoBasilSoup(ingredients, tags),
                GreekMoussaka(ingredients, tags),
                PanSearedSalmon(ingredients, tags),
                VegetableStirFry(ingredients, tags),
                ClassicLasagne(ingredients, tags),
                ChickenFajitas(ingredients, tags),
                MushroomRisotto(ingredients, tags),
                ShepherdsPie(ingredients, tags),
            ], cancellationToken);
    }

    private static Dictionary<string, Ingredient> CreateIngredients()
    {
        return new Dictionary<string, Ingredient>
        {
            ["Spaghetti"] = new Ingredient { Id = 1, Name = "Spaghetti", Plural = "Spaghetti" },
            ["Pancetta"] = new Ingredient { Id = 2, Name = "Pancetta", Plural = "Pancetta" },
            ["Egg"] = new Ingredient { Id = 3, Name = "Egg", Plural = "Eggs" },
            ["Parmesan cheese"] = new Ingredient { Id = 4, Name = "Parmesan cheese", Plural = "Parmesan cheese" },
            ["Black pepper"] = new Ingredient { Id = 5, Name = "Black pepper", Plural = "Black pepper" },
            ["Pizza dough"] = new Ingredient { Id = 6, Name = "Pizza dough", Plural = "Pizza dough" },
            ["Tomato passata"] = new Ingredient { Id = 7, Name = "Tomato passata", Plural = "Tomato passata" },
            ["Fresh mozzarella"] = new Ingredient { Id = 8, Name = "Fresh mozzarella", Plural = "Fresh mozzarella" },
            ["Fresh basil"] = new Ingredient { Id = 9, Name = "Fresh basil", Plural = "Fresh basil" },
            ["Olive oil"] = new Ingredient { Id = 10, Name = "Olive oil", Plural = "Olive oil" },
            ["Chicken breast"] = new Ingredient { Id = 11, Name = "Chicken breast", Plural = "Chicken breasts" },
            ["Natural yoghurt"] = new Ingredient { Id = 12, Name = "Natural yoghurt", Plural = "Natural yoghurt" },
            ["Garam masala"] = new Ingredient { Id = 13, Name = "Garam masala", Plural = "Garam masala" },
            ["Tinned tomatoes"] = new Ingredient { Id = 14, Name = "Tinned tomatoes", Plural = "Tinned tomatoes" },
            ["Double cream"] = new Ingredient { Id = 15, Name = "Double cream", Plural = "Double cream" },
            ["Onion"] = new Ingredient { Id = 16, Name = "Onion", Plural = "Onions" },
            ["Beef chuck"] = new Ingredient { Id = 17, Name = "Beef chuck", Plural = "Beef chuck" },
            ["Red wine"] = new Ingredient { Id = 18, Name = "Red wine", Plural = "Red wine" },
            ["Bacon lardons"] = new Ingredient { Id = 19, Name = "Bacon lardons", Plural = "Bacon lardons" },
            ["Button mushrooms"] = new Ingredient { Id = 20, Name = "Button mushrooms", Plural = "Button mushrooms" },
            ["Pearl onions"] = new Ingredient { Id = 21, Name = "Pearl onions", Plural = "Pearl onions" },
            ["Green curry paste"] = new Ingredient { Id = 22, Name = "Green curry paste", Plural = "Green curry paste" },
            ["Coconut milk"] = new Ingredient { Id = 23, Name = "Coconut milk", Plural = "Coconut milk" },
            ["Green beans"] = new Ingredient { Id = 24, Name = "Green beans", Plural = "Green beans" },
            ["Red bell pepper"] = new Ingredient { Id = 25, Name = "Red bell pepper", Plural = "Red bell peppers" },
            ["Romaine lettuce"] = new Ingredient { Id = 26, Name = "Romaine lettuce", Plural = "Romaine lettuce" },
            ["Croutons"] = new Ingredient { Id = 27, Name = "Croutons", Plural = "Croutons" },
            ["Mayonnaise"] = new Ingredient { Id = 28, Name = "Mayonnaise", Plural = "Mayonnaise" },
            ["Anchovy fillets"] = new Ingredient { Id = 29, Name = "Anchovy fillets", Plural = "Anchovy fillets" },
            ["Dark chocolate"] = new Ingredient { Id = 30, Name = "Dark chocolate", Plural = "Dark chocolate" },
            ["Butter"] = new Ingredient { Id = 31, Name = "Butter", Plural = "Butter" },
            ["Caster sugar"] = new Ingredient { Id = 32, Name = "Caster sugar", Plural = "Caster sugar" },
            ["Plain flour"] = new Ingredient { Id = 33, Name = "Plain flour", Plural = "Plain flour" },
            ["Ripe tomatoes"] = new Ingredient { Id = 34, Name = "Ripe tomatoes", Plural = "Ripe tomatoes" },
            ["Garlic clove"] = new Ingredient { Id = 35, Name = "Garlic clove", Plural = "Garlic cloves" },
            ["Vegetable stock"] = new Ingredient { Id = 36, Name = "Vegetable stock", Plural = "Vegetable stock" },
            ["Aubergine"] = new Ingredient { Id = 37, Name = "Aubergine", Plural = "Aubergines" },
            ["Lamb mince"] = new Ingredient { Id = 38, Name = "Lamb mince", Plural = "Lamb mince" },
            ["Milk"] = new Ingredient { Id = 39, Name = "Milk", Plural = "Milk" },
            ["Salmon fillet"] = new Ingredient { Id = 40, Name = "Salmon fillet", Plural = "Salmon fillets" },
            ["Lemon"] = new Ingredient { Id = 41, Name = "Lemon", Plural = "Lemons" },
            ["Broccoli florets"] = new Ingredient { Id = 42, Name = "Broccoli florets", Plural = "Broccoli florets" },
            ["Carrot"] = new Ingredient { Id = 43, Name = "Carrot", Plural = "Carrots" },
            ["Soy sauce"] = new Ingredient { Id = 44, Name = "Soy sauce", Plural = "Soy sauce" },
            ["Ginger"] = new Ingredient { Id = 45, Name = "Ginger", Plural = "Ginger" },
            ["Lasagne sheets"] = new Ingredient { Id = 46, Name = "Lasagne sheets", Plural = "Lasagne sheets" },
            ["Beef mince"] = new Ingredient { Id = 47, Name = "Beef mince", Plural = "Beef mince" },
            ["Fajita seasoning"] = new Ingredient { Id = 48, Name = "Fajita seasoning", Plural = "Fajita seasoning" },
            ["Flour tortillas"] = new Ingredient { Id = 49, Name = "Flour tortillas", Plural = "Flour tortillas" },
            ["Arborio rice"] = new Ingredient { Id = 50, Name = "Arborio rice", Plural = "Arborio rice" },
            ["Potatoes"] = new Ingredient { Id = 51, Name = "Potatoes", Plural = "Potatoes" },
            ["Peas"] = new Ingredient { Id = 52, Name = "Peas", Plural = "Peas" },
            ["Worcestershire sauce"] = new Ingredient { Id = 53, Name = "Worcestershire sauce", Plural = "Worcestershire sauce" },
        };
    }

    private static Dictionary<string, Tag> CreateTags()
    {
        return new Dictionary<string, Tag>
        {
            ["Italian"] = new Tag { Id = 1, Name = "Italian" },
            ["Pasta"] = new Tag { Id = 2, Name = "Pasta" },
            ["Quick & Easy"] = new Tag { Id = 3, Name = "Quick & Easy" },
            ["Pizza"] = new Tag { Id = 4, Name = "Pizza" },
            ["Vegetarian"] = new Tag { Id = 5, Name = "Vegetarian" },
            ["Indian"] = new Tag { Id = 6, Name = "Indian" },
            ["Curry"] = new Tag { Id = 7, Name = "Curry" },
            ["Spicy"] = new Tag { Id = 8, Name = "Spicy" },
            ["French"] = new Tag { Id = 9, Name = "French" },
            ["Stew"] = new Tag { Id = 10, Name = "Stew" },
            ["Comfort Food"] = new Tag { Id = 11, Name = "Comfort Food" },
            ["Thai"] = new Tag { Id = 12, Name = "Thai" },
            ["Salad"] = new Tag { Id = 13, Name = "Salad" },
            ["Light"] = new Tag { Id = 14, Name = "Light" },
            ["Dessert"] = new Tag { Id = 15, Name = "Dessert" },
            ["Chocolate"] = new Tag { Id = 16, Name = "Chocolate" },
            ["Baking"] = new Tag { Id = 17, Name = "Baking" },
            ["Soup"] = new Tag { Id = 18, Name = "Soup" },
            ["Greek"] = new Tag { Id = 19, Name = "Greek" },
            ["Baked"] = new Tag { Id = 20, Name = "Baked" },
            ["Seafood"] = new Tag { Id = 21, Name = "Seafood" },
            ["Healthy"] = new Tag { Id = 22, Name = "Healthy" },
            ["Asian"] = new Tag { Id = 23, Name = "Asian" },
            ["Mexican"] = new Tag { Id = 24, Name = "Mexican" },
            ["Family-Friendly"] = new Tag { Id = 25, Name = "Family-Friendly" },
            ["Rice"] = new Tag { Id = 26, Name = "Rice" },
            ["British"] = new Tag { Id = 27, Name = "British" },
            ["American"] = new Tag { Id = 28, Name = "American" },
            ["BBQ"] = new Tag { Id = 29, Name = "BBQ" },
            ["Slow Cook"] = new Tag { Id = 30, Name = "Slow Cook" },
            ["Japanese"] = new Tag { Id = 31, Name = "Japanese" },
            ["Street Food"] = new Tag { Id = 32, Name = "Street Food" },
            ["Poultry"] = new Tag { Id = 33, Name = "Poultry" },
            ["Middle Eastern"] = new Tag { Id = 34, Name = "Middle Eastern" },
            ["One-Pot"] = new Tag { Id = 35, Name = "One-Pot" },
            ["Stir-Fry"] = new Tag { Id = 36, Name = "Stir-Fry" },
            ["Chinese"] = new Tag { Id = 37, Name = "Chinese" },
            ["Pie"] = new Tag { Id = 38, Name = "Pie" }
        };
    }

    private static Recipe SpaghettiCarbonara(Dictionary<string, Ingredient> ingredients, Dictionary<string, Tag> tags)
    {
        var now = DateTime.UtcNow;

        return new Recipe
        {
            Id = 1,
            Name = "Spaghetti Carbonara",
            Icon = "🍝",
            Description = "A classic Italian pasta dish made with eggs, cheese, pancetta, and black pepper. Rich, creamy, and utterly delicious.",
            PreparationTime = TimeSpan.FromMinutes(10),
            CookingTime = TimeSpan.FromMinutes(15),
            Servings = 4,
            Difficulty = RecipeDifficulty.Easy,
            Notes = "The key is to work quickly when adding the egg mixture to avoid scrambling. Use freshly grated Parmesan for the best flavour.",
            CreatedAt = now,
            Ingredients =
            [
                new RecipeIngredient { Id = 1, Ingredient = ingredients["Spaghetti"], Amount = 400, Unit = "g" },
                new RecipeIngredient { Id = 2, Ingredient = ingredients["Pancetta"], Amount = 200, Unit = "g" },
                new RecipeIngredient { Id = 3, Ingredient = ingredients["Egg"], Amount = 4, Unit = "whole" },
                new RecipeIngredient { Id = 4, Ingredient = ingredients["Parmesan cheese"], Amount = 100, Unit = "g" },
                new RecipeIngredient { Id = 5, Ingredient = ingredients["Black pepper"], Amount = 1, Unit = "tsp" },
            ],
            Instructions =
            [
                new Instruction { Id = 1, Order = 1, Text = "Bring a large pot of salted water to the boil and cook the spaghetti according to package instructions." },
                new Instruction { Id = 2, Order = 2, Text = "Meanwhile, dice the pancetta and fry in a large pan over medium heat until crispy." },
                new Instruction { Id = 3, Order = 3, Text = "In a bowl, whisk together the eggs, grated Parmesan, and plenty of black pepper." },
                new Instruction { Id = 4, Order = 4, Text = "Reserve a cup of pasta water, then drain the spaghetti." },
                new Instruction { Id = 5, Order = 5, Text = "Add the hot pasta to the pancetta pan, remove from heat, and quickly stir in the egg mixture, adding pasta water as needed to create a creamy sauce." },
                new Instruction { Id = 6, Order = 6, Text = "Serve immediately with extra Parmesan and black pepper." },
            ],
            Tags = [tags["Italian"], tags["Pasta"], tags["Quick & Easy"]],
        };
    }

    private static Recipe ClassicMargheritaPizza(Dictionary<string, Ingredient> ingredients, Dictionary<string, Tag> tags)
    {
        var now = DateTime.UtcNow;

        return new Recipe
        {
            Id = 2,
            Name = "Classic Margherita Pizza",
            Icon = "🍕",
            Description = "A traditional Neapolitan pizza topped with tomato sauce, fresh mozzarella, basil, and olive oil.",
            PreparationTime = TimeSpan.FromHours(2).Add(TimeSpan.FromMinutes(30)),
            CookingTime = TimeSpan.FromMinutes(12),
            Servings = 4,
            Difficulty = RecipeDifficulty.Medium,
            Notes = "Allow the dough to rise properly for the best texture. A pizza stone helps achieve a crispy base.",
            CreatedAt = now,
            Ingredients =
            [
                new RecipeIngredient { Id = 7, Ingredient = ingredients["Pizza dough"], Amount = 500, Unit = "g" },
                new RecipeIngredient { Id = 8, Ingredient = ingredients["Tomato passata"], Amount = 200, Unit = "ml" },
                new RecipeIngredient { Id = 9, Ingredient = ingredients["Fresh mozzarella"], Amount = 250, Unit = "g" },
                new RecipeIngredient { Id = 10, Ingredient = ingredients["Fresh basil"], Amount = 1, Unit = "bunch" },
                new RecipeIngredient { Id = 11, Ingredient = ingredients["Olive oil"], Amount = 2, Unit = "tbsp" },
            ],
            Instructions =
            [
                new Instruction { Id = 7, Order = 1, Text = "Preheat your oven to the highest temperature (usually 250°C/480°F) with a pizza stone if you have one." },
                new Instruction { Id = 8, Order = 2, Text = "Divide the dough into 4 portions and roll each into a thin circle." },
                new Instruction { Id = 9, Order = 3, Text = "Spread tomato passata evenly over each base, leaving a border for the crust." },
                new Instruction { Id = 10, Order = 4, Text = "Tear the mozzarella and distribute over the pizzas." },
                new Instruction { Id = 11, Order = 5, Text = "Bake for 10-12 minutes until the crust is golden and cheese is bubbling." },
                new Instruction { Id = 12, Order = 6, Text = "Top with fresh basil leaves and a drizzle of olive oil before serving." },
            ],
            Tags = [tags["Italian"], tags["Comfort Food"], tags["American"]],
        };
    }

    private static Recipe ChickenTikkaMasala(Dictionary<string, Ingredient> ingredients, Dictionary<string, Tag> tags)
    {
        var now = DateTime.UtcNow;

        return new Recipe
        {
            Id = 3,
            Name = "Chicken Tikka Masala",
            Icon = "🍛",
            Description = "Tender chicken pieces in a rich, creamy tomato-based sauce with aromatic Indian spices.",
            PreparationTime = TimeSpan.FromMinutes(30),
            CookingTime = TimeSpan.FromMinutes(45),
            Servings = 6,
            Difficulty = RecipeDifficulty.Medium,
            Notes = "Marinating the chicken overnight intensifies the flavour. Serve with basmati rice and naan bread.",
            CreatedAt = now,
            Ingredients =
            [
                new RecipeIngredient { Id = 12, Ingredient = ingredients["Chicken breast"], Amount = 800, Unit = "g" },
                new RecipeIngredient { Id = 13, Ingredient = ingredients["Natural yoghurt"], Amount = 200, Unit = "ml" },
                new RecipeIngredient { Id = 14, Ingredient = ingredients["Garam masala"], Amount = 2, Unit = "tbsp" },
                new RecipeIngredient { Id = 15, Ingredient = ingredients["Tinned tomatoes"], Amount = 400, Unit = "g" },
                new RecipeIngredient { Id = 16, Ingredient = ingredients["Double cream"], Amount = 150, Unit = "ml" },
                new RecipeIngredient { Id = 17, Ingredient = ingredients["Onion"], Amount = 2, Unit = "whole" },
            ],
            Instructions =
            [
                new Instruction { Id = 13, Order = 1, Text = "Cut chicken into bite-sized pieces and marinate in yoghurt and half the garam masala for at least 2 hours." },
                new Instruction { Id = 14, Order = 2, Text = "Grill or fry the marinated chicken until golden and set aside." },
                new Instruction { Id = 15, Order = 3, Text = "Finely chop onions and fry until softened, then add remaining garam masala and cook for 1 minute." },
                new Instruction { Id = 16, Order = 4, Text = "Add tinned tomatoes and simmer for 15 minutes until thickened." },
                new Instruction { Id = 17, Order = 5, Text = "Stir in the cream and cooked chicken, simmer for another 10 minutes." },
                new Instruction { Id = 18, Order = 6, Text = "Garnish with fresh coriander and serve hot with rice or naan." },
            ],
            Tags = [tags["BBQ"], tags["Slow Cook"], tags["Indian"]],
        };
    }

    private static Recipe BeefBourguignon(Dictionary<string, Ingredient> ingredients, Dictionary<string, Tag> tags)
    {
        var now = DateTime.UtcNow;

        return new Recipe
        {
            Id = 4,
            Name = "Beef Bourguignon",
            Icon = "🥘",
            Description = "A French classic: slow-cooked beef stew with red wine, pearl onions, mushrooms, and bacon.",
            PreparationTime = TimeSpan.FromMinutes(30),
            CookingTime = TimeSpan.FromHours(3),
            Servings = 6,
            Difficulty = RecipeDifficulty.Hard,
            Notes = "This dish improves with time - make it a day ahead for even better flavour. Use a good quality red wine.",
            CreatedAt = now,
            Ingredients =
            [
                new RecipeIngredient { Id = 18, Ingredient = ingredients["Beef chuck"], Amount = 1200, Unit = "g" },
                new RecipeIngredient { Id = 19, Ingredient = ingredients["Red wine"], Amount = 750, Unit = "ml" },
                new RecipeIngredient { Id = 20, Ingredient = ingredients["Bacon lardons"], Amount = 200, Unit = "g" },
                new RecipeIngredient { Id = 21, Ingredient = ingredients["Button mushrooms"], Amount = 300, Unit = "g" },
                new RecipeIngredient { Id = 22, Ingredient = ingredients["Pearl onions"], Amount = 250, Unit = "g" },
            ],
            Instructions =
            [
                new Instruction { Id = 19, Order = 1, Text = "Cut beef into large chunks and season generously with salt and pepper." },
                new Instruction { Id = 20, Order = 2, Text = "Brown the beef in batches in a large casserole dish, then set aside." },
                new Instruction { Id = 21, Order = 3, Text = "Fry the bacon lardons until crispy, then add pearl onions and mushrooms." },
                new Instruction { Id = 22, Order = 4, Text = "Return the beef to the pot, add red wine and enough stock to cover." },
                new Instruction { Id = 23, Order = 5, Text = "Cover and cook in a low oven (160°C/320°F) for 2.5-3 hours until the beef is tender." },
                new Instruction { Id = 24, Order = 6, Text = "Serve with creamy mashed potatoes or crusty bread." },
            ],
            Tags = [tags["Spicy"], tags["Curry"], tags["Japanese"]],
        };
    }

    private static Recipe ThaiGreenCurry(Dictionary<string, Ingredient> ingredients, Dictionary<string, Tag> tags)
    {
        var now = DateTime.UtcNow;

        return new Recipe
        {
            Id = 5,
            Name = "Thai Green Curry",
            Icon = "🍲",
            Description = "A fragrant and spicy Thai curry with chicken, vegetables, and coconut milk.",
            PreparationTime = TimeSpan.FromMinutes(15),
            CookingTime = TimeSpan.FromMinutes(25),
            Servings = 4,
            Difficulty = RecipeDifficulty.Easy,
            Notes = "Adjust the amount of curry paste to your preferred spice level. Add Thai basil at the end for authentic flavour.",
            CreatedAt = now,
            Ingredients =
            [
                new RecipeIngredient { Id = 23, Ingredient = ingredients["Chicken breast"], Amount = 500, Unit = "g" },
                new RecipeIngredient { Id = 24, Ingredient = ingredients["Green curry paste"], Amount = 3, Unit = "tbsp" },
                new RecipeIngredient { Id = 25, Ingredient = ingredients["Coconut milk"], Amount = 400, Unit = "ml" },
                new RecipeIngredient { Id = 26, Ingredient = ingredients["Green beans"], Amount = 200, Unit = "g" },
                new RecipeIngredient { Id = 27, Ingredient = ingredients["Red bell pepper"], Amount = 1, Unit = "whole" },
            ],
            Instructions =
            [
                new Instruction { Id = 25, Order = 1, Text = "Heat a little oil in a wok or large pan and fry the curry paste for 1 minute." },
                new Instruction { Id = 26, Order = 2, Text = "Add sliced chicken and stir-fry until no longer pink." },
                new Instruction { Id = 27, Order = 3, Text = "Pour in the coconut milk and bring to a gentle simmer." },
                new Instruction { Id = 28, Order = 4, Text = "Add green beans and sliced peppers, cook for 8-10 minutes until vegetables are tender." },
                new Instruction { Id = 29, Order = 5, Text = "Season with fish sauce and palm sugar to taste." },
                new Instruction { Id = 30, Order = 6, Text = "Serve with jasmine rice and garnish with Thai basil leaves." },
            ],
            Tags = [tags["Seafood"], tags["Slow Cook"], tags["Quick & Easy"]],
        };
    }

    private static Recipe ClassicCaesarSalad(Dictionary<string, Ingredient> ingredients, Dictionary<string, Tag> tags)
    {
        var now = DateTime.UtcNow;

        return new Recipe
        {
            Id = 6,
            Name = "Classic Caesar Salad",
            Icon = "🥗",
            Description = "Crisp romaine lettuce with creamy Caesar dressing, Parmesan shavings, and crunchy croutons.",
            PreparationTime = TimeSpan.FromMinutes(15),
            CookingTime = TimeSpan.FromMinutes(10),
            Servings = 4,
            Difficulty = RecipeDifficulty.Easy,
            Notes = "Make your own croutons by toasting cubed bread with olive oil and garlic. Add grilled chicken for a heartier meal.",
            CreatedAt = now,
            Ingredients =
            [
                new RecipeIngredient { Id = 28, Ingredient = ingredients["Romaine lettuce"], Amount = 2, Unit = "heads" },
                new RecipeIngredient { Id = 29, Ingredient = ingredients["Parmesan cheese"], Amount = 50, Unit = "g" },
                new RecipeIngredient { Id = 30, Ingredient = ingredients["Croutons"], Amount = 100, Unit = "g" },
                new RecipeIngredient { Id = 31, Ingredient = ingredients["Mayonnaise"], Amount = 100, Unit = "ml" },
                new RecipeIngredient { Id = 32, Ingredient = ingredients["Anchovy fillets"], Amount = 4, Unit = "whole" },
            ],
            Instructions =
            [
                new Instruction { Id = 31, Order = 1, Text = "Wash and dry the romaine lettuce, then tear into bite-sized pieces." },
                new Instruction { Id = 32, Order = 2, Text = "Make the dressing by blending mayonnaise, anchovies, garlic, lemon juice, and half the Parmesan." },
                new Instruction { Id = 33, Order = 3, Text = "Toss the lettuce with the dressing until well coated." },
                new Instruction { Id = 34, Order = 4, Text = "Add croutons and toss gently." },
                new Instruction { Id = 35, Order = 5, Text = "Serve topped with Parmesan shavings and extra black pepper." },
            ],
            Tags = [tags["Healthy"], tags["Mexican"], tags["Quick & Easy"]],
        };
    }

    private static Recipe ChocolateBrownies(Dictionary<string, Ingredient> ingredients, Dictionary<string, Tag> tags)
    {
        var now = DateTime.UtcNow;

        return new Recipe
        {
            Id = 7,
            Name = "Chocolate Brownies",
            Icon = "🍫",
            Description = "Rich, fudgy chocolate brownies with a crackly top and gooey centre.",
            PreparationTime = TimeSpan.FromMinutes(15),
            CookingTime = TimeSpan.FromMinutes(30),
            Servings = 12,
            Difficulty = RecipeDifficulty.Easy,
            Notes = "Don't overbake - they should still be slightly wobbly in the centre. Add walnuts or chocolate chips for extra texture.",
            CreatedAt = now,
            Ingredients =
            [
                new RecipeIngredient { Id = 33, Ingredient = ingredients["Dark chocolate"], Amount = 200, Unit = "g" },
                new RecipeIngredient { Id = 34, Ingredient = ingredients["Butter"], Amount = 175, Unit = "g" },
                new RecipeIngredient { Id = 35, Ingredient = ingredients["Caster sugar"], Amount = 250, Unit = "g" },
                new RecipeIngredient { Id = 36, Ingredient = ingredients["Egg"], Amount = 3, Unit = "whole" },
                new RecipeIngredient { Id = 37, Ingredient = ingredients["Plain flour"], Amount = 75, Unit = "g" },
            ],
            Instructions =
            [
                new Instruction { Id = 36, Order = 1, Text = "Preheat the oven to 180°C/350°F and line a 20cm square tin with baking parchment." },
                new Instruction { Id = 37, Order = 2, Text = "Melt the chocolate and butter together in a bowl over simmering water." },
                new Instruction { Id = 38, Order = 3, Text = "Remove from heat and stir in the sugar, then beat in the eggs one at a time." },
                new Instruction { Id = 39, Order = 4, Text = "Fold in the flour until just combined - don't overmix." },
                new Instruction { Id = 40, Order = 5, Text = "Pour into the prepared tin and bake for 25-30 minutes." },
                new Instruction { Id = 41, Order = 6, Text = "Allow to cool completely before cutting into squares." },
            ],
            Tags = [tags["Vegetarian"], tags["Street Food"], tags["French"]],
        };
    }

    private static Recipe TomatoBasilSoup(Dictionary<string, Ingredient> ingredients, Dictionary<string, Tag> tags)
    {
        var now = DateTime.UtcNow;

        return new Recipe
        {
            Id = 8,
            Name = "Tomato & Basil Soup",
            Icon = "🍅",
            Description = "A comforting, velvety smooth tomato soup with fresh basil. Perfect with crusty bread.",
            PreparationTime = TimeSpan.FromMinutes(10),
            CookingTime = TimeSpan.FromMinutes(30),
            Servings = 4,
            Difficulty = RecipeDifficulty.Easy,
            Notes = "Use ripe tomatoes for the best flavour, or good quality tinned tomatoes work well too.",
            CreatedAt = now,
            Ingredients =
            [
                new RecipeIngredient { Id = 38, Ingredient = ingredients["Ripe tomatoes"], Amount = 1000, Unit = "g" },
                new RecipeIngredient { Id = 39, Ingredient = ingredients["Onion"], Amount = 1, Unit = "whole" },
                new RecipeIngredient { Id = 40, Ingredient = ingredients["Garlic clove"], Amount = 3, Unit = "whole" },
                new RecipeIngredient { Id = 41, Ingredient = ingredients["Fresh basil"], Amount = 1, Unit = "bunch" },
                new RecipeIngredient { Id = 42, Ingredient = ingredients["Vegetable stock"], Amount = 500, Unit = "ml" },
            ],
            Instructions =
            [
                new Instruction { Id = 42, Order = 1, Text = "Roughly chop the tomatoes, onion, and garlic." },
                new Instruction { Id = 43, Order = 2, Text = "Heat olive oil in a large pot and sauté the onion and garlic until softened." },
                new Instruction { Id = 44, Order = 3, Text = "Add the tomatoes and cook for 10 minutes until they start breaking down." },
                new Instruction { Id = 45, Order = 4, Text = "Pour in the vegetable stock and simmer for 15 minutes." },
                new Instruction { Id = 46, Order = 5, Text = "Add most of the basil leaves and blend until smooth." },
                new Instruction { Id = 47, Order = 6, Text = "Season to taste and serve with a swirl of cream and fresh basil leaves." },
            ],
            Tags = [tags["Baked"], tags["American"], tags["Japanese"]],
        };
    }

    private static Recipe GreekMoussaka(Dictionary<string, Ingredient> ingredients, Dictionary<string, Tag> tags)
    {
        var now = DateTime.UtcNow;

        return new Recipe
        {
            Id = 9,
            Name = "Greek Moussaka",
            Icon = "🇬🇷",
            Description = "Layers of aubergine, spiced lamb mince, and creamy béchamel sauce baked until golden.",
            PreparationTime = TimeSpan.FromMinutes(40),
            CookingTime = TimeSpan.FromHours(1),
            Servings = 6,
            Difficulty = RecipeDifficulty.Hard,
            Notes = "Salting the aubergine slices beforehand removes bitterness and excess moisture.",
            CreatedAt = now,
            Ingredients =
            [
                new RecipeIngredient { Id = 43, Ingredient = ingredients["Aubergine"], Amount = 3, Unit = "whole" },
                new RecipeIngredient { Id = 44, Ingredient = ingredients["Lamb mince"], Amount = 500, Unit = "g" },
                new RecipeIngredient { Id = 45, Ingredient = ingredients["Tinned tomatoes"], Amount = 400, Unit = "g" },
                new RecipeIngredient { Id = 46, Ingredient = ingredients["Milk"], Amount = 600, Unit = "ml" },
                new RecipeIngredient { Id = 47, Ingredient = ingredients["Plain flour"], Amount = 50, Unit = "g" },
            ],
            Instructions =
            [
                new Instruction { Id = 48, Order = 1, Text = "Slice aubergines, salt them, and leave for 30 minutes. Rinse and pat dry." },
                new Instruction { Id = 49, Order = 2, Text = "Fry the aubergine slices in olive oil until golden, then set aside." },
                new Instruction { Id = 50, Order = 3, Text = "Brown the lamb mince with onions, add tomatoes and simmer for 20 minutes." },
                new Instruction { Id = 51, Order = 4, Text = "Make béchamel sauce by melting butter, adding flour, then gradually whisking in milk until thick." },
                new Instruction { Id = 52, Order = 5, Text = "Layer aubergine and meat sauce in a baking dish, finishing with aubergine. Pour béchamel on top." },
                new Instruction { Id = 53, Order = 6, Text = "Bake at 180°C/350°F for 45 minutes until golden and bubbling." },
            ],
            Tags = [tags["Poultry"], tags["Middle Eastern"], tags["Japanese"]],
        };
    }

    private static Recipe PanSearedSalmon(Dictionary<string, Ingredient> ingredients, Dictionary<string, Tag> tags)
    {
        var now = DateTime.UtcNow;

        return new Recipe
        {
            Id = 10,
            Name = "Pan-Seared Salmon",
            Icon = "🐟",
            Description = "Crispy-skinned salmon fillets with a tender, flaky interior. Simple yet elegant.",
            PreparationTime = TimeSpan.FromMinutes(5),
            CookingTime = TimeSpan.FromMinutes(12),
            Servings = 4,
            Difficulty = RecipeDifficulty.Easy,
            Notes = "Pat the salmon dry before cooking for the crispiest skin. Don't move it around in the pan.",
            CreatedAt = now,
            Ingredients =
            [
                new RecipeIngredient { Id = 48, Ingredient = ingredients["Salmon fillet"], Amount = 4, Unit = "pieces" },
                new RecipeIngredient { Id = 49, Ingredient = ingredients["Olive oil"], Amount = 2, Unit = "tbsp" },
                new RecipeIngredient { Id = 50, Ingredient = ingredients["Lemon"], Amount = 1, Unit = "whole" },
                new RecipeIngredient { Id = 51, Ingredient = ingredients["Butter"], Amount = 30, Unit = "g" },
            ],
            Instructions =
            [
                new Instruction { Id = 54, Order = 1, Text = "Pat the salmon fillets dry with kitchen paper and season generously with salt and pepper." },
                new Instruction { Id = 55, Order = 2, Text = "Heat olive oil in a non-stick pan over medium-high heat." },
                new Instruction { Id = 56, Order = 3, Text = "Place salmon skin-side down and press gently for 30 seconds to prevent curling." },
                new Instruction { Id = 57, Order = 4, Text = "Cook for 6-7 minutes without moving, until the skin is crispy." },
                new Instruction { Id = 58, Order = 5, Text = "Flip and cook for another 2-3 minutes, adding butter and lemon juice to the pan." },
                new Instruction { Id = 59, Order = 6, Text = "Baste the salmon with the buttery pan juices and serve immediately." },
            ],
            Tags = [tags["One-Pot"], tags["Thai"], tags["Quick & Easy"]],
        };
    }

    private static Recipe VegetableStirFry(Dictionary<string, Ingredient> ingredients, Dictionary<string, Tag> tags)
    {
        var now = DateTime.UtcNow;

        return new Recipe
        {
            Id = 11,
            Name = "Vegetable Stir-Fry",
            Icon = "🥦",
            Description = "A colourful mix of crisp vegetables in a savoury Asian sauce. Quick, healthy, and delicious.",
            PreparationTime = TimeSpan.FromMinutes(15),
            CookingTime = TimeSpan.FromMinutes(10),
            Servings = 4,
            Difficulty = RecipeDifficulty.Easy,
            Notes = "The key to a great stir-fry is high heat and quick cooking. Prep all ingredients before you start.",
            CreatedAt = now,
            Ingredients =
            [
                new RecipeIngredient { Id = 52, Ingredient = ingredients["Broccoli florets"], Amount = 200, Unit = "g" },
                new RecipeIngredient { Id = 53, Ingredient = ingredients["Red bell pepper"], Amount = 1, Unit = "whole" },
                new RecipeIngredient { Id = 54, Ingredient = ingredients["Carrot"], Amount = 2, Unit = "whole" },
                new RecipeIngredient { Id = 55, Ingredient = ingredients["Soy sauce"], Amount = 3, Unit = "tbsp" },
                new RecipeIngredient { Id = 56, Ingredient = ingredients["Ginger"], Amount = 1, Unit = "inch" },
            ],
            Instructions =
            [
                new Instruction { Id = 60, Order = 1, Text = "Prepare all vegetables: slice peppers, cut broccoli into florets, julienne carrots." },
                new Instruction { Id = 61, Order = 2, Text = "Heat oil in a wok or large frying pan until smoking hot." },
                new Instruction { Id = 62, Order = 3, Text = "Add ginger and garlic, stir-fry for 30 seconds." },
                new Instruction { Id = 63, Order = 4, Text = "Add the hardest vegetables first (carrots, broccoli), stir-fry for 3 minutes." },
                new Instruction { Id = 64, Order = 5, Text = "Add softer vegetables (peppers), stir-fry for another 2 minutes." },
                new Instruction { Id = 65, Order = 6, Text = "Pour in soy sauce and toss everything together. Serve immediately with rice or noodles." },
            ],
            Tags = [tags["Stir-Fry"], tags["American"], tags["Thai"]],
        };
    }

    private static Recipe ClassicLasagne(Dictionary<string, Ingredient> ingredients, Dictionary<string, Tag> tags)
    {
        var now = DateTime.UtcNow;

        return new Recipe
        {
            Id = 12,
            Name = "Classic Lasagne",
            Icon = "🧀",
            Description = "Layers of pasta, rich meat sauce, and creamy béchamel, baked until bubbling and golden.",
            PreparationTime = TimeSpan.FromMinutes(30),
            CookingTime = TimeSpan.FromMinutes(60),
            Servings = 8,
            Difficulty = RecipeDifficulty.Medium,
            Notes = "Let it rest for 10 minutes after baking for cleaner slices. Freezes beautifully.",
            CreatedAt = now,
            Ingredients =
            [
                new RecipeIngredient { Id = 57, Ingredient = ingredients["Lasagne sheets"], Amount = 12, Unit = "sheets" },
                new RecipeIngredient { Id = 58, Ingredient = ingredients["Beef mince"], Amount = 500, Unit = "g" },
                new RecipeIngredient { Id = 59, Ingredient = ingredients["Tinned tomatoes"], Amount = 800, Unit = "g" },
                new RecipeIngredient { Id = 60, Ingredient = ingredients["Milk"], Amount = 600, Unit = "ml" },
                new RecipeIngredient { Id = 61, Ingredient = ingredients["Parmesan cheese"], Amount = 100, Unit = "g" },
            ],
            Instructions =
            [
                new Instruction { Id = 66, Order = 1, Text = "Brown the beef mince with diced onions, add tinned tomatoes and simmer for 30 minutes." },
                new Instruction { Id = 67, Order = 2, Text = "Make béchamel sauce by melting butter, adding flour, then gradually whisking in milk." },
                new Instruction { Id = 68, Order = 3, Text = "In a baking dish, layer meat sauce, lasagne sheets, and béchamel. Repeat 3-4 times." },
                new Instruction { Id = 69, Order = 4, Text = "Finish with béchamel and sprinkle generously with Parmesan." },
                new Instruction { Id = 70, Order = 5, Text = "Bake at 180°C/350°F for 40-45 minutes until golden and bubbling." },
                new Instruction { Id = 71, Order = 6, Text = "Let rest for 10 minutes before slicing and serving." },
            ],
            Tags = [tags["Italian"], tags["Pasta"], tags["Middle Eastern"]],
        };
    }

    private static Recipe ChickenFajitas(Dictionary<string, Ingredient> ingredients, Dictionary<string, Tag> tags)
    {
        var now = DateTime.UtcNow;

        return new Recipe
        {
            Id = 13,
            Name = "Chicken Fajitas",
            Icon = "🌮",
            Description = "Sizzling strips of spiced chicken with peppers and onions, wrapped in warm tortillas.",
            PreparationTime = TimeSpan.FromMinutes(15),
            CookingTime = TimeSpan.FromMinutes(15),
            Servings = 4,
            Difficulty = RecipeDifficulty.Easy,
            Notes = "Serve with sour cream, guacamole, and grated cheese for the full experience.",
            CreatedAt = now,
            Ingredients =
            [
                new RecipeIngredient { Id = 62, Ingredient = ingredients["Chicken breast"], Amount = 600, Unit = "g" },
                new RecipeIngredient { Id = 63, Ingredient = ingredients["Red bell pepper"], Amount = 2, Unit = "whole" },
                new RecipeIngredient { Id = 64, Ingredient = ingredients["Onion"], Amount = 2, Unit = "whole" },
                new RecipeIngredient { Id = 65, Ingredient = ingredients["Fajita seasoning"], Amount = 3, Unit = "tbsp" },
                new RecipeIngredient { Id = 66, Ingredient = ingredients["Flour tortillas"], Amount = 8, Unit = "whole" },
            ],
            Instructions =
            [
                new Instruction { Id = 72, Order = 1, Text = "Slice chicken into thin strips and toss with fajita seasoning." },
                new Instruction { Id = 73, Order = 2, Text = "Slice peppers and onions into strips." },
                new Instruction { Id = 74, Order = 3, Text = "Heat oil in a large pan and cook chicken strips until golden and cooked through. Set aside." },
                new Instruction { Id = 75, Order = 4, Text = "In the same pan, fry peppers and onions until softened and slightly charred." },
                new Instruction { Id = 76, Order = 5, Text = "Return chicken to the pan and toss everything together." },
                new Instruction { Id = 77, Order = 6, Text = "Warm tortillas and serve the fajita filling with your choice of toppings." },
            ],
            Tags = [tags["Chinese"], tags["Quick & Easy"], tags["British"]],
        };
    }

    private static Recipe MushroomRisotto(Dictionary<string, Ingredient> ingredients, Dictionary<string, Tag> tags)
    {
        var now = DateTime.UtcNow;

        return new Recipe
        {
            Id = 14,
            Name = "Mushroom Risotto",
            Icon = "🍄",
            Description = "Creamy, comforting Italian rice dish with earthy mushrooms and Parmesan.",
            PreparationTime = TimeSpan.FromMinutes(10),
            CookingTime = TimeSpan.FromMinutes(35),
            Servings = 4,
            Difficulty = RecipeDifficulty.Medium,
            Notes = "Constant stirring and patience are key to perfect risotto. Add stock gradually.",
            CreatedAt = now,
            Ingredients =
            [
                new RecipeIngredient { Id = 67, Ingredient = ingredients["Arborio rice"], Amount = 300, Unit = "g" },
                new RecipeIngredient { Id = 68, Ingredient = ingredients["Button mushrooms"], Amount = 400, Unit = "g" },
                new RecipeIngredient { Id = 69, Ingredient = ingredients["Vegetable stock"], Amount = 1200, Unit = "ml" },
                new RecipeIngredient { Id = 70, Ingredient = ingredients["Parmesan cheese"], Amount = 75, Unit = "g" },
                new RecipeIngredient { Id = 71, Ingredient = ingredients["Red wine"], Amount = 150, Unit = "ml" },
            ],
            Instructions =
            [
                new Instruction { Id = 78, Order = 1, Text = "Keep the stock warm in a separate pan." },
                new Instruction { Id = 79, Order = 2, Text = "Fry sliced mushrooms in butter until golden, then set aside." },
                new Instruction { Id = 80, Order = 3, Text = "In the same pan, sauté diced onion until soft, add rice and stir for 1 minute." },
                new Instruction { Id = 81, Order = 4, Text = "Add wine and stir until absorbed." },
                new Instruction { Id = 82, Order = 5, Text = "Add stock one ladle at a time, stirring constantly until absorbed before adding more. This takes about 20-25 minutes." },
                new Instruction { Id = 83, Order = 6, Text = "Stir in mushrooms, butter, and Parmesan. Season and serve immediately." },
            ],
            Tags = [tags["Italian"], tags["American"], tags["Pie"]],
        };
    }

    private static Recipe ShepherdsPie(Dictionary<string, Ingredient> ingredients, Dictionary<string, Tag> tags)
    {
        var now = DateTime.UtcNow;

        return new Recipe
        {
            Id = 15,
            Name = "Shepherd's Pie",
            Icon = "🥧",
            Description = "A British classic with savoury lamb mince topped with fluffy mashed potato and baked until golden.",
            PreparationTime = TimeSpan.FromMinutes(20),
            CookingTime = TimeSpan.FromMinutes(40),
            Servings = 6,
            Difficulty = RecipeDifficulty.Easy,
            Notes = "Traditionally made with lamb (cottage pie uses beef). Great for using up leftover mash.",
            CreatedAt = now,
            Ingredients =
            [
                new RecipeIngredient { Id = 72, Ingredient = ingredients["Lamb mince"], Amount = 750, Unit = "g" },
                new RecipeIngredient { Id = 73, Ingredient = ingredients["Potatoes"], Amount = 1000, Unit = "g" },
                new RecipeIngredient { Id = 74, Ingredient = ingredients["Carrot"], Amount = 2, Unit = "whole" },
                new RecipeIngredient { Id = 75, Ingredient = ingredients["Peas"], Amount = 150, Unit = "g" },
                new RecipeIngredient { Id = 76, Ingredient = ingredients["Worcestershire sauce"], Amount = 2, Unit = "tbsp" },
            ],
            Instructions =
            [
                new Instruction { Id = 84, Order = 1, Text = "Boil peeled potatoes until tender, drain, and mash with butter and milk until smooth." },
                new Instruction { Id = 85, Order = 2, Text = "Brown the lamb mince with diced onions and carrots." },
                new Instruction { Id = 86, Order = 3, Text = "Add stock, Worcestershire sauce, and peas. Simmer for 15 minutes until thickened." },
                new Instruction { Id = 87, Order = 4, Text = "Transfer the mince to a baking dish and spread the mashed potato evenly on top." },
                new Instruction { Id = 88, Order = 5, Text = "Use a fork to create texture on the potato surface." },
                new Instruction { Id = 89, Order = 6, Text = "Bake at 200°C/400°F for 25-30 minutes until the top is golden and crispy." },
            ],
            Tags = [tags["Greek"], tags["Japanese"], tags["British"]],
        };
    }
}
