using Gameloop.Vdf.JsonConverter;
using Gameloop.Vdf.Linq;
using Gameloop.Vdf;
using Newtonsoft.Json.Linq;
using Microsoft.Win32;

namespace Loader
{
    public static class Resolve
    {
        // data
        private static readonly string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        // methods
        public static void CheatPath()
        {
            string cheatPath = Path.Combine(appdata, "NetWare\\loader");
            if (!Directory.Exists(cheatPath))
                Directory.CreateDirectory(cheatPath);
            Data.cheatPath = Path.Combine(cheatPath, "NetWare.dll");
        }

        public static void SteamPaths()
        {
            // get correct registry key
            string registryKey;
            if (Environment.Is64BitOperatingSystem)
                registryKey = "HKEY_LOCAL_MACHINE\\SOFTWARE\\WOW6432Node\\Valve\\Steam";
            else
                registryKey = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Valve\\Steam";

            // get steam path and check if it exists
            if (Registry.GetValue(registryKey, "InstallPath", null) is not string steamPath)
                return;
            Data.steamPath = steamPath;

            // get steam library folders
            VProperty libraryFolders = VdfConvert.Deserialize(File.ReadAllText(Path.Combine(steamPath, "steamapps\\libraryfolders.vdf")));

            for (int index = 0; index < libraryFolders.Value.Count(); index++)
            {
                // index to string
                VToken? libraryFolder = libraryFolders.Value[index.ToString()];
                if (libraryFolder == null)
                    continue;

                // check if 1v1.lol is in the steam apps
                var steamApps = JObject.Parse(libraryFolder["apps"].ToJson().ToString());
                if (!steamApps.ContainsKey("2305790"))
                    continue;

                // get possible 1v1.lol installation path
                string possibleDirectory = Path.Combine(libraryFolder["path"].ToString(), "steamapps\\common\\1v1.LOL");
                if (Directory.Exists(possibleDirectory))
                    Data.gamePath = possibleDirectory;
            }
        }

        public static void TempPath()
        {
            string tempPath = Path.Combine(appdata, "NetWare\\temp");
            if (!Directory.Exists(tempPath))
                Directory.CreateDirectory(tempPath);
            Data.tempPath = tempPath;
        }
    }
}
