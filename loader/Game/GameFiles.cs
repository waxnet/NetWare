using System.IO.Compression;
using System.Net;

namespace Loader;

public static class GameFiles
{
    public static bool Revert()
    {
        // paths
        string gameFilesPath = Path.Combine(Data.tempPath, "1v1_LOL.zip");

        // download game files
        try {
            using var client = new WebClient();
            client.DownloadFile(
                "https://github.com/waxnet/NetWare/releases/download/loader_v3/GAMEFILES.zip",
                gameFilesPath
            );
        } catch {
            return false;
        }

        // extract game files and cleanup
        try {
            if (Directory.Exists(Data.gamePath))
                Directory.Delete(Data.gamePath, true);
            ZipFile.ExtractToDirectory(gameFilesPath, Data.gamePath);
        } catch {
            return false;
        } finally {
            if (File.Exists(gameFilesPath))
                File.Delete(gameFilesPath);
        }

        return true;
    }

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
            using var archive = ZipFile.OpenRead(unstrippedFilesPath);

            var entry = archive.GetEntry("UnityEngine.IMGUIModule.dll");

            if (entry != null)
                entry.ExtractToFile(
                    Path.Combine(gameFilesPath, entry.FullName),
                    true
                );
            else
                return false;
        } catch {
            return false;
        }

        // cleanup
        File.Delete(unstrippedFilesPath);

        return true;
    }
}
