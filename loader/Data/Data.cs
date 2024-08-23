namespace Loader
{
    public static class Data
    {
        // data
        public static string? cheatPath;
        public static string? steamPath;
        public static string? gamePath;
        public static string? tempPath;

        // methods
        public static bool ArePathsValid()
        {
            return (
                cheatPath != null &&
                steamPath != null &&
                gamePath != null &&
                tempPath != null
            );
        }
    }
}
