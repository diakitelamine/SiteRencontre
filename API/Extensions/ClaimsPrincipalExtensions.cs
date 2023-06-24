using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        // Récupère le username du token
        public static string GetUsername(this ClaimsPrincipal user)
        {
            // Récupère le username du token
            return user.FindFirst(ClaimTypes.Name)?.Value;
        }

        
        public static string GetUserId(this ClaimsPrincipal user)
        {
            // Récupère le username du token
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

    }
}