using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace SimpleWebApi;

public class HomeController: Controller
{
    [HttpGet("/")]
    [OutputCache(Duration = 3600)]
    public IActionResult Get()
    {
        var randomString = GetRandomString(100*1024);
        return Ok(randomString);
    }

    private string GetRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}