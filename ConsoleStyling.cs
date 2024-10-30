using System;

namespace ConsoleStyling
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

        // För att skriva i fet text
        public static void PrintBold(string text)
        {
            Console.WriteLine("\x1b[1m" + text + "\x1b[22m");
        }
    }
}
