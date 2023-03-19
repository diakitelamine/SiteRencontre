
using API.Extensions;
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
         
            // On ajoute le service de contrôleurs MVC à notre conteneur de dépendances
            services.AddControllers();
            services.AppApplicationServices(_config);
            services.AddIdentityServices(_config);
            
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
            
            app.UseAuthentication();
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

