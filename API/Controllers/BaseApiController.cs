using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{   
    // Le controlller de base, toute les requêtes passe par ce controller
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        
    }
}


