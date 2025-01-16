using BitbucketCommitPlotter.Models;
using BitbucketCommitPlotter.Utils;

var configuration = ConfigurationReader.ReadConfiguration();

/* Get commit data from Bitbucket API */
var commitDataDownloader = new CommitDataDownloader(configuration.Projects, configuration.BaseUrl, configuration.Username, 
    configuration.Password, configuration.Author);
var commitData = commitDataDownloader.GetCommitData();
File.WriteAllText("CommitData.json", commitData.ToJson());

/* Get commit data from json file */
// var commitData = CommitData.FromJson(File.ReadAllText(@"C:\Bruno\Code\BitbucketBitbucketCommitPlotter\CommitData.json"));

commitData.PlotCommitGraph(new DateTime(configuration.Year, 1, 1), new DateTime(configuration.Year, 12, 31), configuration.PlotCommiterName);