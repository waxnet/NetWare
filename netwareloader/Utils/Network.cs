using System.IO.Compression;
using System.Net;

namespace NetWareLoader
{
    public static class Network
    {
        public static bool DownloadFile(string url, string path)
        {
            // create client
            using WebClient client = new();

            // download file
            try {
                client.DownloadFile(url, path);
            }
            catch { return false; }
            finally
            {
                client.Dispose();
            }

            return true;
        }
    }
}
