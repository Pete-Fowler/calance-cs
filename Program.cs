using System.Net.Http.Headers;
using System.Text.Json;

using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/json"));
client.DefaultRequestHeaders.Add("User-Agent", "Bootstrap release to csv script");

var releases = await ProcessReleasesAsync(client);

foreach (var release in releases ?? Enumerable.Empty<Release>()) {
  Console.WriteLine(release.created_at);
  Console.WriteLine(release.tag_name);
  Console.WriteLine(release.assets?[0].browser_download_url);
}


static async Task<List<Release>> ProcessReleasesAsync(HttpClient client)
{
await using Stream stream =
    await client.GetStreamAsync("https://api.github.com/repos/twbs/bootstrap/releases");

var releases =
    await JsonSerializer.DeserializeAsync<List<Release>>(stream);
    return releases ?? new();
   
}