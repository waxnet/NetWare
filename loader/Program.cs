using System.Diagnostics;
using SharpMonoInjector;

namespace Loader
{
    public static class Program
    {
        public static void Main()
        {
            // setup console
            Window.SetSize(80, 20);
            Window.SetTitle("NetWare Loader");
            Window.DisableResizing();
            Console.CursorVisible = false;

            while (true)
            {
                // display banner
                Console.Clear();
                Banner.Display();

                char selection = IO.WaitForInput(
                    " [1] Load\n [2] Spoof\n [3] Exit\n\n",
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
                        Spoof();
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
            Resolve.TempPath();
            if (!Data.ArePathsValid())
            {
                Message.ShowError("000A");
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
                Message.ShowError("001A");
                return;
            }

            // disable/patch anticheat
            IO.Puts("Patching AntiCheat . . .", ConsoleColor.DarkYellow);
            if (!AntiCheat.Patch())
            {
                Message.ShowError("002A");
                return;
            }

            // download unstripped files
            IO.Puts("Unstripping files . . .", ConsoleColor.DarkYellow);
            if (!GameFiles.Unstrip())
            {
                Message.ShowError("003A");
                return;
            }

            // start 1v1.lol
            IO.Puts("Starting 1v1.LOL . . .", ConsoleColor.DarkYellow);
            if (!Manager.StartGameProcess())
            {
                Message.ShowError("004A");
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
                Message.ShowError("005A");
                return;
            }

            // wait for input to exit
            IO.Puts("\nDone!", ConsoleColor.DarkGreen);
            IO.WaitForInput("\nPress any key to continue . . .", ConsoleColor.DarkGray);
        }

        public static void Spoof()
        {
            // generate account
            IO.Puts("Generating account . . .", ConsoleColor.DarkYellow);
            Task<(string, bool)> generationTask = Network.GenerateAccount();
            generationTask.Wait();
            (string, bool) accountData = generationTask.Result;
            if (!accountData.Item2)
            {
                Message.ShowError("000B");
                return;
            }

            // set refresh token
            IO.Puts("Setting refresh token . . .", ConsoleColor.DarkYellow);
            string refreshTokenKey = RegEdit.FindRefreshTokenKey();
            if (refreshTokenKey == "")
            {
                Message.ShowError("001B");
                return;
            }
            if (!RegEdit.SetRefreshToken(refreshTokenKey, accountData.Item1))
            {
                Message.ShowError("002B");
                return;
            }

            // set sign in platform
            IO.Puts("Setting sign in platform . . .", ConsoleColor.DarkYellow);
            string signInPlatformKey = RegEdit.FindSignInPlatformKey();
            if (signInPlatformKey == "")
            {
                Message.ShowError("003B");
                return;
            }
            if (!RegEdit.SetSignInPlatform(signInPlatformKey))
            {
                Message.ShowError("004B");
                return;
            }

            // wait for input to exit
            IO.Puts("Done!", ConsoleColor.DarkGreen);
            IO.WaitForInput("\nPress any key to continue . . .", ConsoleColor.DarkGray);
        }
    }
}
