using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GitHubConnector_Aventisia.Services
{
    public class GitHubService
    {
        private readonly HttpClient _httpClient;
        private readonly string _token;

        public GitHubService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _token = config["GitHub:Token"];

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _token);

            _httpClient.DefaultRequestHeaders.UserAgent
                .Add(new ProductInfoHeaderValue("MyApp", "1.0"));
        }

        public async Task<object> GetRepositories(string username)
        {
            var response = await _httpClient.GetAsync(
                $"https://api.github.com/users/{username}/repos");

            if (!response.IsSuccessStatusCode)
                return "Error fetching repos";

            var json = await response.Content.ReadAsStringAsync();

            var repos = JArray.Parse(json);

            var result = repos.Select(r => new
            {
                name = r["name"]?.ToString(),
                url = r["html_url"]?.ToString(),
                isPrivate = r["private"]?.ToObject<bool>()
            });

            return result;
        }
        //public async Task<string> CreateIssue(string owner, string repo, string title, string body)
        //{
        //    var data = new
        //    {
        //        title = title,
        //        body = body
        //    };

        //    var json = JsonConvert.SerializeObject(data);

        //    var content = new StringContent(json, Encoding.UTF8, "application/json");

        //    var response = await _httpClient.PostAsync(
        //        $"https://api.github.com/repos/{owner}/{repo}/issues",
        //        content);

        //    if (!response.IsSuccessStatusCode)
        //        return "Error creating issue";

        //    return await response.Content.ReadAsStringAsync();
        //}

        public async Task<string> CreateIssue(string owner, string repo, string title, string body)
        {
            var data = new
            {
                title = title,
                body = body
            };

            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(
                $"https://api.github.com/repos/{owner}/{repo}/issues",
                content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return $"Error creating issue: {error}";
            }

            var jsonIssue = await response.Content.ReadAsStringAsync();

            var issue = JObject.Parse(jsonIssue);

            var result = new
            {
                title = issue["title"]?.ToString(),
                url = issue["html_url"]?.ToString(),
                state = issue["state"]?.ToString(),
                number = issue["number"]?.ToObject<int>()
            };

            return JsonConvert.SerializeObject(result, Formatting.Indented);
        }
    }
}
