using Microsoft.AspNetCore.Mvc;

namespace MultyAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MultyApiController(HttpClient httpClient) : ControllerBase
{
    private readonly HttpClient _httpClient = httpClient;

    private async Task<string> GetDataFromAsync(string url)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex)
        {
            return $"Error fetching data from {url}: {ex.Message}";
        }
    }

    [HttpGet("multiple_request")]
    public async Task<IActionResult> GetDataFromMultypleApis()
    {
        string[] urls = new string[] { "https://jsonplaceholder.typicode.com/posts", "https://catfact.ninja/fact", "https://v2.jokeapi.dev/joke/Any"};
        Task[] respTasks = new Task[urls.Length];
        for (int i = 0; i < urls.Length; i++)
        {
            respTasks[i] = GetDataFromAsync(urls[i]);
        }

        await Task.WhenAll(respTasks);

        var result = new
        {
            Weather = respTasks[0],
            Currency = respTasks[1],
            News = respTasks[2]
        };

        return Ok(result);
    }
}