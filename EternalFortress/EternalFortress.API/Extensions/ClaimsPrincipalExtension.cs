using System.Security.Claims;

namespace EternalFortress.API.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static int GetId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                return 0;
            }

            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
            return int.Parse(claim?.Value ?? "0");
        }
    }
}
