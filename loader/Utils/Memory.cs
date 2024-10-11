using System.Runtime.InteropServices;
using System.Text;

namespace Loader;

public static class Memory
{
    // internal methods
    [DllImport("psapi.dll")]
    private static extern bool EnumProcessModules(IntPtr hProcess, IntPtr[] lphModule, uint cb, out uint lpcbNeeded);

    [DllImport("psapi.dll")]
    private static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, StringBuilder lpBaseName, uint nSize);

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool CloseHandle(IntPtr hObject);

    // methods
    public static int GetLoadedModules(int processId)
    {
        int dllCount = 0;
        
        IntPtr processHandle = OpenProcess(0x0400 | 0x0010, false, processId);
        if (processHandle == IntPtr.Zero)
            return 0;

        try {
            IntPtr[] modules = new IntPtr[1024];

            if (EnumProcessModules(processHandle, modules, (uint)(modules.Length * IntPtr.Size), out uint bytesNeeded))
                dllCount = (int)(bytesNeeded / IntPtr.Size);
            else
                dllCount = 0;
        } catch {
            return 0;
        } finally {
            if (processHandle != IntPtr.Zero)
                CloseHandle(processHandle);
        }

        return dllCount;
    }
}
