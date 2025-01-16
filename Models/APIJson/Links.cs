using System.Text.Json.Serialization;

namespace BitbucketCommitPlotter.Models.APIJson;

public class Links
{
    [JsonPropertyName("self")]
    public List<Self>? Self { get; set; }
}