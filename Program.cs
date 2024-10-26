using System;
using System.Data.Common;
using System.Runtime.Serialization.Formatters;
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
            Console.Clear();
            Console.WriteLine("Ogiltigt val, vänligen försök igen.\n");
            DisplayMenu();
        }
    }

    // Visa alla recept
    public static void ShowAllRecipes()
    {
        Console.Clear();
        Console.WriteLine("ALLA RECEPT\n");

        // Om det inte finns några recept, be användaren återvända till huvudmeyn
        if (recipeList.Count == 0)
        {
            Console.WriteLine("Det finns inga recept att hämta..\n");
            Console.WriteLine("Tryck på B för att återgå till huvudmenyn.");


            while (true)
            {
                char input = Console.ReadKey(true).KeyChar;
                if (input == 'B' || input == 'b')
                {
                    Console.Clear();
                    DisplayMenu();
                    return;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Ogiltigt val, försök igen.");
                }
            }

        }

        Console.WriteLine("Ange ID för det recept du vill titta på eller tryck på B för återgå till huvudmenyn.\n");
        // Annars loopa igenom listan med recept
        foreach (var recipe in recipeList)
        {
            Console.WriteLine($"{recipe.Id}: {recipe.Name}");
            Console.WriteLine("Kategori:");
            foreach (var category in recipe.Category)
            {
                Console.WriteLine($"- {category}\n");
            }
        }

        // Loop för att välja ett recept
        while (true)
        {
            string? input = Console.ReadLine();

            if (input?.ToUpper() == "B")
            {
                // Gå tillbaka till menyn
                Console.Clear();
                DisplayMenu();
            }

            else if (int.TryParse(input, out int recipeId))
            {
                // Visa receptet som valts
                ShowSingleRecipe(recipeId);
            }
        }


    }

    // Visa valt recept
    public static void ShowSingleRecipe(int recipeId)
    {
        Console.WriteLine("SHOW SINGLE RECIPE");
        // Hitta recept med angivet ID
        var recipe = recipeList.FirstOrDefault(r => r.Id == recipeId);

        // Om recept hittades, skriv ut
        if (recipe != null)
        {
            Console.Clear();
            Console.WriteLine($"{recipe.Name}\n");
            Console.WriteLine($"Ingredienser:\n");
            foreach (var ingredient in recipe.Ingredients)
            {
                Console.WriteLine($"- {ingredient}");
            }
            Console.WriteLine($"\nInstruktioner:\n");
            for (int i = 0; i < recipe.Instructions.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {recipe.Instructions[i]}");
            }
            Console.WriteLine($"\nKategori:\n");
            foreach (var category in recipe.Category)
            {
                Console.WriteLine($"- {category}\n");
            }
        }
        // Felhantering
        else
        {
            Console.Clear();
            Console.WriteLine("Det finns inget recept med det angivna ID:et.\n");
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
                Console.Clear();
                Console.WriteLine("Ogiltigt val, tryck på B för att återgå till alla recept.");
            }
        }
    }

    // Visa alla kategorier
    public static void ShowCategories()
    {
        Console.Clear();
        Console.WriteLine("Alla kategorier\n");

        // Hämta kategorier
        List<string> allCategories = recipeList
            .SelectMany(r => r.Category)
            .Distinct()
            .ToList();

        // Om det inte finns några kategorier
        if (allCategories.Count == 0)
        {
            Console.WriteLine("Det finns inga kategorier att visa..");
            Console.WriteLine("Tryck på B för att återgå till huvudmenyn.");

            char input = Console.ReadKey(true).KeyChar;
            if (input == 'B' || input == 'b')
            {
                Console.Clear();
                DisplayMenu();
                return;
            }
        }

        while (true)
        {
            Console.WriteLine("Ange ID för den kategori du vill se eller tryck på B för att återgå till huvudmenyn.\n");
            // Loopa igenom och visa alla kategorier
            for (int i = 0; i < allCategories.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {allCategories[i]}");
            }

            string? inputBack = Console.ReadLine();
            if (inputBack?.ToUpper() == "B")
            {
                Console.Clear();
                DisplayMenu();
                return;
            }

            // Välj kategori
            if (int.TryParse(inputBack, out int categoryIndex) && categoryIndex > 0 && categoryIndex <= allCategories.Count)
            {
                string selectedCategory = allCategories[categoryIndex - 1];
                RecipesInCategory(selectedCategory);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Ogiltigt val, försök igen.");
            }
        }


    }

    // Visa recept via kategorier
    public static void RecipesInCategory(string category)
    {
        Console.Clear();
        Console.WriteLine($"Recept i kategori: {category}\n");
        Console.WriteLine("Ange ID för det recept du vill se eller tryck på B för att gå tillbaka till alla kategorier.\n");

        // Filtrera ur recept från vald kategori
        List<Recipe> fromCategory = recipeList
        .Where(r => r.Category.Contains(category))
        .ToList();

        foreach (var recipe in fromCategory)
        {
            Console.WriteLine($"{recipe.Id}: {recipe.Name}");
        }

        string? input = Console.ReadLine();

        if (input?.ToUpper() == "B")
        {
            Console.Clear();
            ShowCategories();
            return;
        }

        if (int.TryParse(input, out int recipeId))
        {
            // Kontroll att receptet finns i kategorin
            if (fromCategory.Any(r => r.Id == recipeId))
            {
                ShowSingleRecipe(recipeId);
            }

        }
        else
        {
            Console.WriteLine("Ogiltigt val, försök igen.");
        }
    }

    // Lägg till recept
    public static void AddNewRecipe()
    {
        Console.Clear();

        Recipe newRecipe = new Recipe(); // Nytt objekt

        // Generera ID till recept baserat på listan
        newRecipe.Id = recipeList.Count + 1;

        Console.WriteLine("LÄGG TILL NYTT RECEPT\n");
        // Kontroll för tom inmatning
        while (true)
        {
            Console.WriteLine("Ange namn för recept:");
            newRecipe.Name = Console.ReadLine();
            Console.Clear();

            if (string.IsNullOrEmpty(newRecipe.Name))
            {
                Console.WriteLine("Du måste ange ett namn för receptet.");
            }
            else
            {
                break; // Avbryt när namnet är ifyllt
            }
        }

        while (true)
        {
            Console.WriteLine("Ange ingredienser:");
            Console.WriteLine("Separera ingrediens med kommatecken eller tryck enter.");
            Console.WriteLine("Skriv ok när du är klar.\n");
            List<string> ingredients = new List<string>();
            string? input;

            while ((input = Console.ReadLine())?.ToLower() != "ok")
            {
                if (!string.IsNullOrEmpty(input))
                {
                    ingredients.Add(input);
                }
            }
            newRecipe.Ingredients = ingredients.ToArray();
            Console.Clear();

            if (ingredients.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("Lägg till minst 1 ingrediens.\n");
            }
            else
            {
                break; // Avbryt när ingredienser är ifyllt
            }
        }

        while (true)
        {
            Console.WriteLine("Ange instruktioner:");
            Console.WriteLine("Separera instruktionerna med kommatecken eller tryck enter.\n");
            Console.WriteLine("Skriv ok när du är klar.");
            List<string> instructions = new List<string>();
            string? input;

            while ((input = Console.ReadLine())?.ToLower() != "ok")
            {
                if (!string.IsNullOrEmpty(input))
                {
                    instructions.Add(input);
                }
            }
            newRecipe.Instructions = instructions.ToArray();
            Console.Clear();

            if (instructions.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("Lägg till minst 1 instruktion.\n");
            }
            else
            {
                break; // Avbryt när ingredienser är ifyllt
            }
        }


        // Hantera kategorier
        List<string> allCategories = recipeList
        .SelectMany(r => r.Category)
        .Distinct()
        .ToList();

        Console.WriteLine("Lägg receptet i en kategori.\n");
        Console.WriteLine("För att skapa ny kategori: Tryck N\n");
        Console.WriteLine("För att lägga till i befintlig kategori: Tryck A\n");
        Console.WriteLine("Kategorier som redan finns:");
        for (int i = 0; i < allCategories.Count; i++)
        {
            Console.WriteLine($"{allCategories[i]}");
        }



        // Lägg till ny kategori
        while (true)
        {
            string? catInput = Console.ReadLine()?.ToUpper();
            if (catInput == "N")
            {
                Console.Clear();
                Console.WriteLine("Ange namn för den nya kategorin.\n");
                string? newCategory = Console.ReadLine();

                if (!string.IsNullOrEmpty(newCategory))
                {
                    newRecipe.Category = new[] { newCategory }; // Lägg till ny kategori
                    break; // Avbryt
                }
                else
                {
                    Console.WriteLine("Den nya kategorin får inte vara tom.\n");
                }
            }



            else if (catInput == "A" && allCategories.Count > 0)
            {

                // Visa befintliga kategorier    
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Ange ID för den kategori receptet ska läggas till i:\n");
                    for (int i = 0; i < allCategories.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}: {allCategories[i]}");
                    }
                    // Kontroll för korrekt ID
                    if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= allCategories.Count)
                    {
                        newRecipe.Category = new[] { allCategories[index - 1] }; // Lägg till vald kategori till receptet
                        break; // Avbryt
                    }
                    else
                    {
                        Console.WriteLine("Det angivna ID:et finns inte, testa igen.\n");
                    }
                }
                break;

            }
            else
            {
                Console.WriteLine("Ogiltigt val, försök igen. Välj N för ny kategori eller A för att lägga till i en befintlig.\n");
            }
            }



            // Lägg till recept i lista
            recipeList.Add(newRecipe);

            // Spara listan i JSON-fil
            SaveRecipes();

            // återgå till menyn
            Console.Clear();
            Console.WriteLine("Recept tillagt!\n");
            Console.WriteLine("\nTryck på B för att återgå till huvudmenyn.");

            while (true)
            {
                string? input = Console.ReadLine();
                if (input?.ToUpper() == "B")
                {
                    Console.Clear();
                    DisplayMenu();
                    return;
                }
                else
                {
                    Console.WriteLine("Ogiltigt val, försök igen.");
                }
            }
        
    }

    // Slumpa fram recept
    public static void RandomRecipe()
    {
        Console.Clear();
        Console.WriteLine("SLUMPA FRAM RECEPT");
        // Om det inte finns några recept att hämta
        if (recipeList.Count == 0)
        {
            Console.WriteLine("Det finns inga recept att slumpa fram.. \n");
            Console.WriteLine("Tryck på B för att återgå till huvudmenyn.");

            while (true)
            {
                char input = Console.ReadKey(true).KeyChar;
                if (input == 'B' || input == 'b')
                {
                    Console.Clear();
                    DisplayMenu();
                    return;
                }
                else
                {
                    Console.WriteLine("Ogiltigt val, försök igen.");
                }
            }
        }

        // Om det finns recept att hämta
        Random random = new Random();
        int randomIndex = random.Next(recipeList.Count);
        Recipe randomRecipe = recipeList[randomIndex];

        // Visa slumpat recept
        Console.WriteLine($"Recept: {randomRecipe.Name}\n");
        Console.WriteLine("Ingredienser:\n");
        foreach (var ingredient in randomRecipe.Ingredients)
        {
            Console.WriteLine($"- {ingredient}");
        }

        Console.WriteLine("\nInstruktioner:\n");
        for (int i = 0; i < randomRecipe.Instructions.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {randomRecipe.Instructions[i]}");
        }

        Console.WriteLine("\nKategori:\n");
        foreach (var category in randomRecipe.Category)
        {
            Console.WriteLine($"- {category}");
        }

        // Återgå till huvudmenyn
        Console.WriteLine("\nTryck på B för att återgå till huvudmenyn.");
        while (true)
        {
            string? input = Console.ReadLine();
            if (input?.ToUpper() == "B")
            {
                Console.Clear();
                DisplayMenu();
                return;
            }
        }

    }

    // Ta bort recept
    public static void RemoveRecipe()
    {
        Console.Clear();
        Console.WriteLine("TA BORT RECEPT");

        // Visa alla recept
        foreach (var recipe in recipeList)
        {
            Console.WriteLine($"{recipe.Id}: {recipe.Name}");
        }

        while (true)
        {
            Console.WriteLine("Ange ID för det recept du vill ta bort eller tryck B för att återgå till huvudmenyn.");

            string? input = Console.ReadLine();

            if (input?.ToUpper() == "B")
            {
                Console.Clear();
                DisplayMenu();
                return;
            }

            // Parsa inmatning
            if (int.TryParse(input, out int recipeId))
            {
                // Hitta inmatat ID
                var recipeToRemove = recipeList.FirstOrDefault(r => r.Id == recipeId);

                if (recipeToRemove != null)
                {
                    recipeList.Remove(recipeToRemove);
                    SaveRecipes();
                    Console.WriteLine($"Receptet har tagits bort.");
                    break;
                }
                else
                {
                    Console.WriteLine("Ogiltigt ID, försök igen.");
                }
            }
        }
        // Visa alla recept
        // foreach (var recipe in recipeList)
        // {
        //     Console.WriteLine($"{recipe.Id}: {recipe.Name}");
        // }


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