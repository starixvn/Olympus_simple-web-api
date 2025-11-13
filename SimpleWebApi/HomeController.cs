using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace SimpleWebApi;

public class HomeController: Controller
{
    private static readonly HttpClient Client = new HttpClient
    {
        BaseAddress = new Uri("http://nexcel_redmine_NEXAWS:9293")
    };

    [HttpGet("/")]
    [OutputCache(NoStore = true)]
    public async Task<IActionResult> Get()
    {
        var response = await Client.GetAsync("/stats");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();

        return Ok(content);
    }

    private string GetRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}