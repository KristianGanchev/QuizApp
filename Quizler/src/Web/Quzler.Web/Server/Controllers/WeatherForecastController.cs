<<<<<<< HEAD:Quizler/src/Web/Quizler.Web/Server/Controllers/WeatherForecastController.cs
﻿using Quizler.Web.Shared;
=======
﻿using Quzler.Web.Shared;
>>>>>>> 3045bb8d6e3ffefd1bdcd0f7a1d568e828332ccb:Quizler/src/Web/Quzler.Web/Server/Controllers/WeatherForecastController.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

<<<<<<< HEAD:Quizler/src/Web/Quizler.Web/Server/Controllers/WeatherForecastController.cs
namespace Quizler.Web.Server.Controllers
=======
namespace Quzler.Web.Server.Controllers
>>>>>>> 3045bb8d6e3ffefd1bdcd0f7a1d568e828332ccb:Quizler/src/Web/Quzler.Web/Server/Controllers/WeatherForecastController.cs
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
