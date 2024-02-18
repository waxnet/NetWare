namespace NetWareLoader
{
    public static class Data
    {
        public static string? injectorPath;
        public static string? loaderPath;
        public static string? cheatPath;
        public static string? steamPath;
        public static string? gamePath;

        public static bool ArePathsValid()
        {
            return (
                injectorPath != null &&
                loaderPath != null &&
                cheatPath != null &&
                steamPath != null &&
                gamePath != null
            );
        }
    }
}
