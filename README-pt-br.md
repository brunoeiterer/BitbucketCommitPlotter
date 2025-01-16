[![en](https://img.shields.io/badge/lang-en-red.svg)](https://github.com/brunoeiterer/BitbucketCommitPlotter/blob/master/README.md)

# BitbucketCommitPlotter
Um aplicativo em c# para obter dados de commits da API do Bitbucket e plotar um gráfico semelhante ao gráfico de contribuições do github.

Esse é o meu gráfico de 2024:
![CommitHistory2024](https://github.com/brunoeiterer/BitbucketCommitPlotter/blob/main/CommitData-Bruno%20Vale%20Barbosa%20Eiterer-2024.png?raw=true)

## Como rodar o projeto

### Pré-Requisitos
* .NET SDK 8 ou maior instalado

### Passo a Passo
* Clone o repositório:
```
git clone https://github.com/brunoeiterer/BitbucketCommitPlotter.git
cd BitbucketCommitPlotter
```
* Crie um arquivo de configuração com o nome Config.json na raíz do repositório com os seguintes atributos:
  * Projects deve conter atributos cujos nomes são um projeto no Bitbucket e o valor é uma lista de repositórios para buscar pelos commits
  * Year é o ano para filtrar os commits
  * BaseUrl é a url do servidor Bitbucket para buscar os commits
  * Username é o usuário para autenticar no servidor Bitbucket
  * Password é a senha para autenticar no servidor Bitbucket
  * Author é o autor para filtrar os commits
  * PlotCommiterName é o nome para mostrar no título do gráfico
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
* Rode:
```
dotnet run
```
