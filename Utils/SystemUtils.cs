using System;
using System.Runtime.InteropServices;

namespace NetWare.Utils;

public static class SystemUtils
{
    [DllImport("shell32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr ShellExecute(IntPtr hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, int nShowCmd);

    public static void OpenFolder(string folderPath)
    {
        ShellExecute(IntPtr.Zero, "open", folderPath, null, null, 1);
    }
}
