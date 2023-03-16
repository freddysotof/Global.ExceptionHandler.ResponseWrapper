using Global.ExceptionHandler.ResponseWrapper.Exceptions;
using Global.ExceptionHandler.ResponseWrapper.Models;
using Microsoft.AspNetCore.Mvc;

namespace Global.ExceptionHandler.ResponseWrapper.Api.Controllers
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

        /// <summary>
        /// Get Operation for Weather Forecast
        /// </summary>
        /// <returns>Weather Forecast Array</returns>
        [HttpGet(Name = "GetWeatherForecasts")]
        public IActionResult Get([FromQuery] int? limit, int offset = 0)
        {
            var result = Enumerable.Range(1, 10).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray();


            return Ok(result);
        }

        [HttpGet("{id:int}", Name = "GetWeatherForecast")]
        public IActionResult GetOne(int id)
        {
            return Ok(new WeatherForecast
            {
                Date = DateTime.Now.AddDays(id),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            });
        }


        [HttpPost(Name = "PostWeatherForecast")]
        public IActionResult Post([FromBody] WeatherForecast weatherForecast)
        {
            return new CreatedAtRouteResult(
                        "GetWeatherForecast",
                        new { id = 1 },
                        new { Id = 1, Date = DateTime.Now }
                    );
        }

        [HttpPut]
        public IActionResult Update()
        {
            return NoContent();

        }


        [HttpPut("{id:int}")]
        public IActionResult Update(int id)
        {
            throw new BadRequestException($"A product from the database with ID: {id} could not be found.", new List<ResponseError>()
        {
            new ResponseError("Error #1")
        });


        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            throw new AccessViolationException("Violation Exception while accessing the resource.");
        }

    }
}