using System.Diagnostics;

namespace NetWareLoader
{
    public static class Injector
    {
        public static bool Inject()
        {
            if (!Directory.Exists(Data.injectorPath))
                return false;

            Process cmd = new()
            {
                StartInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = Path.Combine(Data.injectorPath, "smi.exe"),
                    Arguments = ("/C inject -p 1v1_LOL -a \"" + Data.cheatPath + "\" -n NetWare -c Loader -m Load > nul")
                }
            };
            return cmd.Start();
        }
    }
}
