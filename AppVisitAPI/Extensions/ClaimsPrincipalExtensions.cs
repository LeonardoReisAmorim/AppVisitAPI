using System.Security.Claims;

namespace AppVisitAPI.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return Boolean.Parse(user.FindFirst("admin").Value);
        }
    }
}
