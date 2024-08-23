using Microsoft.Win32;

namespace Loader
{
    public static class RegEdit
    {
        public static bool SetRefreshToken(string valueName, string value)
        {
            // open the registry key for 1v1.lol with write access
            using RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\JustPlay.LOL\1v1.LOL", true);
            if (registryKey == null)
                return false;

            // set the registry value as a binary value
            try{
                registryKey.SetValue(valueName, System.Text.Encoding.UTF8.GetBytes(value), RegistryValueKind.Binary);
            } catch {
                return false;
            }
            return true;
        }

        public static bool SetSignInPlatform(string valueName)
        {
            // open the registry key for 1v1.lol with write access
            using RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\JustPlay.LOL\1v1.LOL", true);
            if (registryKey == null)
                return false;

            // set the registry value with the specific byte sequence
            try {
                byte[] googleBytes = [0x47, 0x6F, 0x6F, 0x67, 0x6C, 0x65, 0x00];
                registryKey.SetValue(valueName, googleBytes, RegistryValueKind.Binary);
            } catch {
                return false;
            }
            return true;
        }

        public static string FindRefreshTokenKey()
        {
            // open the registry key for 1v1.lol
            using RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\JustPlay.LOL\1v1.LOL");
            if (registryKey == null)
                return "";

            // get all value names in the registry key
            string[] registryValues = registryKey.GetValueNames();

            // find the value containing "firebase_refresh_token"
            foreach (string registryValue in registryValues)
                if (registryValue.Contains("firebase_refresh_token"))
                    return registryValue;

            // return default if not found
            return "firebase_refresh_token_h1193372590";
        }

        public static string FindSignInPlatformKey()
        {
            // open the registry key for 1v1.lol
            using RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\JustPlay.LOL\1v1.LOL");
            if (registryKey == null)
                return "";

            // get all value names in the registry key
            string[] registryValues = registryKey.GetValueNames();

            // find the value containing "FirebaseSignInPlatform"
            foreach (string registryValue in registryValues)
                if (registryValue.Contains("FirebaseSignInPlatform"))
                    return registryValue;

            // return default if not found
            return "FirebaseSignInPlatform_h2279995555";
        }
    }
}
