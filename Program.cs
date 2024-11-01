using System.Text.Json;
using Recipes; // Importerar recipe-klass från recipes.cs
using ConsoleStyling; // Importerar styling-klass från ConsoleStyling.cs

namespace CookBook
{

     class Program
    {

        // Skapa lista med JSON
        private static List<Recipe> recipeList = new List<Recipe>(); // List of things
        private static string filename = "cookbook.json"; // Filnamn

        // Metod för att kunna hämta listan
        public static List<Recipe> GetRecipeList()
        {
            return recipeList; // Returnera lista
        }


        // Meny
        public static void DisplayMenu()
        {
            ConsoleStyle.PrintColor("\nVälkommen till kokboken!", ConsoleColor.DarkBlue);

            ConsoleStyle.PrintColor("\n1 - Visa alla recept", ConsoleColor.Yellow);
            ConsoleStyle.PrintColor("2 - Visa alla kategorier", ConsoleColor.Yellow);
            ConsoleStyle.PrintColor("3 - Lägg till nytt recept", ConsoleColor.Yellow);
            ConsoleStyle.PrintColor("4 - Slumpa fram ett recept\n", ConsoleColor.Yellow);

            ConsoleStyle.PrintColor("5 - Ta bort ett recept\n", ConsoleColor.Red);

            ConsoleStyle.PrintColor("X - Stäng kokboken\n", ConsoleColor.DarkGray);

            char option = Console.ReadKey(true).KeyChar;

            // Switch-sats för menynvalen
            switch (option)
            {
                case '1':
                    RecipeManager.ShowAllRecipes(); // Visa alla recept
                    break;

                case '2':
                    CategoryManager.ShowCategories(); // Visa kategorier
                    break;

                case '3':
                    RecipeManager.AddNewRecipe(); // Lägg till nytt recept
                    break;

                case '4':
                    RecipeManager.RandomRecipe(); // Slumpa fram recept
                    break;

                case '5':
                    RecipeManager.RemoveRecipe(); // Ta bort recept
                    break;

                case 'x':
                case 'X':
                    Quit();
                    break;

                // Vid felaktig inmatning
                default:
                    Console.Clear();
                    ConsoleStyle.PrintColor("Ogiltigt val, vänligen försök igen.\n", ConsoleColor.DarkRed);
                    DisplayMenu();
                    break;
            }
        }

        // Spara till JSON-fil
        public static void SaveRecipes()
        {
            string jsonString = JsonSerializer.Serialize(recipeList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filename, jsonString);
        }

        // Läs in recept från fil
        public static void LoadRecipes()
        {
            if (File.Exists(filename))
            {
                string jsonString = File.ReadAllText(filename);
                recipeList = JsonSerializer.Deserialize<List<Recipe>>(jsonString) ?? new List<Recipe>();
            }
        }

        // Avsluta
        public static void Quit()
        {
            Console.Clear();
            ConsoleStyle.PrintBold("\nHAPPY COOKING :) \n");
            Environment.Exit(0); // Avsluta programmet
        }

        static void Main(string[] args)
        {

            // läs in befintliga från fil
            LoadRecipes();

            // Visa menyn
            DisplayMenu();
        }


    }
}