using SharpMonoInjector;
using System.Diagnostics;

namespace Loader;

public static class Program
{
    // properties
    public static CancellationTokenSource cancellationToken = new();
    public static Task runningFunction;

    private static Window window;

    // main
    [STAThread]
    public static void Main()
    {
        ApplicationConfiguration.Initialize();
        window = new Window();
        Application.Run(window);
    }

    // load
    public static void Load(CancellationToken token)
    {
        window.ClearConsole();

        // check if 1v1.lol is already running and shut it down
        Process? gameProcessA = Manager.FindGameProcess();
        if (gameProcessA != null)
        {
            window.AddConsoleLog("Killing 1v1.LOL process . . .", Brushes.Orange);
            gameProcessA.Kill();
            gameProcessA.WaitForExit();
        }
        if (token.IsCancellationRequested) return;

        // search game and steam path
        window.AddConsoleLog("Searching paths . . .", Brushes.Orange);
        Resolve.CheatPath();
        Resolve.SteamPaths();
        Resolve.TempPath();
        if (!Data.ArePathsValid())
        {
            window.AddConsoleLog("Error : restart steam.", Brushes.Red);
            return;
        }
        if (token.IsCancellationRequested) return;

        // download latest cheat version
        window.AddConsoleLog("Downloading latest cheat version . . .", Brushes.Orange);

        if (File.Exists(Data.cheatPath))
            File.Delete(Data.cheatPath);

        bool downloadedFile = Network.DownloadFile(
            "https://raw.githubusercontent.com/waxnet/NetWare/main/.bin/NetWare.dll",
            Data.cheatPath
        );
        if (!downloadedFile)
        {
            window.AddConsoleLog("Error : check your connection and disable antivirus.", Brushes.Red);
            return;
        }
        if (token.IsCancellationRequested) return;

        // disable/patch anticheat
        window.AddConsoleLog("Patching AntiCheat . . .", Brushes.Orange);
        if (!AntiCheat.Patch())
        {
            window.AddConsoleLog("Error : disable antivirus.", Brushes.Red);
            return;
        }
        if (token.IsCancellationRequested) return;

        // download unstripped files
        window.AddConsoleLog("Unstripping files . . .", Brushes.Orange);
        if (!GameFiles.Unstrip())
        {
            window.AddConsoleLog("Error : check your connection and disable antivirus.", Brushes.Red);
            return;
        }
        if (token.IsCancellationRequested) return;

        // start 1v1.lol
        window.AddConsoleLog("Starting 1v1.LOL . . .", Brushes.Orange);
        if (!Manager.StartGameProcess())
        {
            window.AddConsoleLog("Error : check your 1v1.LOL installation.", Brushes.Red);
            return;
        }
        if (token.IsCancellationRequested) return;

        // wait for injection
        window.AddConsoleLog("Waiting for 1v1.LOL to load . . .", Brushes.Orange);

        Process? gameProcessB = Manager.FindGameProcess();

        int maxIterations = 180;
        int currentIterations = 0;

        while (!token.IsCancellationRequested)
        {
            Thread.Sleep(1000);

            currentIterations++;
            if (currentIterations == maxIterations)
            {
                window.AddConsoleLog("Error : could not check 1v1.LOL loading progress.", Brushes.Red);
                
                if (gameProcessB is not null) {
                    window.AddConsoleLog($"Info : {Memory.GetLoadedModules(gameProcessB.Id)}.", Brushes.Red);
                } else {
                    window.AddConsoleLog("Info : couldn't find 1v1.LOL process.", Brushes.Red);
                }
                
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
        window.AddConsoleLog("Injecting . . .", Brushes.Orange);

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
            window.AddConsoleLog("Error : disable antivirus.", Brushes.Red);
            return;
        }
        if (token.IsCancellationRequested) return;

        // finish
        window.AddConsoleLog();
        window.AddConsoleLog("Done!", Brushes.Green);
    }

    // spoof
    public static void Spoof(CancellationToken token)
    {
        window.ClearConsole();

        // generate account
        window.AddConsoleLog("Generating account . . .", Brushes.Orange);
        Task<(string, bool)> generationTask = Network.GenerateAccount();
        generationTask.Wait();
        (string, bool) accountData = generationTask.Result;
        if (!accountData.Item2)
        {
            window.AddConsoleLog("Error : check your connection and disable antivirus.", Brushes.Red);
            return;
        }
        if (token.IsCancellationRequested) return;

        // set refresh token
        window.AddConsoleLog("Setting refresh token . . .", Brushes.Orange);
        string refreshTokenKey = RegEdit.FindRefreshTokenKey();
        if (refreshTokenKey == "")
        {
            window.AddConsoleLog("Error : check your 1v1.LOL installation.", Brushes.Red);
            return;
        }
        if (!RegEdit.SetRefreshToken(refreshTokenKey, accountData.Item1))
        {
            window.AddConsoleLog("Error : disable antivirus or run as admin.", Brushes.Red);
            return;
        }
        if (token.IsCancellationRequested) return;

        // set sign in platform
        window.AddConsoleLog("Setting sign in platform . . .", Brushes.Orange);
        string signInPlatformKey = RegEdit.FindSignInPlatformKey();
        if (signInPlatformKey == "")
        {
            window.AddConsoleLog("Error : check your 1v1.LOL installation.", Brushes.Red);
            return;
        }
        if (!RegEdit.SetSignInPlatform(signInPlatformKey))
        {
            window.AddConsoleLog("Error : disable antivirus or run as admin.", Brushes.Red);
            return;
        }
        if (token.IsCancellationRequested) return;

        // wait for input to exit
        window.AddConsoleLog();
        window.AddConsoleLog("Done!", Brushes.Green);
    }
}
