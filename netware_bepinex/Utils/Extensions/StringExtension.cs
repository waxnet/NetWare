using System.IO;

namespace NetWare.Extensions;

public static class StringExtension
{
    public static string CombineWith(this string path, params string[] paths) => Path.Combine([path, .. paths]);
    public static string ChangeExtension(this string path, string extension) => Path.ChangeExtension(path, extension);
}
