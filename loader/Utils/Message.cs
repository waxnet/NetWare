using System.Runtime.InteropServices;

namespace Loader
{
    public static class Message
    {
        // internal methods
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern void MessageBox(IntPtr hWnd, string text, string caption, uint type);

        // methods
        public static void ShowError(string possibleSolution, string errorCode)
        {
            MessageBox(
                IntPtr.Zero,
                string.Concat(
                    $"Possible solution : {possibleSolution}.",
                    "\n\nIf the suggested solution doesnt seem to work report this error code on GitHub or Discord.",
                    $"\nError Code : {errorCode}"
                ),
                "Error!",
                0x00000010
            );
        }
    }
}
