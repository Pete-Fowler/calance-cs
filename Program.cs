using System.Net.Http.Headers;
using System.Text.Json;

using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/json"));
client.DefaultRequestHeaders.Add("User-Agent", "Bootstrap release to csv script");

var releases = await ProcessReleasesAsync(client);

foreach (var release in releases ?? Enumerable.Empty<Release>())
   Console.Write(release.Name);

static async Task<List<Release>> ProcessReleasesAsync(HttpClient client)
{
await using Stream stream =
    await client.GetStreamAsync("https://api.github.com/repos/twbs/bootstrap/releases");
var releases =
    await JsonSerializer.DeserializeAsync<List<Release>>(stream);
    return releases ?? new();
   
}

//         "https://api.github.com/repos/twbs/bootstrap/releases");
//  foreach (var release in releases ?? Enumerable.Empty<Release>())
//    Console.Write(release.Name);