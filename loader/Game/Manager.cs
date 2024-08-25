using System.Diagnostics;

namespace Loader
{
    public static class Manager
    {
        public static Process? FindGameProcess()
        {
            Process[] processes = Process.GetProcessesByName("1v1_LOL");

            if (processes.Length == 1)
                return processes[0];
            return null;
        }

        public static bool StartGameProcess()
        {
            string steamExecutable = Path.Combine(Data.steamPath, "steam.exe");
            if (!File.Exists(steamExecutable))
                return false;

            Process cmd = new()
            {
                StartInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "cmd.exe",
                    Arguments = ("/C \"" + steamExecutable + "\" steam://rungameid/2305790 > nul")
                }
            };
            return cmd.Start();
        }
    }
}
