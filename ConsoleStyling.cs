using System;

namespace CookBook
{
    public static class ConsoleStyle
    {

        // För att skriva i färg
        public static void PrintColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
