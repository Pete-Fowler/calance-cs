using System.Net.Http.Headers;
using System.Text.Json;

using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
client.DefaultRequestHeaders.Add("User-Agent", "Bootstrap release to csv script");

var releases = await ProcessReleasesAsync(client);

List<string?> data = new List<string?>();
data.Add("Date, Name, Download Url");

foreach (var release in releases ?? Enumerable.Empty<Release>())
{
    data.Add(
        $"{release.created_at?.Substring(0, 10)}, {release.tag_name}, {release.assets?[0].browser_download_url}"
    );
}

// const csv = data.map((row) => row.join(",")).join("\n");

data.ForEach(i => Console.WriteLine(i));

static async Task<List<Release>> ProcessReleasesAsync(HttpClient client)
{
    await using Stream stream = await client.GetStreamAsync(
        "https://api.github.com/repos/twbs/bootstrap/releases"
    );

    var releases = await JsonSerializer.DeserializeAsync<List<Release>>(stream);
    return releases ?? new();
}
