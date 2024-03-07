using System.Security.Cryptography;

namespace NetWareLoader.Game
{
    public static class Checksum
    {
        public static string currentChecksum()
        {
            Resolve.SteamPaths();
            string originalAssembly = Path.Combine(Data.gamePath, "1v1_LOL_Data\\Managed\\ACTk.Runtime.dll");
            try
            {
                using (var sha256 = SHA256.Create())
                {
                    using (var stream = File.OpenRead(originalAssembly))
                    {
                        var hash = sha256.ComputeHash(stream);
                        var currentChecksum = BitConverter.ToString(hash).Replace("-", string.Empty);
                        stream.Close();
                        return currentChecksum;
                    }
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Couldn't read the ACTk.Runtime.dll file.");
                return null;
            }
        }
    }
}