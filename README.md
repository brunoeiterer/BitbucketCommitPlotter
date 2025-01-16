[![pt-br](https://img.shields.io/badge/lang-pt--br-green.svg)](https://github.com/brunoeiterer/BitbucketCommitPlotter/blob/master/README-pt-br.md)

# BitbucketCommitPlotter
A c# app to get commit data from Bitbucket's API and plot like the github contribution graph

Here is mine from 2024:
![CommitHistory2024](https://github.com/brunoeiterer/BitbucketCommitPlotter/blob/main/CommitData-Bruno%20Vale%20Barbosa%20Eiterer-2024.png?raw=true)

## How to run the project

### Prerequisites
* .NET SDK 8 or higher installed

### Steps
* Clone the repository:
```
git clone https://github.com/brunoeiterer/BitbucketCommitPlotter.git
cd BitbucketCommitPlotter
```
* Setup a configuration file named Config.json at the root of the repository. It should have the following attributes:
  * Projects contains attributes where the name of the attribute is a Project in bitbucket and the value is a list of repositories to search
  * Year is the year to filter for the commits
  * BaseUrl is the url address of the bitbucket server you want to get the commits
  * Username is the username to authenticate in the bitbucket server
  * Password is the password to authenticate in the bitbucket server
  * Author is the author to filter the commits
  * PlotCommiterName is the name to be displayed in the generated commit graph
```
"Projects": {
    "Project1": ["repo1", "repo2"],
    "Project2": ["repo3", "repo4"]
},
"Year": 2024,
"BaseUrl": "https://companygit.com",
"Username": "user",
"Password": "pass",
"Author": "author",
"PlotCommiterName": "Full Name"
```
* Run:
```
dotnet run
```
