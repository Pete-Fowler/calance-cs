﻿using System.Net.Http.Headers;
using System.Text.Json;

using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/json"));
client.DefaultRequestHeaders.Add("User-Agent", "Bootstrap release to csv script");

var releases = await ProcessReleasesAsync(client);

List<string?> data = new List<string?>();

foreach (var release in releases ?? Enumerable.Empty<Release>())
{
  data.Add(release.created_at?.Substring(0, 10));
  data.Add(release.tag_name);
  data.Add(release.assets?[0].browser_download_url);
  // Console.WriteLine(release.created_at);
  // Console.WriteLine(release.tag_name);
  // Console.WriteLine(release.assets?[0].browser_download_url);
}
foreach (string? s in data)
{
  Console.WriteLine(s);

}

static async Task<List<Release>> ProcessReleasesAsync(HttpClient client)
{
  await using Stream stream =
      await client.GetStreamAsync("https://api.github.com/repos/twbs/bootstrap/releases");

  var releases =
      await JsonSerializer.DeserializeAsync<List<Release>>(stream);
  return releases ?? new();
}