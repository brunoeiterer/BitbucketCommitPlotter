using System.Text.Json;
using BitbucketCommitPlotter.Models.ConfigurationJson;

namespace BitbucketCommitPlotter.Utils;

public static class ConfigurationReader
{
    public static Configuration ReadConfiguration(string configurationFilePath = "Config.json") =>
        JsonSerializer.Deserialize<Configuration>(File.ReadAllText(configurationFilePath)) ??
        throw new JsonException("Invalid configuration file.");
}