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
    await client.GetStreamAsync("https://api.github.com/repos/twbs/bootstrap/releases");
var releases =
    await JsonSerializer.DeserializeAsync<List<Release>>(stream);

    foreach (var release in releases ?? Enumerable.Empty<Release>())
      Console.Write(release.tag_name);
}

//         "https://api.github.com/repos/twbs/bootstrap/releases");
