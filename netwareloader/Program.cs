using System.Diagnostics;
using NetWareLoader;

namespace NetWareLoader
{
    public class Program
    {
        public static void Main()
        {
            // setup console
            Window.SetSize(90, 20);
            Window.SetTitle("NetWare Loader");
            Console.Clear();

            // display banner
            Banner.Display();

            // check if 1v1.lol is already running and shut it down
            Process? gameProcess = Manager.FindGameProcess();
            if (gameProcess != null)
            {
                IO.Puts("Killing 1v1.LOL process . . .", ConsoleColor.DarkYellow);
                gameProcess.Kill();
                gameProcess.WaitForExit();
            }

            // search game and steam path
            IO.Puts("Searching paths . . .", ConsoleColor.DarkYellow);
            Resolve.Paths();
            if (!Data.ArePathsValid())
            {
                Message.ShowError("Please report this on GitHub through an issue or on Discord.\nError Code : 000");
                return;
            }

            // download injector
            if (!Directory.Exists(Data.injectorPath))
            {
                IO.Puts("Downloading injector . . .", ConsoleColor.DarkYellow);

                // create injector path
                Directory.CreateDirectory(Data.injectorPath);

                // download injector
                bool downloadedZIP = Network.DownloadZIP(
                    "https://github.com/warbler/SharpMonoInjector/releases/download/v2.2/SharpMonoInjector.Console.zip",
                    Data.injectorPath
                );
                if (!downloadedZIP)
                {
                    Message.ShowError("Please report this on GitHub through an issue or on Discord.\nError Code : 001");
                    return;
                }

                // cleanup
                string componentsFolder = Path.Combine(Data.injectorPath, "SharpMonoInjector.Console");
                string componentA = Path.Combine(Data.injectorPath, componentsFolder, "SharpMonoInjector.dll");
                string componentB = Path.Combine(Data.injectorPath, componentsFolder, "smi.exe");

                File.Move(componentA, Path.Combine(Data.injectorPath, Path.GetFileName(componentA)));
                File.Move(componentB, Path.Combine(Data.injectorPath, Path.GetFileName(componentB)));
                Directory.Delete(componentsFolder);
            }

            // download latest cheat version
            IO.Puts("Downloading latest cheat version . . .", ConsoleColor.DarkYellow);
            
            if (File.Exists(Data.cheatPath))
                File.Delete(Data.cheatPath);

            bool downloadedFile = Network.DownloadFile(
                "https://github.com/waxnet/NetWare/releases/latest/download/NetWare.dll",
                Data.cheatPath
            );
            if (!downloadedFile)
            {
                Message.ShowError("Please report this on GitHub through an issue or on Discord.\nError Code : 002");
                return;
            }

            // disable/patch anticheat
            IO.Puts("Patching AntiCheat . . .", ConsoleColor.DarkYellow);
            if (!AntiCheat.Patch())
            {
                Message.ShowError("Please report this on GitHub through an issue or on Discord.\nError Code : 003");
                return;
            }

            // start 1v1.lol
            IO.Puts("Starting 1v1.LOL . . .", ConsoleColor.DarkYellow);
            if (!Manager.StartGameProcess())
            {
                Message.ShowError("Please report this on GitHub through an issue or on Discord.\nError Code : 004");
                return;
            }

            // wait for injection
            IO.WaitForInput("\nPress any key to inject once the game has loaded . . .", ConsoleColor.DarkYellow);
            if (!Injector.Inject())
            {
                Message.ShowError("Please report this on GitHub through an issue or on Discord.\nError Code : 005");
                return;
            }
        }
    }
}
