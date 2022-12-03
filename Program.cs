using System.Net.Http.Headers;

using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/json"));
client.DefaultRequestHeaders.Add("User-Agent", "Bootstrap release to csv script");

await ProcessReleasesAsync(client);

static async Task ProcessReleasesAsync(HttpClient client)
{
     var json = await client.GetStringAsync(
         "https://api.github.com/repos/twbs/bootstrap/releases");

     Console.Write(json);
}