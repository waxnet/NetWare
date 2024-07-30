using System.Diagnostics;
using SharpMonoInjector;
using Microsoft.Win32;

namespace Loader
{
    public static class Program
    {
        public static void Main()
        {
            // setup console
            Window.SetSize(80, 20);
            Window.SetTitle("NetWare Loader");

            while (true)
            {
                // display banner
                Console.Clear();
                Banner.Display();

                char selection = IO.WaitForInput(
                    " [1] Load\n [2] Clean\n [3] Exit\n\n",
                    ConsoleColor.DarkGray
                );

                // display banner without options
                Console.Clear();
                Banner.Display();

                // execute selected option
                switch (selection)
                {
                    case '1':
                        Load();
                        break;
                    case '2':
                        Clean();
                        break;
                    case '3':
                        return;
                }
            }
        }

        public static void Load()
        {
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
            Resolve.CheatPath();
            Resolve.SteamPaths();
            if (!Data.ArePathsValid())
            {
                Message.ShowError("000");
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
                Message.ShowError("001");
                return;
            }

            // disable/patch anticheat
            IO.Puts("Patching AntiCheat . . .", ConsoleColor.DarkYellow);
            if (!AntiCheat.Patch())
            {
                Message.ShowError("002");
                return;
            }

            // start 1v1.lol
            IO.Puts("Starting 1v1.LOL . . .", ConsoleColor.DarkYellow);
            if (!Manager.StartGameProcess())
            {
                Message.ShowError("003");
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
                Message.ShowError("004");
                return;
            }

            // wait for input to exit
            IO.Puts("\nDone!", ConsoleColor.DarkGreen);
            IO.WaitForInput("\nPress any key to continue . . .", ConsoleColor.DarkGray);
        }

        public static void Clean()
        {
            // delete 1v1.lol registry data
            IO.Puts("Deleting registry data . . .", ConsoleColor.DarkYellow);
            if (Registry.CurrentUser.OpenSubKey("HKEY_CURRENT_USER\\SOFTWARE\\JustPlay.LOL") != null)
                Registry.CurrentUser.DeleteSubKeyTree("HKEY_CURRENT_USER\\SOFTWARE\\JustPlay.LOL");

            // wait for input to exit
            IO.Puts("Done!", ConsoleColor.DarkGreen);
            IO.WaitForInput("\nPress any key to continue . . .", ConsoleColor.DarkGray);
        }
    }
}
