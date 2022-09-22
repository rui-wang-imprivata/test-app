using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Trace;
using System.Diagnostics;
using TestAppApi.Services;

namespace TestAppApi.Controllers
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
        private readonly IForecastService _service;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IForecastService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            try {
                return _service.GetWeatherForecasts();
            } catch (Exception ex) {
                Activity.Current?.RecordException(ex);
                _logger.LogError(ex, "Error happened");
            }
            return new List<WeatherForecast>();
        }
    }
}