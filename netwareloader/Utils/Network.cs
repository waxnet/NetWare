using System.IO.Compression;
using System.Net;

namespace NetWareLoader
{
    public static class Network
    {
        public static bool DownloadZIP(string url, string path)
        {
            // create client
            using WebClient client = new();

            // paths
            string downloadPath = Path.Combine(path, "temp.zip");

            // download and extract file
            try {
                client.DownloadFile(url, downloadPath);
                ZipFile.ExtractToDirectory(downloadPath, path);
            } catch { return false; }
            finally {
                client.Dispose();
                File.Delete(downloadPath);
            }

            return true;
        }

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
