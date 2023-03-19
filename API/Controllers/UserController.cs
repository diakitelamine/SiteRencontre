using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//Définir le namespace pour la classe
namespace API.Controllers
{
    
     [Authorize]
    //Attribuer un contrôleur API à la classe UsersController
    public class UsersController : BaseApiController
    {
        //Définir un champ privé pour stocker l'objet DataContext
        private readonly DataContext _context;

        //Créer un constructeur pour initialiser le champ _context
        public UsersController(DataContext context)
        {
            _context = context;
        }

        //Définir une méthode qui gère une requête HTTP GET pour obtenir tous les utilisateurs
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            //Utiliser la méthode ToListAsync pour obtenir tous les utilisateurs dans la base de données
            return await _context.Users.ToListAsync();
        }

        //Définir une méthode qui gère une requête HTTP GET pour obtenir un utilisateur en fonction de son ID
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            //Utiliser la méthode FindAsync pour trouver un utilisateur en fonction de son ID
            return await _context.Users.FindAsync(id);
        }
    }
}
