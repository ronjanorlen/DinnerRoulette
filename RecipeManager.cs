using System;
using System.Text.Json;
using ConsoleStyling; // Importera styling-klass från ConsoleStyle
using Recipes; // Importerar recipe-klass från recipes.cs


namespace CookBook
{

    public static class RecipeManager
    {

        // Visa alla recept
        public static void ShowAllRecipes()
        {
            var recipeList = Program.GetRecipeList(); // Hämta listan
            Console.Clear();
            ConsoleStyle.PrintColor("ALLA RECEPT\n", ConsoleColor.Yellow);

            // Om det inte finns några recept, be användaren återvända till huvudmeyn
            if (recipeList.Count == 0)
            {
                ConsoleStyle.PrintColor("Det finns inga recept att hämta..\nTryck på valfri tangent för att återgå till huvudmenyn.\n", ConsoleColor.DarkYellow);
                Console.ReadKey();
                Console.Clear();
                Program.DisplayMenu();
            }

            // Om det finns recept att visa
            ConsoleStyle.PrintColor("Ange siffra för det recept du vill titta på. \n", ConsoleColor.DarkYellow);
            ConsoleStyle.PrintColor("Tryck på X för att gå tillbaka till huvudmenyn.\n", ConsoleColor.DarkGray);

            while (true)
            {
                // loopa igenom listan med recept
                for (int i = 0; i < recipeList.Count; i++)
                {
                    ConsoleStyle.PrintColor($"[{i}] {recipeList[i].Name}\n", ConsoleColor.Yellow);
                }

                // Ta input från användare
                string? input = Console.ReadLine();

                if (input?.ToUpper() == "X")
                {
                    // Gå tillbaka till menyn
                    Console.Clear();
                    Program.DisplayMenu();
                }
                // Parsa inmatning
                else if (int.TryParse(input, out int recipeIndex) && recipeIndex >= 0 && recipeIndex < recipeList.Count)
                {
                    // Visa receptet som valts
                    ShowSingleRecipe(recipeIndex);
                }
                else
                // Vid ogiltigt val
                {
                    Console.Clear();
                    ConsoleStyle.PrintColor("Ogiltigt val, ange siffra på receptet du vill se. \n", ConsoleColor.DarkRed);
                    ConsoleStyle.PrintColor("Tryck på X för att gå tillbaka till huvudmenyn.\n", ConsoleColor.DarkGray);
                }

            }

        }

        // Visa valt recept
        public static void ShowSingleRecipe(int recipeIndex)
        {
            var recipeList = Program.GetRecipeList(); // Hämta listan

            // Hitta recept med angivet ID
            var recipe = recipeList[recipeIndex];

            // Om recept hittades, skriv ut
            if (recipe != null)
            {
                Console.Clear();
                ConsoleStyle.PrintColor($"{recipe.Name}\n", ConsoleColor.Yellow);
                ConsoleStyle.PrintColor($"Ingredienser:\n", ConsoleColor.Yellow);
                foreach (var ingredient in recipe.Ingredients)
                {
                    ConsoleStyle.PrintColor($"- {ingredient}", ConsoleColor.DarkYellow);
                }
                ConsoleStyle.PrintColor($"\nInstruktioner:\n", ConsoleColor.Yellow);
                for (int i = 0; i < recipe.Instructions.Length; i++)
                {
                    ConsoleStyle.PrintColor($"{i + 1}. {recipe.Instructions[i]}", ConsoleColor.DarkYellow);
                }
                ConsoleStyle.PrintColor($"\nKategori:\n", ConsoleColor.Yellow);
                foreach (var category in recipe.Category)
                {
                    ConsoleStyle.PrintColor($"- {category}\n", ConsoleColor.DarkYellow);
                }
                ConsoleStyle.PrintColor("\nTryck på valfri tangent för att gå till alla recept.\n", ConsoleColor.DarkGray);
                Console.ReadKey();
                ShowAllRecipes();
            }
        }

