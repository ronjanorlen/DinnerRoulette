using ConsoleStyling; // Importera styling-klass från ConsoleStyle
using Recipes; // Importerar recipe-klass från recipes.cs

namespace CookBook
{
    public static class CategoryManager
    {
        // Visa alla kategorier
        public static void ShowCategories()
        {
            var recipeList = Program.GetRecipeList();
            Console.Clear();
            ConsoleStyle.PrintColor("Alla kategorier\n", ConsoleColor.Yellow);

            // Hämta kategorier
            List<string> allCategories = recipeList
                .SelectMany(r => r.Category) // Ta alla kategorier från recepten
                .Distinct() // Ta bort ev. dubletter
                .ToList(); // Lägg katgorierna i en lista

            // Om det inte finns några kategorier
            if (allCategories.Count == 0)
            {
                ConsoleStyle.PrintColor("Det finns inga kategorier att visa..\n", ConsoleColor.DarkRed);
                ConsoleStyle.PrintColor("\nTryck på valfri tangent för att återgå till huvudmenyn.\n", ConsoleColor.DarkGray);
                Console.ReadKey();
                Console.Clear();
                Program.DisplayMenu();
            }

            while (true)
            {

                ConsoleStyle.PrintColor("\nAnge siffran för den kategori du vill se. \n", ConsoleColor.DarkYellow);
                ConsoleStyle.PrintColor("Tryck på X för att gå tillbaka till huvudmenyn.\n", ConsoleColor.DarkGray);

                // Loopa igenom och visa alla kategorier
                for (int i = 0; i < allCategories.Count; i++)
                {
                    ConsoleStyle.PrintColor($"{i + 1}: {allCategories[i]}\n", ConsoleColor.Yellow);
                }

                string? input = Console.ReadLine();
                if (input?.ToUpper() == "X")
                {
                    Console.Clear();
                    Program.DisplayMenu();
                    return;
                }

                // Välj kategori
                // Parsa inmatning från användare och kontrollera att värdet finns i listan
                if (int.TryParse(input, out int categoryIndex) && categoryIndex > 0 && categoryIndex <= allCategories.Count)
                {
                    // Hämta recept ur vald kategori
                    string selectedCategory = allCategories[categoryIndex - 1];
                    RecipesInCategory(selectedCategory);
                }
                else
                {
                    Console.Clear();
                    ConsoleStyle.PrintColor("Ogiltigt val, försök igen.\n", ConsoleColor.DarkRed);
                }
            }


        }

        // Visa recept via kategorier
        public static void RecipesInCategory(string category)
        {
            var recipeList = Program.GetRecipeList();
            Console.Clear();
            ConsoleStyle.PrintColor("\nAnge siffran för det recept du vill se. \n", ConsoleColor.DarkYellow);
            ConsoleStyle.PrintColor("Tryck på X för att gå tillbaka till alla kategorier.\n", ConsoleColor.DarkGray);

            // Filtrera ur recept från vald kategori
            List<Recipe> fromCategory = recipeList
            .Where(r => r.Category.Contains(category)) // Hämta recept i vald kategori
            .ToList(); // Lägg i lista

            foreach (var recipe in fromCategory) // Loopa igenom och skriv ut recepten
            {
                ConsoleStyle.PrintColor($"{recipe.Id}: {recipe.Name}\n", ConsoleColor.Yellow);
            }

            while (true)
            {
                string? input = Console.ReadLine();

                if (input?.ToUpper() == "X")
                {
                    Console.Clear();
                    ShowCategories();
                    return;
                }

                if (!int.TryParse(input, out int recipeId)) // Om input inte är en siffra
                {
                    ConsoleStyle.PrintColor("Du måste ange en siffra.", ConsoleColor.DarkRed);
                    ConsoleStyle.PrintColor("Testa igen.\n", ConsoleColor.DarkGray);
                    continue;
                }

                // Kontroll att receptet finns i kategorin
                if (fromCategory.Any(r => r.Id == recipeId))
                {
                    RecipeManager.ShowSingleRecipe(recipeId);
                    break;
                }
                else
                {
                    Console.Clear();
                    ConsoleStyle.PrintColor("Siffran du angav matchar inget recept i denna kategori. Vänligen försök igen.\n", ConsoleColor.DarkRed);
                    ConsoleStyle.PrintColor("Välj ett recept ur listan nedan.\n", ConsoleColor.DarkYellow);
                    foreach (var recipe in fromCategory)
                    {
                        ConsoleStyle.PrintColor($"{recipe.Id}: {recipe.Name}\n", ConsoleColor.Yellow);
                    }
                    ConsoleStyle.PrintColor("Eller tryck X för att gå tillbaka till alla kategorier.\n", ConsoleColor.DarkGray);
                }
            }

        }
    }
}