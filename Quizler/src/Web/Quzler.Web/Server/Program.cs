using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

<<<<<<< HEAD:Quizler/src/Web/Quizler.Web/Server/Program.cs
namespace Quizler.Web.Server
=======
namespace Quzler.Web.Server
>>>>>>> 3045bb8d6e3ffefd1bdcd0f7a1d568e828332ccb:Quizler/src/Web/Quzler.Web/Server/Program.cs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
