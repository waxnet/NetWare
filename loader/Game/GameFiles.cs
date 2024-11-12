using System.IO.Compression;

namespace Loader;

public static class GameFiles
{
    public static bool InstallBepInEx()
    {
        // paths
        string bepinexZipPath = Path.Combine(Data.gamePath, "bepinex.zip");

        // check path
        if (Directory.Exists(Path.Combine(Data.gamePath, "doorstop_config.ini")))
            return true;

        // download bepinex
        bool downloadedFile = Network.DownloadFile(
            "https://builds.bepinex.dev/projects/bepinex_be/725/BepInEx-Unity.IL2CPP-win-x64-6.0.0-be.725%2Be1974e2.zip",
            bepinexZipPath
        );
        if (!downloadedFile)
            return false;

        // extract files
        try {
            using var archive = ZipFile.OpenRead(bepinexZipPath);

            foreach (var entry in archive.Entries)
            {
                string destinationPath = Path.Combine(Data.gamePath, entry.FullName);

                if (entry.FullName.EndsWith("/"))
                {
                    Directory.CreateDirectory(destinationPath);
                } else {
                    Directory.CreateDirectory(Path.GetDirectoryName(destinationPath) ?? string.Empty);
                    entry.ExtractToFile(destinationPath, overwrite: true);
                }
            }
        } catch {
            return false;
        }

        // cleanup
        File.Delete(bepinexZipPath);

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
