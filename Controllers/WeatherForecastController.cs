using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace nswag_liquid_slim.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        /// <summary>
        /// Retrieve weather forecast
        /// </summary>
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();

            return (
                from index in Enumerable.Range(1, 5)
                select new WeatherForecast
                {
                    Date         = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary      = Summaries[rng.Next(Summaries.Length)]
                }
            );
        }

        /// <summary>
        /// Add a weather forecast
        /// </summary>
        /// <param name="forecast">The forecast</param>
        [HttpPost]
        public WeatherForecast AddForecast(WeatherForecast forecast)
        {
            return forecast;
        }
        
        /// <summary>
        /// Add a weather forecast
        /// </summary>
        /// <param name="forecast">The forecast</param>
        [HttpPut]
        public WeatherForecast UpdateForecast(WeatherForecast forecast)
        {
            return forecast;
        }
    }
    
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }
    }
}
