using System.IO.Compression;

namespace Loader
{
    public static class GameFiles
    {
        public static bool Unstrip()
        {
            // get and check path
            string gameFilesPath = Path.Combine(Data.gamePath, "1v1_LOL_Data\\Managed");
            string unstrippedFilesPath = Path.Combine(Data.tempPath, "unstripped_files.zip");

            if (!Directory.Exists(gameFilesPath))
                return false;

            // download unstripped files
            bool downloadedFile = Network.DownloadFile(
                "https://unity.bepinex.dev/libraries/2021.3.37.zip",
                unstrippedFilesPath
            );
            if (!downloadedFile)
                return false;

            // extract files
            try {
                ZipFile.ExtractToDirectory(unstrippedFilesPath, gameFilesPath, true);
            } catch {
                return false;
            }

            // cleanup
            File.Delete(unstrippedFilesPath);

            return true;
        }
    }
}
