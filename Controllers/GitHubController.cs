using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GitHubConnector_Aventisia.Services;
using GitHubConnector_Aventisia.Models;

namespace GitHubConnector_Aventisia.Controllers
{
    [ApiController]
    [Route("api/github")]
    public class GitHubController : ControllerBase
    {
        private readonly GitHubService _service;

        public GitHubController(GitHubService service)
        {
            _service = service;
        }

        [HttpGet("repos")]
        public async Task<IActionResult> GetRepos(string username)
        {
            var result = await _service.GetRepositories(username);
            return Ok(result);
        }

        [HttpPost("create-issue")]
        public async Task<IActionResult> CreateIssue([FromBody] IssueRequest request)
        {
            var result = await _service.CreateIssue(
                request.Owner,
                request.Repo,
                request.Title,
                request.Body
            );

            return Ok(result);
        }
    }
}
