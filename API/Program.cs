using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Appelle la méthode CreateHostBuilder avec les arguments passés en paramètre, puis construit et exécute l'hôte
            CreateHostBuilder(args).Build().Run();
        }

        // Configure l'hôte pour qu'il utilise les valeurs par défaut et configure le service web pour utiliser la classe Startup
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
