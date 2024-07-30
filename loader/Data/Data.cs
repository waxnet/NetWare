namespace Loader
{
    public static class Data
    {
        public static string? cheatPath;
        public static string? steamPath;
        public static string? gamePath;

        public static bool ArePathsValid()
        {
            return (
                cheatPath != null &&
                steamPath != null &&
                gamePath != null
            );
        }
    }
}
