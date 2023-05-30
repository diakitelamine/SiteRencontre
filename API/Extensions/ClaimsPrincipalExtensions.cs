using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            // Récupère le username du token
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

    }
}