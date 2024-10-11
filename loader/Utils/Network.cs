using System.Text.Json;
using System.Net;
using System.Text;

namespace Loader;

public static class Network
{
    // data
    private class ResponseStruct
    {
        public string kind { get; set; } = "";
        public string idToken { get; set; } = "";
        public string refreshToken { get; set; } = "";
        public string expiresIn { get; set; } = "";
        public string localID { get; set; } = "";
    }

    // methods
    public static bool DownloadFile(string url, string path)
    {
        // create client
        using WebClient client = new();

        // download file
        try { client.DownloadFile(url, path); }
        catch { return false; }
        finally { client.Dispose(); }

        return true;
    }

    public static async Task<(string, bool)> GenerateAccount()
    {
        // create client
        using HttpClient client = new();

        // generate and check account
        StringContent generationContent = new("{\"key\": \"value\"}", Encoding.UTF8, "application/json");
        HttpResponseMessage generationResponse = await client.PostAsync(
            "https://www.googleapis.com/identitytoolkit/v3/relyingparty/signupNewUser?key=AIzaSyBPrAfspM9RFxuNuDtSyaOZ5YRjDBNiq5I&returnSecureToken=true",
            generationContent
        );

        if (!generationResponse.IsSuccessStatusCode)
            return ("", false);

        // parse request content
        string content = await generationResponse.Content.ReadAsStringAsync();

        var jsonResponse = JsonSerializer.Deserialize<ResponseStruct>(content);
        if (jsonResponse == null)
            return ("", false);;

        // return data and cleanup
        client.Dispose();
        return (jsonResponse.refreshToken, true);
    }
}
