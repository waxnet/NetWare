namespace Loader.Functions;

public static class Spoof
{
    public static void Run(CancellationToken token)
    {
        Program.window.ClearConsole();

        // generate account
        Program.window.AddConsoleLog("Generating account . . .", Brushes.Orange);
        Task<(string, bool)> generationTask = Network.GenerateAccount();
        generationTask.Wait();
        (string, bool) accountData = generationTask.Result;
        if (!accountData.Item2)
        {
            Program.window.AddConsoleLog("Error : check your connection and disable antivirus.", Brushes.Red);
            return;
        }
        if (token.IsCancellationRequested) return;

        // set refresh token
        Program.window.AddConsoleLog("Setting refresh token . . .", Brushes.Orange);
        string refreshTokenKey = RegEdit.FindRefreshTokenKey();
        if (refreshTokenKey == "")
        {
            Program.window.AddConsoleLog("Error : check your 1v1.LOL installation or run as admin.", Brushes.Red);
            return;
        }
        if (!RegEdit.SetRefreshToken(refreshTokenKey, accountData.Item1))
        {
            Program.window.AddConsoleLog("Error : disable antivirus or run as admin.", Brushes.Red);
            return;
        }
        if (token.IsCancellationRequested) return;

        // set sign in platform
        Program.window.AddConsoleLog("Setting sign in platform . . .", Brushes.Orange);
        string signInPlatformKey = RegEdit.FindSignInPlatformKey();
        if (signInPlatformKey == "")
        {
            Program.window.AddConsoleLog("Error : check your 1v1.LOL installation or run as admin.", Brushes.Red);
            return;
        }
        if (!RegEdit.SetSignInPlatform(signInPlatformKey))
        {
            Program.window.AddConsoleLog("Error : disable antivirus or run as admin.", Brushes.Red);
            return;
        }
        if (token.IsCancellationRequested) return;

        // wait for input to exit
        Program.window.AddConsoleLog();
        Program.window.AddConsoleLog("Done!", Brushes.Green);
    }
}
