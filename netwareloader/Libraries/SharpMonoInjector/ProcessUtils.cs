using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace NetWareLoader.SharpMonoInjector
{
    public static class ProcessUtils
    {
        public static IEnumerable<ExportedFunction> GetExportedFunctions(nint handle, nint mod)
        {
            using (Memory memory = new(handle))
            {
                int e_lfanew = memory.ReadInt(mod + 0x3C);
                nint ntHeaders = mod + e_lfanew;
                nint optionalHeader = ntHeaders + 0x18;
                nint dataDirectory = optionalHeader + (Is64BitProcess(handle) ? 0x70 : 0x60);
                nint exportDirectory = mod + memory.ReadInt(dataDirectory);
                nint names = mod + memory.ReadInt(exportDirectory + 0x20);
                nint ordinals = mod + memory.ReadInt(exportDirectory + 0x24);
                nint functions = mod + memory.ReadInt(exportDirectory + 0x1C);
                int count = memory.ReadInt(exportDirectory + 0x18);

                for (int i = 0; i < count; i++)
                {
                    int offset = memory.ReadInt(names + i * 4);
                    string name = memory.ReadString(mod + offset, 32, Encoding.ASCII);
                    short ordinal = memory.ReadShort(ordinals + i * 2);
                    nint address = mod + memory.ReadInt(functions + ordinal * 4);

                    if (address != nint.Zero)
                        yield return new ExportedFunction(name, address);
                }
            }
        }

        public static bool GetMonoModule(nint handle, out nint monoModule)
        {
            int size = Is64BitProcess(handle) ? 8 : 4;

            nint[] ptrs = [];

            if (!Native.EnumProcessModulesEx(
                handle, ptrs, 0, out int bytesNeeded, ModuleFilter.LIST_MODULES_ALL))
            {
                throw new InjectorException("Failed to enumerate process modules", new Win32Exception(Marshal.GetLastWin32Error()));
            }

            int count = bytesNeeded / size;
            ptrs = new nint[count];

            if (!Native.EnumProcessModulesEx(
                handle, ptrs, bytesNeeded, out bytesNeeded, ModuleFilter.LIST_MODULES_ALL))
            {
                throw new InjectorException("Failed to enumerate process modules", new Win32Exception(Marshal.GetLastWin32Error()));
            }

            for (int i = 0; i < count; i++)
            {
                StringBuilder path = new StringBuilder(260);
                Native.GetModuleFileNameEx(handle, ptrs[i], path, 260);

                if (path.ToString().IndexOf("mono", StringComparison.OrdinalIgnoreCase) > -1)
                {
                    if (!Native.GetModuleInformation(handle, ptrs[i], out MODULEINFO info, (uint)(size * ptrs.Length)))
                        throw new InjectorException("Failed to get module information", new Win32Exception(Marshal.GetLastWin32Error()));

                    var funcs = GetExportedFunctions(handle, info.lpBaseOfDll);

                    if (funcs.Any(f => f.Name == "mono_get_root_domain"))
                    {
                        monoModule = info.lpBaseOfDll;
                        return true;
                    }
                }
            }

            monoModule = nint.Zero;
            return false;
        }

        public static bool Is64BitProcess(nint handle)
        {
            if (!Environment.Is64BitOperatingSystem)
                return false;

            if (!Native.IsWow64Process(handle, out bool isWow64))
                return nint.Size == 8; // assume it's the same as the current process

            return !isWow64;
        }
    }
}
