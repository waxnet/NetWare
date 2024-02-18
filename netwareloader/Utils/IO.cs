using System.Drawing;

namespace NetWareLoader
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

        public static void WaitForInput(string text = "", ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
            Console.ReadKey(intercept: true);
        }
    }
}
