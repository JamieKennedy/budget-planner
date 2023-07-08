using Common.DataTransferObjects.Authentication;
using Common.DataTransferObjects.Token;
using Common.Exceptions.Configuration;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using Services.Contracts;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IServiceManager serviceManager, IConfiguration configuration)
        {
            _serviceManager = serviceManager;
            _configuration = configuration;
        }

        [HttpPost(Name = nameof(Authenticate))]
        public async Task<IActionResult> Authenticate([FromBody] UserAuthenticationDto userAuthenticationDto)
        {
            if (!await _serviceManager.AuthenticationService.AuthenticateUser(userAuthenticationDto)) return Unauthorized();

            var tokenDto = _serviceManager.AuthenticationService.CreateToken();

            var authCookie = new AuthenticationCookieDto()
            {
                RefreshToken = tokenDto.RefreshToken,
                KeepLoggedIn = userAuthenticationDto.KeepLoggedIn
            };

            var refreshExpiry = _configuration.GetSection("JwtSettings")["RefreshTokenExpiryDays"];

            if (string.IsNullOrEmpty(refreshExpiry))
            {
                throw new ConfigurationItemNotFoundException("JwtSettings:Secret");
            }

            Response.Cookies.Append("AuthCookie", JsonConvert.SerializeObject(authCookie), new CookieOptions()
            {
                Expires = userAuthenticationDto.KeepLoggedIn ? DateTimeOffset.Now.AddDays(int.Parse(refreshExpiry)) : null,
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true
            });

            return Ok(tokenDto.AccessToken);
        }

        [HttpPost("Refresh", Name = nameof(Refresh))]
        public async Task<IActionResult> Refresh()
        {
            if (Request.Cookies.TryGetValue("AuthCookie", out var authCookieString))
            {
                var authCookie = JsonConvert.DeserializeObject<AuthenticationCookieDto>(authCookieString);

                if (authCookie is not null)
                {
                    var newToken = await _serviceManager.AuthenticationService.RefreshToken(new RefreshTokenDto()
                    {
                        RefreshToken = authCookie.RefreshToken
                    });

                    authCookie.RefreshToken = newToken.RefreshToken;

                    var refreshExpiry = _configuration.GetSection("JwtSettings")["RefreshTokenExpiryDays"];

                    if (string.IsNullOrEmpty(refreshExpiry))
                    {
                        throw new ConfigurationItemNotFoundException("JwtSettings:Secret");
                    }

                    Response.Cookies.Append("AuthCookie", JsonConvert.SerializeObject(authCookie), new CookieOptions()
                    {
                        Expires = authCookie.KeepLoggedIn ? DateTimeOffset.Now.AddDays(int.Parse(refreshExpiry)) : null,
                        HttpOnly = true,
                        SameSite = SameSiteMode.None,
                        Secure = true
                    });

                    return Ok(newToken.AccessToken);
                }
            }
            return Unauthorized("Invalid Refresh Token");
        }

        [HttpPost("Logout", Name = nameof(Logout))]
        public IActionResult Logout()
        {
            if (Request.Cookies.TryGetValue("AuthCookie", out var authCookieString))
            {
                Response.Cookies.Append("AuthCookie", authCookieString, new CookieOptions()
                {
                    Expires = DateTimeOffset.MinValue,
                    HttpOnly = true,
                    SameSite = SameSiteMode.None,
                    Secure = true
                });
            }

            return Ok();
        }
    }
}
