using System.Text.Json.Serialization;
public class Asset
{
  public string? browser_download_url { get; set; }
}

public class Author
{
  public string? login { get; set; }
}

public class Release
{
  public List<Asset>? assets { get; set; }
  public string? tag_name { get; set; }
  public string? created_at { get; set; }
}