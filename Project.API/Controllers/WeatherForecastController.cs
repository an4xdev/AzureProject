using Microsoft.AspNetCore.Mvc;
using Project.API.Services;
using Project.Shared;

namespace Project.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(ILogger<WeatherForecastController> logger, ISendOnTopic sendOnTopic)
    : ControllerBase
{
    private ISendOnTopic _sendOnTopic = sendOnTopic;

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger = logger;

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    [HttpPost("topic")]
    public async Task PushNotification()
    {
        _sendOnTopic.Initialize();
        await _sendOnTopic.SendMessage("TEST MESSAGE");
    }
}