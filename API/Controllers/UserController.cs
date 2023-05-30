using System.Security.Claims;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//Définir le namespace pour la classe
namespace API.Controllers
{
    
     [Authorize]
    //Attribuer un contrôleur API à la classe UsersController
    public class UsersController : BaseApiController
    {
        //Définir un champ privé pour stocker l'objet DataContext
         
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        //Créer un constructeur pour initialiser le champ _context
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
              _mapper = mapper;
              _userRepository = userRepository;
            
        }

        //Définir une méthode qui gère une requête HTTP GET pour obtenir tous les utilisateurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            //Utiliser la méthode ToListAsync pour obtenir tous les utilisateurs dans la base de données
            var users = await  _userRepository.GetMembersAsync();
            return Ok(users);
        }
       
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser( string username)
        {
            //Utiliser la méthode ToListAsync pour obtenir tous les utilisateurs dans la base de données
            return   await  _userRepository.GetMemberAsyn(username);
            
        }

       //Mise a jour des données de l'utilisateur
        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            //Utiliser la méthode ToListAsync pour obtenir tous les utilisateurs dans la base de données
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if(user == null) return NotFound();
            _mapper.Map(memberUpdateDto, user);
            _userRepository.Update(user);
            if(await _userRepository.SaveAllAsync()) return NoContent();
            return BadRequest("Failed to update user");
            
        }

    }
}
