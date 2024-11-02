using System.Runtime.Serialization.Formatters;


namespace CookBook
{
    public static class CategoryManager
    {
        // Visa alla kategorier
        public static void ShowCategories()
        {
            var recipeList = Program.GetRecipeList();
            Console.Clear();
            ConsoleStyle.PrintColor("ALLA KATEGORIER\n", ConsoleColor.Yellow);

            // Hämta kategorier med Linq-metoder
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
            var recipeList = Program.GetRecipeList(); // hämta listan
            Console.Clear();
            ConsoleStyle.PrintColor("\nAnge siffran för det recept du vill se. \n", ConsoleColor.DarkYellow);
            ConsoleStyle.PrintColor("Tryck på X för att gå tillbaka till alla kategorier.\n", ConsoleColor.DarkGray);

            // Filtrera ur recept från vald kategori med Linq-metoder
            List<Recipe> fromCategory = recipeList
            .Where(r => r.Category.Contains(category)) // Hämta recept i vald kategori
            .ToList(); // Lägg i lista

            // Loopa igenom och visa recept i vald kategori
            for (int i = 0; i < fromCategory.Count; i++)
            {
                ConsoleStyle.PrintColor($"[{i}] {fromCategory[i].Name}\n", ConsoleColor.Yellow);
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

                // Kontrollera input och parsa inmatning
                if (int.TryParse(input, out int recipeIndex) && recipeIndex >= 0 && recipeIndex < fromCategory.Count)
                {
                    RecipeManager.ShowSingleRecipe(recipeIndex); // visa valt recept
                    break;
                }
                else
                {
                    ConsoleStyle.PrintColor("Ogiltigt val, försök igen.\n", ConsoleColor.DarkRed);
                }
            }

        }

    }
}
