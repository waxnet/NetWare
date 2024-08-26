using NetWare.Natives;
using System;

namespace NetWare.Utils;

public static class SystemUtils
{
    public static void OpenFolder(string folderPath)
    {
        Shell32Lib.ShellExecute(IntPtr.Zero, "open", folderPath, null, null, 1);
    }
}