        // Lägg till recept
        public static void AddNewRecipe()
        {
            var recipeList = Program.GetRecipeList(); // Hämta listan
            Console.Clear();
            ConsoleStyle.PrintColor("LÄGG TILL NYTT RECEPT\n", ConsoleColor.Yellow);

            while (true)
            {
                ConsoleStyle.PrintColor("Tryck 1 för att lägga till ett nytt recept.", ConsoleColor.DarkYellow);
                ConsoleStyle.PrintColor("Tryck X för att gå tillbaka till huvudmenyn.\n", ConsoleColor.DarkGray);

                string? addRecipe = Console.ReadLine()?.ToUpper();

                // Om inmatning är X gå till huvudmenyn
                if (addRecipe == "X")
                {
                    Console.Clear();
                    Program.DisplayMenu();
                    return;
                }
                // Om inmatning är 1, bryt loopen och låt användaren lägga till recept
                else if (addRecipe == "1")
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    // Om felaktig inmatning
                    Console.Clear();
                    ConsoleStyle.PrintColor("Ogiltigt val, försök igen.\n", ConsoleColor.DarkRed);
                }
            }

            Recipe newRecipe = new Recipe(); // Nytt objekt

            // Kontroll för tom inmatning
            while (true)
            {
                ConsoleStyle.PrintColor("Ange namn för recept:\n", ConsoleColor.Yellow);
                newRecipe.Name = Console.ReadLine();

                if (string.IsNullOrEmpty(newRecipe.Name)) // om inget namn anges
                {
                    Console.Clear();
                    ConsoleStyle.PrintColor("Du måste ange ett namn för receptet.\n", ConsoleColor.DarkRed);
                }
                else
                {
                    Console.Clear();
                    break; // Avbryt när namnet är ifyllt
                }
            }

            while (true)
            {
                ConsoleStyle.PrintColor("Ange ingredienser:\n", ConsoleColor.Yellow);
                ConsoleStyle.PrintColor("Tryck enter mellan varje ingrediens.", ConsoleColor.DarkYellow);
                ConsoleStyle.PrintColor("Skriv ok när du är klar.\n", ConsoleColor.DarkYellow);

                List<string> ingredients = new List<string>(); // Skapa lista med instruktioner

                string? ingredientInput;

                while (true)
                {
                    ingredientInput = Console.ReadLine()?.Trim().ToLower();

                    if (ingredientInput == "ok") // när användare skriver ok bryt loopen
                    {
                        break;
                    }

                    if (!string.IsNullOrEmpty(ingredientInput))
                    {
                        ingredients.Add(ingredientInput); // lägg till ingredienser
                    }
                    else
                    {
                        ConsoleStyle.PrintColor("Lägg till minst 1 ingrediens.\n", ConsoleColor.DarkRed);
                    }

                }
                // om användaren skriver ok utan att lägga till ingrediens
                if (ingredients.Count == 0)
                {
                    Console.Clear();
                    ConsoleStyle.PrintColor("Ogiltig inmatning.\n", ConsoleColor.DarkRed);
                }
                else
                {
                    newRecipe.Ingredients = ingredients.ToArray(); // lägg till ingredienserna i array
                    Console.Clear();
                    break; // Avbryt när ingredienser är tillagt
                }
            }

            while (true)
            {
                ConsoleStyle.PrintColor("Ange instruktioner:\n", ConsoleColor.Yellow);
                ConsoleStyle.PrintColor("Tryck enter mellan varje instruktion.", ConsoleColor.DarkYellow);
                ConsoleStyle.PrintColor("Skriv ok när du är klar.\n", ConsoleColor.DarkYellow);

                List<string> instructions = new List<string>(); // Skapa lista med instruktioner

                string? instructionInput;

                while (true)
                {
                    instructionInput = Console.ReadLine()?.Trim().ToLower();

                    if (instructionInput == "ok") // när användare skriver ok, bryt loopen
                    {
                        break;
                    }

                    if (!string.IsNullOrEmpty(instructionInput))
                    {
                        instructions.Add(instructionInput); // lägg till instruktioner
                    }
                    else
                    {
                        ConsoleStyle.PrintColor("Lägg till minst 1 instruktion.\n", ConsoleColor.DarkRed);
                    }

                }

                // om användaren skriver ok utan att lägga till instruktion
                if (instructions.Count == 0)
                {
                    Console.Clear();
                    ConsoleStyle.PrintColor("Ogiltig inmatning.\n", ConsoleColor.DarkRed);
                }
                else
                {
                    newRecipe.Instructions = instructions.ToArray(); // lägg instruktioner i en array
                    Console.Clear();
                    break; // Avbryt när ingredienser är tillagt
                }
            }


