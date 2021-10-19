using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using VehicleRenter_BL;

namespace VehicleRenter_API.Controllers
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
        private readonly IConfiguration configuration;
        private readonly IDbConnection db = SqlHelper.Open();
        public WeatherForecastController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        [HttpGet]
        public JsonResult Get()
         {
            try
            {
                var rng = new Random();
                var Result= Enumerable.Range(1, 20).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToList();

                return new JsonResult(new { Message = "", Result = Result });
            }
            catch(Exception ex)
            {
                return new JsonResult(new { Message = "", Result = ex.Message });
            }
        }
    }
}
