using System;
using System.Collections.Generic;
using System.Text;

<<<<<<< HEAD:Quizler/src/Web/Quizler.Web/Shared/WeatherForecast.cs
namespace Quizler.Web.Shared
=======
namespace Quzler.Web.Shared
>>>>>>> 3045bb8d6e3ffefd1bdcd0f7a1d568e828332ccb:Quizler/src/Web/Quzler.Web/Shared/WeatherForecast.cs
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
