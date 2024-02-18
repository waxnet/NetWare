using System.Runtime.InteropServices;

namespace NetWareLoader
{
    public static class Message
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern void MessageBox(IntPtr hWnd, string text, string caption, uint type);

        public static void ShowError(string message)
        {
            MessageBox(IntPtr.Zero, message, "Error!", 0x00000010);
        }
    }
}
