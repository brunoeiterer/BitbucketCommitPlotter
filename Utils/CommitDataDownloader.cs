using System.Collections.Concurrent;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using BitbucketCommitPlotter.Models;
using BitbucketCommitPlotter.Models.APIJson;

namespace BitbucketCommitPlotter.Utils;

public class CommitDataDownloader(Dictionary<string, List<string>> projects, string baseUrl, string username, string password, string author)
{
    private readonly Dictionary<string, List<string>> Projects = projects;
    private readonly string BaseUrl = baseUrl;
    private readonly string Username = username;
    private readonly string Password = password;
    private readonly string Author = author;
    private HttpClient? _httpClient;
    private HttpClient HttpClient
    {
        get => _httpClient ??= new HttpClient
        {
            DefaultRequestHeaders =
            {
                Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
                    Encoding.ASCII.GetBytes($"{Username}:{Password}")))
            }
        };
    }
    private readonly BlockingCollection<List<Commit>> DownloadCommitDataQueue = [];

    public CommitData GetCommitData()
    {
        var commitData = new CommitData();
        _ = DownloadCommitDataAsync();
        foreach (var commits in DownloadCommitDataQueue.GetConsumingEnumerable())
        {
            foreach (var commit in commits)
            {
                var date = DateTimeOffset.FromUnixTimeMilliseconds(commit.AuthorTimestamp).DateTime.Date;
                if (commitData.TryGetValue(date, out var commitAmount))
                {
                    commitData[date] = commitAmount + 1;
                }
                else
                {
                    commitData[date] = 1;
                }
            }
        }

        return commitData;
    }

    private async Task DownloadCommitDataAsync()
    {
        foreach (var project in Projects)
        {
            foreach (var repository in project.Value)
            {
                var result = await HttpClient.GetAsync(GetRepoUrl(project.Key, repository));
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    Console.WriteLine($"Failed to download commits from repo {repository}");
                    continue;
                }

                var commits = await GetCommitsFromResponseAsync(result);
                if (commits.Count == 0)
                {
                    Console.WriteLine($"No commits from repo {repository}");
                    continue;
                }

                Console.WriteLine($"Downloaded {commits.Count} commits from repo {repository}");
                DownloadCommitDataQueue.Add(commits);
            }
        }

        DownloadCommitDataQueue.CompleteAdding();
    }

    private string GetRepoUrl(string project, string repository) =>
        $"{BaseUrl}/rest/api/1.0/projects/{project}/repos/{repository}/commits?limit=100000";

    private async Task<List<Commit>> GetCommitsFromResponseAsync(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        var commits = JsonSerializer.Deserialize<Commits>(content);
        return commits?.Values?.Where(v => v.Author?.Name == Author).ToList() ?? [];
    }
}