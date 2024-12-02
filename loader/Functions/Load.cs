using SharpMonoInjector;
using System.Diagnostics;

namespace Loader.Functions;

public static class Load
{
    public static void Normal(CancellationToken token)
    {
        Program.window.ClearConsole();

        // check if 1v1.lol is already running and shut it down
        Process? gameProcessA = Manager.FindGameProcess();
        if (gameProcessA != null)
        {
            Program.window.AddConsoleLog("Killing 1v1.LOL process . . .", Brushes.Orange);
            gameProcessA.Kill();
            gameProcessA.WaitForExit();
        }
        if (token.IsCancellationRequested) return;

        // search game and steam path
        Program.window.AddConsoleLog("Searching paths . . .", Brushes.Orange);
        Resolve.NormalCheatPath();
        Resolve.SteamPaths();
        Resolve.TempPath();
        if (!Data.NormalArePathsValid())
        {
            Program.window.AddConsoleLog("Error : restart steam.", Brushes.Red);
            return;
        }
        if (token.IsCancellationRequested) return;

        // revert game files if needed
        string checkPath = Path.Combine(Data.gamePath, "baselib.dll");
        if (File.Exists(checkPath))
        {
            Program.window.AddConsoleLog("Reverting game files . . .", Brushes.Orange);

            // delete current version
            try {
                Directory.Delete(Data.gamePath, true);
                Directory.CreateDirectory(Data.gamePath);
            } catch {
                Program.window.AddConsoleLog("Error : make sure your antivirus is disabled.", Brushes.Red);
                return;
            }
            if (token.IsCancellationRequested) return;

            // revert files
            try {
                if (!GameFiles.Revert())
                {
                    Program.window.AddConsoleLog("Error : check your connection and disable antivirus.", Brushes.Red);
                    return;
                }
            } catch {
                return;
            }
            if (token.IsCancellationRequested) return;
        }

        // download latest cheat version
        Program.window.AddConsoleLog("Downloading latest cheat version . . .", Brushes.Orange);

        if (File.Exists(Data.cheatPath))
            File.Delete(Data.cheatPath);

        bool downloadedFile = Network.DownloadFile(
            "https://raw.githubusercontent.com/waxnet/NetWare/main/.bin/NetWare.dll",
            Data.cheatPath
        );
        if (!downloadedFile)
        {
            Program.window.AddConsoleLog("Error : check your connection and disable antivirus.", Brushes.Red);
            return;
        }
        if (token.IsCancellationRequested) return;

        // disable/patch anticheat
        Program.window.AddConsoleLog("Patching AntiCheat . . .", Brushes.Orange);
        if (!AntiCheat.Patch())
        {
            Program.window.AddConsoleLog("Error : make sure your antivirus is disabled.", Brushes.Red);
            return;
        }
        if (token.IsCancellationRequested) return;

        // download unstripped files
        Program.window.AddConsoleLog("Unstripping files . . .", Brushes.Orange);
        if (!GameFiles.Unstrip())
        {
            Program.window.AddConsoleLog("Error : check your connection and disable antivirus.", Brushes.Red);
            return;
        }
        if (token.IsCancellationRequested) return;

        // start 1v1.lol
        Program.window.AddConsoleLog("Starting 1v1.LOL . . .", Brushes.Orange);
        if (!Manager.StartGameProcess())
        {
            Program.window.AddConsoleLog("Error : check your 1v1.LOL installation.", Brushes.Red);
            return;
        }
        if (token.IsCancellationRequested) return;

        // wait for injection
        Program.window.AddConsoleLog("Waiting for 1v1.LOL to load . . .", Brushes.Orange);

        Process? gameProcessB = Manager.FindGameProcess();

        int maxIterations = 180;
        int currentIterations = 0;

        while (!token.IsCancellationRequested)
        {
            Thread.Sleep(1000);

            currentIterations++;
            if (currentIterations == maxIterations)
            {
                Program.window.AddConsoleLog("Error : could not check 1v1.LOL loading progress.", Brushes.Red);
                return;
            }

            if (gameProcessB is null)
            {
                gameProcessB = Manager.FindGameProcess();
                continue;
            }

            if (Memory.GetLoadedModules(gameProcessB.Id) >= 90)
                break;
        }
        if (token.IsCancellationRequested) return;

        // inject
        Program.window.AddConsoleLog("Injecting . . .", Brushes.Orange);

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
            Program.window.AddConsoleLog("Error : make sure your antivirus is disabled.", Brushes.Red);
            return;
        }
        if (token.IsCancellationRequested) return;

        // finish
        Program.window.AddConsoleLog();
        Program.window.AddConsoleLog("Done!", Brushes.Green);
    }
}
