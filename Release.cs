using System.Text.Json.Serialization;

public record class Release(
  [property: JsonPropertyName("created_at")] DateTime Date,
  [property: JsonPropertyName("tag_name")] string Name,
  [property: JsonPropertyName("browser_download_url")] string Download_Url
  );



