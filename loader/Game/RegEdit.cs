using Microsoft.Win32;

namespace Loader;

public static class RegEdit
{
    public static bool SetRefreshToken(string valueName, string value)
    {
        // values
        var encodedValue = System.Text.Encoding.UTF8.GetBytes(value);
        bool isSet = false;

        // set the refresh token registry value
        using var registryKeyA = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\JustPlay.LOL\1v1.LOL", true);
        if (registryKeyA is not null)
        {
            try
            {
                registryKeyA.SetValue(valueName, encodedValue, RegistryValueKind.Binary);
                isSet = true;
            } catch { }
        }

        using var registryKeyB = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\JustPlay.LOL\1v1_LOL", true);
        if (registryKeyB is not null)
        {
            try
            {
                registryKeyB.SetValue(valueName, encodedValue, RegistryValueKind.Binary);
                isSet = true;
            } catch { }
        }
        return isSet;
    }

    public static bool SetSignInPlatform(string valueName)
    {
        // values
        byte[] googleBytes = [0x47, 0x6F, 0x6F, 0x67, 0x6C, 0x65, 0x00];
        bool isSet = false;

        // set the registry value with the specific byte sequence
        using var registryKeyA = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\JustPlay.LOL\1v1.LOL", true);
        if (registryKeyA is not null)
        {
            try {
                registryKeyA.SetValue(valueName, googleBytes, RegistryValueKind.Binary);
                isSet = true;
            } catch { }
        }

        using var registryKeyB = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\JustPlay.LOL\1v1_LOL", true);
        if (registryKeyB is not null)
        {
            try
            {
                registryKeyB.SetValue(valueName, googleBytes, RegistryValueKind.Binary);
                isSet = true;
            } catch { }
        }
        return isSet;
    }

    public static string FindRefreshTokenKey()
    {
        // open the registry key for 1v1.lol
        var registryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\JustPlay.LOL\1v1.LOL");
        
        if (registryKey == null) {
            registryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\JustPlay.LOL\1v1_LOL");
            
            if (registryKey == null)
                return "";
        }

        // get all value names in the registry key
        string[] registryValues = registryKey.GetValueNames();
        registryKey.Dispose();

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
        var registryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\JustPlay.LOL\1v1.LOL");

        if (registryKey == null) {
            registryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\JustPlay.LOL\1v1_LOL");

            if (registryKey == null)
                return "";
        }

        // get all value names in the registry key
        string[] registryValues = registryKey.GetValueNames();
        registryKey.Dispose();

        // find the value containing "FirebaseSignInPlatform"
        foreach (string registryValue in registryValues)
            if (registryValue.Contains("FirebaseSignInPlatform"))
                return registryValue;

        // return default if not found
        return "FirebaseSignInPlatform_h2279995555";
    }
}
