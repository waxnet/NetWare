namespace Loader
{
    public static class Window
    {
        public static void SetSize(int width, int height)
        {
            Console.SetWindowSize(width, height);

            Console.BufferWidth = width;
            Console.BufferHeight = height;
        }

        public static void SetTitle(string title)
        {
            Console.Title = title;
        }
    }
}