            // Hantera kategorier
            List<string> allCategories = recipeList
            .SelectMany(r => r.Category) // Hämta kategorierna
            .Distinct() // Sortera ut ev dubletter
            .ToList(); // Lägg i lista

            ConsoleStyle.PrintColor("Lägg receptet i en kategori.\n", ConsoleColor.Yellow);
            ConsoleStyle.PrintColor("För att skapa ny kategori: Tryck N\n", ConsoleColor.DarkYellow);
            ConsoleStyle.PrintColor("För att lägga till i befintlig kategori: Tryck A\n", ConsoleColor.DarkYellow);

            if (allCategories.Count == 0) // Om det inte finns några kategorier ännu
            {
                ConsoleStyle.PrintColor("Det finns inga kategorier ännu..", ConsoleColor.DarkGray);
            }
            else // Om det finns befintliga kategorier
            {
                ConsoleStyle.PrintColor("Kategorier som redan finns:", ConsoleColor.Yellow);
                for (int i = 0; i < allCategories.Count; i++)
                {
                    ConsoleStyle.PrintColor($"{allCategories[i]}", ConsoleColor.DarkYellow);
                }
            }

            // Lägg till en ny kategori
            while (true)
            {
                string? catInput = Console.ReadLine()?.ToUpper();
                if (catInput == "N")
                {
                    while (true)
                    {
                        Console.Clear();
                        ConsoleStyle.PrintColor("Ange namn för den nya kategorin.\n", ConsoleColor.Yellow);
                        string? newCategory = Console.ReadLine();

                        if (!string.IsNullOrEmpty(newCategory))
                        {
                            newRecipe.Category = new[] { newCategory }; // Lägg till ny kategori
                            break; // Avbryt
                        }
                    }
                    break;
                }

                else if (catInput == "A" && allCategories.Count > 0)
                {
                    // Visa befintliga kategorier    
                    while (true)
                    {
                        Console.Clear();
                        ConsoleStyle.PrintColor("Ange ID för den kategori receptet ska läggas till i:\n", ConsoleColor.Yellow);
                        for (int i = 0; i < allCategories.Count; i++)
                        {
                            ConsoleStyle.PrintColor($"{i + 1}: {allCategories[i]}", ConsoleColor.Yellow);
                        }
                        // Kontroll för korrekt ID
                        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= allCategories.Count)
                        {
                            newRecipe.Category = new[] { allCategories[index - 1] }; // Lägg till receptet i vald kategori
                            break; // Avbryt
                        }
                    }
                    break;

                }
                else
                {
                    Console.Clear();
                    ConsoleStyle.PrintColor("Ogiltigt val, försök igen. \n", ConsoleColor.DarkRed);
                    ConsoleStyle.PrintColor("Välj N för ny kategori eller A för att lägga till i en befintlig.\n", ConsoleColor.DarkGray);
                }
            }

            // Lägg till recept i lista
            recipeList.Add(newRecipe);

            // Spara listan i JSON-fil
            Program.SaveRecipes();

