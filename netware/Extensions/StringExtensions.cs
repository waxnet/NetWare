using System.IO;

namespace NetWare.Extensions;

public static class StringExtensions
{
    public static string CombineWith(this string path, params string[] paths)
    {
        return Path.Combine([path, .. paths]);
    }

    public static string ChangeExtension(this string path, string extension)
    {
        return Path.ChangeExtension(path, extension);
    }
}
