using Common.DataTransferObjects.Authentication;

using Microsoft.AspNetCore.Http;

namespace Common.Utils
{
    public static class AuthUtils
    {
        public static AuthIdentity GetAuthIdentity(this HttpContext httpContext)
        {
            var claims = httpContext.User.Claims;

            var id = Guid.Parse(claims.FirstOrDefault(claim => claim.Type == "Id")?.Value ?? "");
            var email = claims.FirstOrDefault(claim => claim.Type == "Email")?.Value ?? "";

            return new AuthIdentity(id, email);
        }
    }
}
