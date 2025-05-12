using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Ecommerce.Api.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier) ?? user.FindFirst(JwtRegisteredClaimNames.Sub);

            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException("User must be signed in to perform this action.");
            }

            return int.Parse(userIdClaim.Value);
        }
    }

}
