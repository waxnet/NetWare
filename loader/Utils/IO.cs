using System.Drawing;

namespace Loader
{
    public static class IO
    {
        public static void Puts(string text, ConsoleColor color = ConsoleColor.White, bool newLine = true)
        {
            if (newLine)
                text += "\n";

            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        public static string Gets(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;

            Console.Write(text);
            string? value = Console.ReadLine();
            if (value == null)
                value = "";

            Console.ResetColor();

            return value;
        }

        public static void NewLine()
        {
            Console.WriteLine();
        }

        public static char WaitForInput(string text = "", ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();

            return Console.ReadKey(true).KeyChar;
        }
    }
}
