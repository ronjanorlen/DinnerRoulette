# Programmering i C#.NET - DT071G - Projektuppgift
## DinnerRoulette - en kokbok med twist
Detta repository innehåller kod för en konsolapplikation i form av en kokbok.  Applikationen är skriven i programmeringsspråket C# med .NET som ramverk och låter användare hantera recept och kategorier. Applikationen är uppdelad i olika komponenter som hanterar specifika delar av programmet och interagerar med varandra för komplett funktionalitet.  

Projektet har skapats som en del av kursen Programmering i C#.NET.
## Funktioner
* **Visa alla recept:** Denna funktion visar alla recept som finns i kokboken. Användare kan välja ett specifikt recept att titta på ur denna lista.
* **Visa alla kategorier:** Denna funktion visar de kategorier som recepten lagts till i. Användare kan välja att visa alla recept i en specifik kategori.
* **Lägga till nytt recept:** Denna funktion tillåter använder att lägga till nya recept och placera dessa i en befintlig kategori eller skapa en ny kategori som receptet läggs i.
* **Slumpa fram ett recept:** Denna funktion används för att slumpa fram ett recept ur hela kokboken. Den fungerar som en rolig twist i applikationen där användare inte vet vilket typ av recept som slumpas fram.
* **Ta bort ett recept:** Denna funktion låter användare ta bort ett recept ur listan. Alla recept visas med ett ID och användaren får mata in det ID som ska tas bort. 

## Komponenter
### Program.cs
Innehåller huvudlogiken för applikationen. Detta inkluderar menyn och hantering av JSON-fil för att spara och ladda recept.
### RecipeManager.cs
Hanterar funktioner relaterat till recept så som att visa, lägga till, ta bort och slumpa fram ett recept.
### CategoryManager.cs
Hanterar funktioner för kategorier så som att visa alla kategorier samt recept inom en specifik kategori. 
### recipe.cs
Definierar recept-klassen med de egenskaper som ett recept innehåller. Dessa är namn, ingredienser, instruktioner och kategori.
### ConsoleStyling.cs
Innehåller metoder för att styla utskrifter i färg eller fet stil.

## Tekniker och verktyg
* **Visual Studio Code:** Används som utvecklingsmiljö för att bygga och testa applikationen.
* **C# och .NET:** Används som programmeringsspråk och ramverk.
* **JSON-fil:** Används för att lagra recepten. Denna laddas in när programmet startar och uppdateras när ändringar i receptlistan görs. 
 
## Skapad:
**Av:** Ronja Norlén  
**Kurs:** DT071G Programmering i C#.NET  
**Program:** Webbutveckling  
**Skola:** Mittuniversitetet  
**År:** 2024

