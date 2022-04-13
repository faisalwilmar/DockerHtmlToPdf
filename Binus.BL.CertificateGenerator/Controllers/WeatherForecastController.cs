using Microsoft.AspNetCore.Mvc;

namespace Binus.BL.CertificateGenerator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        [Route("GetChoices")]
        public IEnumerable<WeatherForecast> Gethh()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet(Name = "GetRandomString")]
        [Route("GetRandom")]
        public TheObject Sthring()
        {
            return new TheObject
            {
                Name = "Rizky ganteng",
                Description = "Gapapa"
            };
        }

        public class TheObject
        {
            public string Name { get; set; }
            public string Description { get; set; }
        }
    }
}