using System.Runtime.InteropServices;

namespace Loader
{
    public static class Message
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern void MessageBox(IntPtr hWnd, string text, string caption, uint type);

        public static void ShowError(string errorCode)
        {
            MessageBox(
                IntPtr.Zero,
                string.Concat(
                    "You can try to fix this issue by looking in the debug folder.",
                    "\nIf you cant fix it report the error code on GitHub or on Discord.",
                    ("\n\nError Code : " + errorCode)
                ),
                "Error!",
                0x00000010
            );
        }
    }
}
