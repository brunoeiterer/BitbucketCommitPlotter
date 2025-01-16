using System.Text.Json.Serialization;

namespace BitbucketCommitPlotter.Models.APIJson;

public class Self
{
    [JsonPropertyName("href")]
    public string? Href { get; set; }
}