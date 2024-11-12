namespace Loader;

public static class Program
{
    // properties
    public static CancellationTokenSource cancellationToken = new();
    public static Task runningFunction;

    public static Window window;

    // main
    [STAThread]
    public static void Main()
    {
        ApplicationConfiguration.Initialize();
        window = new Window();
        Application.Run(window);
    }
}
