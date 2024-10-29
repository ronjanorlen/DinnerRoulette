using System;

namespace ConsoleStyling
{
    public static class ConsoleStyle
    {

        public static void PrintColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void PrintBold(string text)
        {
            Console.WriteLine("\x1b[1m" + text + "\x1b[22m");
        }
    }
}
