using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;

        // Le constructeur reçoit la configuration de l'application via un objet IConfiguration
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // Cette méthode est appelée par le runtime ASP.NET Core pour ajouter des services à notre conteneur de dépendances
        public void ConfigureServices(IServiceCollection services)
        {
            // On ajoute un DbContext de type DataContext à notre conteneur de dépendances et on configure l'option de base de données avec une connexion SQLite via la chaîne de connexion "DefaultConnection" stockée dans notre fichier appsettings.json
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(_config.GetConnectionString("DefaultConnection"));
            });
            // On ajoute le service de contrôleurs MVC à notre conteneur de dépendances
            services.AddControllers();
            // On ajoute le service CORS à notre conteneur de dépendances
            services.AddCors();
        }

        // Cette méthode est appelée par le runtime ASP.NET Core pour configurer le pipeline de requêtes HTTP
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // On vérifie si l'application est en mode développement, et si oui, on active la page d'exception de développeur pour afficher les erreurs détaillées
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // On configure notre middleware de redirection HTTP vers HTTPS
            app.UseHttpsRedirection();

            // On configure notre middleware de routage
            app.UseRouting();

            // On configure CORS pour autoriser les requêtes avec n'importe quelle méthode, en provenance de n'importe quelle origine et avec n'importe quelle entête depuis "https://localhost:4200"
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

            // On configure l'authentification et l'autorisation
            app.UseAuthorization();

            // On configure les points d'extrémité de l'API pour que les requêtes HTTP soient traitées par les contrôleurs appropriés
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

