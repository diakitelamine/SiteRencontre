using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using API.DTOs;
using Microsoft.EntityFrameworkCore;
using API.Interfaces;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("register")] //POST: api/account/register
        public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
        {
            // Vérifie si le nom d'utilisateur existe déjà dans la base de données
            if(await UserExists(registerDto.UserName)) return BadRequest("Username is token");
        
            // Créer une instance de HMACSHA512 pour hacher le mot de passe
            using var hmac = new HMACSHA512();

            // Crée un nouvel utilisateur avec les données de l'objet RegisterDto
            var user = new AppUser
            {
                UserName = registerDto.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            // Ajoute l'utilisateur à la base de données
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
         // Api pour le formulaire  d'authentification
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            // Recherche l'utilisateur dans la base de données
            var user = await  _context.Users
                .Include(p=>p.Photos)
                .SingleOrDefaultAsync(x=>x.UserName == loginDto.UserName);

            // Vérifie si l'utilisateur existe
            if(user == null) return Unauthorized("Le nom d'utilisateur est invalide !");

            // Créer une instance de HMACSHA512 avec le sel de l'utilisateur pour hacher le mot de passe de l'objet LoginDto
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            // Compare les hachages pour vérifier si les mots de passe correspondent
            for(int i = 0 ; i < computedHash.Length; i++){
              
              if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Le mot passe est invalide !");

            }

            // Si les informations d'identification sont valides, retourne un objet UserDto avec le nom d'utilisateur et le jeton d'accès
            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x=>x.IsMain)?.Url


            };
        }

        // Vérifie si le nom d'utilisateur existe déjà dans la base de données
        private async Task<bool> UserExists(string username){
            return await _context.Users.AnyAsync(x=>x.UserName == username.ToLower());
        }
    }  
}
