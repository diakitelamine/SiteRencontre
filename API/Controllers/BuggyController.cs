using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly DataContext _context;

        public BuggyController(DataContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "Secret text";
        }

        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
            var thing = _context.Users.Find(-1);

            if (thing == null)
                return NotFound();

            return thing;
        }

        [HttpGet("server-error")]
        public IActionResult GetServerError()
        {
            var thing = _context.Users.Find(-1);
            var thingToReturn = thing.ToString();

            return StatusCode(500, thingToReturn);
        }

        [HttpGet("bad-request")]
        public IActionResult GetBadRequest()
        {
            return BadRequest("This was not a good request");
        }
    }
}
