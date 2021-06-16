using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BugTrackerApi.Controllers
{
    [ApiController]
    [Route("weather")] // /weather
    public class WeatherForecastController : ControllerBase
    { // readonly modifier indicates that the given structure is immutable
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        // ??
        private readonly ILogger<WeatherForecastController> _logger;

        // constructor
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        // action method
        [HttpGet("forecast")] // /weather/forecast
        // data type ActionResult
        public IEnumerable<WeatherForecast> Get()
        {
           
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            { // this is the format from weatherForecast class
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
            //.ToArray(); 
        }
    }
}
