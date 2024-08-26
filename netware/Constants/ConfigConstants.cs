using NetWare.Extensions;
using System;

namespace NetWare.Constants;

public static class ConfigConstants
{
    public static string ConfigFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).CombineWith("NetWare", "configs");
    public const string ConfigExtension = ".nwc";
}
