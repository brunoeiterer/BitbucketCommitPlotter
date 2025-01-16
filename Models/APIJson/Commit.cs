using System.Text.Json.Serialization;

namespace BitbucketCommitPlotter.Models.APIJson;

public class Commit
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("displayId")]
    public string? DisplayId { get; set; }

    [JsonPropertyName("author")]
    public Author? Author { get; set; }

    [JsonPropertyName("authorTimestamp")]
    public long AuthorTimestamp { get; set; }

    [JsonPropertyName("committer")]
    public Committer? Committer { get; set; }
}