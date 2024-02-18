namespace NetWareLoader
{
    public static class Banner
    {
        private static readonly string[] banner =
        {
            "   _  __      __  _      __               ",
            "  / |/ /___  / /_| | /| / /___ _ ____ ___ LOADER",
            " /    // -_)/ __/| |/ |/ // _ `// __// -_)",
            "/_/|_/ \\__/ \\__/ |__/|__/ \\_,_//_/   \\__/ "
        };

        public static void Display()
        {
            foreach (string part in banner)
            {
                for (int _ = 0; _ < ((Console.BufferWidth / 2) - 21); _++)
                {
                    IO.Puts(" ", ConsoleColor.White, false);
                }

                IO.Puts(part[..17], ConsoleColor.White, false);
                IO.Puts(part[17..42], ConsoleColor.Red, false);

                if (part.Length > 42)
                {
                    IO.Puts(part[42..48], ConsoleColor.DarkGray, false);
                }

                IO.NewLine();
            }

            IO.Puts("\n");
        }
    }
}