            // återgå till menyn
            Console.Clear();
            ConsoleStyle.PrintColor("Recept tillagt!\n", ConsoleColor.DarkGreen);
            ConsoleStyle.PrintColor("Tryck på valfri tangent för att återgå till huvudmenyn.\n", ConsoleColor.DarkGray);
            Console.ReadKey();
            Console.Clear();
            Program.DisplayMenu();

        }
        // Slumpa fram recept
        public static void RandomRecipe()
        {
            var recipeList = Program.GetRecipeList(); // Hämta listan
            Console.Clear();
            ConsoleStyle.PrintColor("SLUMPAR FRAM RECEPT\n", ConsoleColor.DarkYellow);

            // Simulera att ett recept letas fram i form av nedräkning
            for (int i = 3; i > 0; i--)
            {
                ConsoleStyle.PrintColor($"{i} ", ConsoleColor.DarkYellow); // Skriv ut 3 2 1
                Thread.Sleep(1000); // Använder thread.sleep för fördröjning av siffrorna
            }

            Console.Clear();
            Console.WriteLine("\n");

            // Om det inte finns några recept att hämta
            if (recipeList.Count == 0)
            {
                ConsoleStyle.PrintColor("Kokboken är tom.. \n", ConsoleColor.DarkYellow);
                ConsoleStyle.PrintColor("Tryck på valfri tangent för att återgå till huvudmenyn.\n", ConsoleColor.DarkGray);
                Console.ReadKey();
                Console.Clear();
                Program.DisplayMenu();
            }

            // Om det finns recept att hämta
            Random random = new Random(); // Nytt random-objekt
            int randomIndex = random.Next(recipeList.Count); // Skapa slumpmässigt index baserat på antal recept i listan
            Recipe randomRecipe = recipeList[randomIndex]; // Lagra slumpat recept 

            // Visa slumpat recept
            // Namn
            ConsoleStyle.PrintColor($"Recept: {randomRecipe.Name}\n", ConsoleColor.Yellow);

            // Ingredienser
            ConsoleStyle.PrintColor("Ingredienser:\n", ConsoleColor.Yellow);
            foreach (var ingredient in randomRecipe.Ingredients)
            {
                ConsoleStyle.PrintColor($"- {ingredient}", ConsoleColor.DarkYellow);
            }

            // Instruktioner
            ConsoleStyle.PrintColor("\nInstruktioner:\n", ConsoleColor.Yellow);
            // Skriv steg, 1: gör detta, 2: gör detta, etc
            for (int i = 0; i < randomRecipe.Instructions.Length; i++)
            {
                ConsoleStyle.PrintColor($"{i + 1}. {randomRecipe.Instructions[i]}", ConsoleColor.DarkYellow);
            }

            Console.WriteLine("\nKategori:\n");
            foreach (var category in randomRecipe.Category)
            {
                ConsoleStyle.PrintColor($"- {category}", ConsoleColor.DarkYellow);
            }

            // Återgå till huvudmenyn
            ConsoleStyle.PrintColor("\nTryck på X för att återgå till huvudmenyn.\n", ConsoleColor.DarkGray);
            while (true)
            {
                string? input = Console.ReadLine();
                if (input?.ToUpper() == "X")
                {
                    Console.Clear();
                    Program.DisplayMenu();
                    return;
                }
            }

        }

        // Ta bort recept
        public static void RemoveRecipe()
        {
            var recipeList = Program.GetRecipeList(); // Hämta listan
            Console.Clear();
            ConsoleStyle.PrintColor("TA BORT RECEPT\n", ConsoleColor.Red);
            ConsoleStyle.PrintColor("Ange ID för det recept du vill ta bort.\n", ConsoleColor.DarkYellow);
            ConsoleStyle.PrintColor("Tryck på X för att återgå till huvudmenyn.\n", ConsoleColor.DarkGray);

            // loopa igenom listan med recept
            for (int i = 0; i < recipeList.Count; i++)
            {
                ConsoleStyle.PrintColor($"[{i}] {recipeList[i].Name}\n", ConsoleColor.Yellow);
            }

            // Om det inte finns några recept att visa
            if (recipeList.Count == 0)
            {
                Console.Clear();
                ConsoleStyle.PrintColor("Det finns inga recept att visa, tryck på valfri tangent för att återgå till huvudmenyn.\n", ConsoleColor.DarkYellow);
                Console.ReadKey();
                Console.Clear();
                Program.DisplayMenu();
            }

            // While-loop för kontroll av inmatning från användare
            while (true)
            {

                string? delete = Console.ReadLine();

                // Om inmatning är X, gå till huvudmenyn
                if (delete?.ToUpper() == "X")
                {
                    Console.Clear();
                    Program.DisplayMenu();
                    return;
                }

                // Parsa inmatning
                if (int.TryParse(delete, out int recipeIndex) && recipeIndex >= 0 && recipeIndex < recipeList.Count)
                {
                    recipeList.RemoveAt(recipeIndex); // Ta bort recept
                    Program.SaveRecipes(); // spara listan
                    Console.Clear();
                    ConsoleStyle.PrintColor("Receptet har tagits bort.\n", ConsoleColor.DarkYellow);
                    ConsoleStyle.PrintColor("Tryck på valfri tangent för att återgå till huvudmenyn.\n", ConsoleColor.DarkGray);
                    Console.ReadKey();
                    Console.Clear();
                    Program.DisplayMenu();
                    return;
                }
                // Om användare anger felaktigt ID
                else
                {
                    ConsoleStyle.PrintColor("ID:et hittades inte, försök igen.\n", ConsoleColor.DarkRed);
                }
            }
        }

    }

}