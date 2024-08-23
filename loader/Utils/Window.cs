using System.Runtime.InteropServices;

namespace Loader
{
    public static class Window
    {
        // internal methods
        [DllImport("user32.dll", SetLastError = true)]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        // methods
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

        public static void DisableResizing()
        {
            IntPtr hwnd = GetConsoleWindow();
            if (hwnd == IntPtr.Zero)
                return;
            int currentStyle = GetWindowLong(hwnd, -16);

            _ = SetWindowLong(hwnd, -16, currentStyle & ~(0x00010000 | 0x00040000));
        }
    }
}
