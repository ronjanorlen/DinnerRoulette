using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using Recipes; // Importerar recipe-klass från recipes.cs

class Program
{
    // Skapa lista med JSON
    private static List<Recipe> recipeList = new List<Recipe>(); // List of things
    private static string filename = "cookbook.json"; // Filnamn


    // Meny
    public static void DisplayMenu()
    {
        Console.WriteLine("1 - Visa alla recept");
        Console.WriteLine("2 - Visa alla kategorier");
        Console.WriteLine("3 - Lägg till nytt recept");
        Console.WriteLine("4 - Slumpa fram ett recept");
        Console.WriteLine("5 - Ta bort ett recept\n");
        Console.WriteLine("X - Avsluta\n");

        char option = Console.ReadKey(true).KeyChar;

        if (option == '1')
        {
            ShowAllRecipes(); // Visa alla recept
        }
        else if (option == '2')
        {
            ShowCategories(); // Visa kategorier
        }
        else if (option == '3')
        {
            AddNewRecipe(); // Lägg till nytt recept
        }
        else if (option == '4')
        {
            RandomRecipe(); // Slumpa fram recept
        }
        else if (option == '5')
        {
            RemoveRecipe();
        }
        else if (option == 'X' || option == 'x')
        {
            Quit();
        }
        else
        {
            Console.WriteLine("Ogiltigt val, vänligen försök igen.");
        }
    }

    // Visa alla recept
    public static void ShowAllRecipes()
    {
        Console.Clear();
        Console.WriteLine("KOKBOKEN\n");

        // Om det inte finns några recept
        if (recipeList.Count == 0)
        {
            Console.WriteLine("Det finns inga recept att hämta..");
            return; // Avbryt om det inte finns några recept att hämta
        }

        // Annars loopa igenom listan med recept
        foreach (var recipe in recipeList)
        {
            Console.WriteLine($"{recipe.Id}: {recipe.Name}");
            Console.WriteLine($"Kategori: {recipe.Category}\n");
        }

        // Loop för att välja ett recept
        Console.WriteLine("Ange ID för det recept du vill titta på eller tryck på B för återgå till huvudmenyn.");
        while (true)
        {
            string? input = Console.ReadLine();

            if (input?.ToUpper() == "B")
            {
                // Gå tillbaka till menyn
                Console.Clear();
                DisplayMenu();
            }

            if (int.TryParse(input, out int recipeId))
            {
                // Visa receptet som valts
                ShowSingleRecipe(recipeId);
            }
            else if (!string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Ogiltigt val, försök igen.");
            }
        }


    }

    // Visa valt recept
    public static void ShowSingleRecipe(int recipeId)
    {
        // Hitta recept med angivet ID
        var recipe = recipeList.FirstOrDefault(r => r.Id == recipeId);

        if (recipe != null)
        {
            Console.Clear();
            Console.WriteLine($"{recipe.Name}\n");
            Console.WriteLine($"Ingredienser:\n {recipe.Ingredients}\n");
            Console.WriteLine($"Instruktioner:\n {recipe.Instructions}\n");
            Console.WriteLine($"Kategori:\n {recipe.Category}\n");
        }
        else
        {
            Console.WriteLine("Receptet hittades inte.");
        }

        // Gå tillbaka
        Console.WriteLine("Tryck på B för att gå tillbaka till alla recept.");
        while (true)
        {
            string? input = Console.ReadLine();

            if (input?.ToUpper() == "B")
            {
                // Gå tillbaka till alla recept
                Console.Clear();
                ShowAllRecipes();
            }

            else
            {
                Console.WriteLine("Ogiltigt val, försök igen.");
            }
        }
    }

    // Visa alla kategorier
    public static void ShowCategories()
    {
        Console.WriteLine("Alla kategorier");
    }

    // Lägg till recept
    public static void AddNewRecipe()
    {
        Console.Clear();

        Recipe newRecipe = new Recipe(); // Nytt objekt

        // Generera ID till recept baserat på listan
        newRecipe.Id = recipeList.Count + 1;


        Console.WriteLine("Ange namn för recept:");
        newRecipe.Name = Console.ReadLine();

        Console.WriteLine("Ange ingredienser:");
        newRecipe.Ingredients = Console.ReadLine();

        Console.WriteLine("Ange instruktioner:");
        newRecipe.Instructions = Console.ReadLine();

        Console.WriteLine("Ange kategori för receptet:");
        newRecipe.Category = Console.ReadLine();

        // Lägg till recept i lista
        recipeList.Add(newRecipe);

        // Spara listan i JSON-fil
        SaveRecipes();
    }

    // Slumpa fram recept
    public static void RandomRecipe()
    {
        Console.WriteLine("Slumpa fram recept");
    }

    // Ta bort recept
    public static void RemoveRecipe()
    {
        Console.WriteLine("Ta bort recept");
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
        Console.WriteLine("Hejdå! :) ");
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