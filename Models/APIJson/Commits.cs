using System.Text.Json.Serialization;

namespace BitbucketCommitPlotter.Models.APIJson;

public class Commits
{
    [JsonPropertyName("values")]
    public List<Commit>? Values { get; set; }
}