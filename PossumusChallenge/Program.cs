using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PossumusChallenge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory();
            string pathlog = path + @"\Loggeos\Log.txt";
            Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
                .WriteTo.File(pathlog, outputTemplate:"{Timestamp:yyyy-MM-dd HH:mm:ss} {Message:lj}{NewLine}{Exeption}").CreateLogger();
                
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
