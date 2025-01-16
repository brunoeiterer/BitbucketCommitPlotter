namespace BitbucketCommitPlotter.Models.ConfigurationJson;

public class Configuration
{
    public required Dictionary<string, List<string>> Projects { get; set; }
    public required int Year { get; set; }
    public required string BaseUrl { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Author { get; set; }
    public required string PlotCommiterName { get; set; }
}