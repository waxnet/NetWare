using System.Runtime.InteropServices;
using System;

namespace NetWare.Natives;

public static class Shell32Lib
{
    [DllImport("shell32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr ShellExecute(IntPtr hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, int nShowCmd);
}
