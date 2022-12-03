using System.Net.Http.Headers;
using System.Text.Json;

using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/json"));
client.DefaultRequestHeaders.Add("User-Agent", "Bootstrap release to csv script");

await ProcessReleasesAsync(client);

static async Task ProcessReleasesAsync(HttpClient client)
{
await using Stream stream =
    await client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
var repositories =
    await JsonSerializer.DeserializeAsync<List<Release>>(stream);
}

//         "https://api.github.com/repos/twbs/bootstrap/releases");
