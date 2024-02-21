using System.Diagnostics;
using SharpMonoInjector;

namespace NetWareLoader
{
    public class Program
    {
        public static void Main()
        {
            // setup console
            Window.SetSize(80, 20);
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

            // download latest cheat version
            IO.Puts("Downloading latest cheat version . . .", ConsoleColor.DarkYellow);
            
            if (File.Exists(Data.cheatPath))
                File.Delete(Data.cheatPath);

            bool downloadedFile = Network.DownloadFile(
                "https://raw.githubusercontent.com/waxnet/NetWare/main/.build/NetWare.dll",
                Data.cheatPath
            );
            if (!downloadedFile)
            {
                Message.ShowError("Please report this on GitHub through an issue or on Discord.\nError Code : 001");
                return;
            }

            // disable/patch anticheat
            IO.Puts("Patching AntiCheat . . .", ConsoleColor.DarkYellow);
            if (!AntiCheat.Patch())
            {
                Message.ShowError("Please report this on GitHub through an issue or on Discord.\nError Code : 002");
                return;
            }

            // start 1v1.lol
            IO.Puts("Starting 1v1.LOL . . .", ConsoleColor.DarkYellow);
            if (!Manager.StartGameProcess())
            {
                Message.ShowError("Please report this on GitHub through an issue or on Discord.\nError Code : 003");
                return;
            }

            // wait for injection
            IO.WaitForInput("\nPress any key to inject once the game has loaded . . .", ConsoleColor.DarkYellow);

            Injector gameInjector = new("1v1_LOL");
            IntPtr hasInjected = gameInjector.Inject(
                File.ReadAllBytes(Data.cheatPath),
                "NetWare",
                "Loader",
                "Load"
            );
            gameInjector.Dispose();

            if (hasInjected == IntPtr.Zero)
            {
                Message.ShowError("Please report this on GitHub through an issue or on Discord.\nError Code : 004");
                return;
            }
        }
    }
}
