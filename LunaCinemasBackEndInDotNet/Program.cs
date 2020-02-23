using System.Diagnostics.CodeAnalysis;
using LunaCinemasBackEndInDotNet.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace LunaCinemasBackEndInDotNet
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>().ConfigureServices((context, services) =>
                {
                    context.Configuration.GetSection(nameof(LunaCinemasDatabaseSettings))["ConnectionString"] =
                        args[0];
                });
    }
}
