using Common.DataTransferObjects.Authentication;
using Common.DataTransferObjects.Token;
using Common.Results.Error.Base;
using Common.Results.Error.Configuration;

using FluentResults;

using LoggerService.Interfaces;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using Services.Contracts;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        private readonly IConfiguration _configuration;

        public AuthenticationController(IServiceManager serviceManager, ILoggerManager loggerManager, IHttpContextAccessor contextAccessor, IConfiguration configuration) : base(serviceManager, loggerManager, contextAccessor)
        {
            _configuration = configuration;
        }

        [HttpPost(Name = nameof(Authenticate))]
        public async Task<IActionResult> Authenticate([FromBody] UserAuthenticationDto userAuthenticationDto)
        {

            var authResult = await serviceManager.AuthenticationService.AuthenticateUser(userAuthenticationDto);

            if (authResult.IsFailed) return HandleResult(Result.Fail(new UnauthorisedError("Incorrect Details")));

            var tokenResult = await serviceManager.AuthenticationService.CreateToken(userAuthenticationDto.Email);

            if (tokenResult.IsFailed) return HandleResult(tokenResult);

            var authCookie = new AuthenticationCookieDto()
            {
                RefreshToken = tokenResult.Value.RefreshToken,
                KeepLoggedIn = userAuthenticationDto.KeepLoggedIn
            };

            var refreshExpiry = _configuration.GetSection("JwtSettings")["RefreshTokenExpiryDays"];

            if (string.IsNullOrEmpty(refreshExpiry))
            {
                return HandleResult(Result.Fail(new ConfigurationItemNotFoundError("JwtSettings:Secret")));
            }

            Response.Cookies.Append("AuthCookie", JsonConvert.SerializeObject(authCookie), new CookieOptions()
            {
                Expires = userAuthenticationDto.KeepLoggedIn ? DateTimeOffset.Now.AddDays(int.Parse(refreshExpiry)) : null,
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true
            });

            return Ok(tokenResult.Value.AccessToken);
        }

        [HttpPost("Refresh", Name = nameof(Refresh))]
        public async Task<IActionResult> Refresh()
        {
            if (Request.Cookies.TryGetValue("AuthCookie", out var authCookieString))
            {
                var authCookie = JsonConvert.DeserializeObject<AuthenticationCookieDto>(authCookieString);

                if (authCookie is not null)
                {
                    var newTokenResult = await serviceManager.AuthenticationService.RefreshToken(new RefreshTokenDto()
                    {
                        RefreshToken = authCookie.RefreshToken
                    });

                    if (newTokenResult.IsFailed) return HandleResult(newTokenResult);

                    authCookie.RefreshToken = newTokenResult.Value.RefreshToken;

                    var refreshExpiry = _configuration.GetSection("JwtSettings")["RefreshTokenExpiryDays"];

                    if (string.IsNullOrEmpty(refreshExpiry))
                    {
                        return HandleResult(Result.Fail(new ConfigurationItemNotFoundError("JwtSettings:Secret")));
                    }

                    Response.Cookies.Append("AuthCookie", JsonConvert.SerializeObject(authCookie), new CookieOptions()
                    {
                        Expires = authCookie.KeepLoggedIn ? DateTimeOffset.Now.AddDays(int.Parse(refreshExpiry)) : null,
                        HttpOnly = true,
                        SameSite = SameSiteMode.None,
                        Secure = true
                    });

                    return Ok(newTokenResult.Value.AccessToken);
                }
            }
            return HandleResult(Result.Fail(new UnauthorisedError("Invalid Refresh Token")));
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

        [HttpPost("Reset/Token", Name = nameof(GeneratePasswordResetToken))]
        public async Task<IActionResult> GeneratePasswordResetToken([FromBody] ResetPasswordTokenDto resetPasswordTokenDto)
        {
            var tokenResult = await serviceManager.AuthenticationService.GeneratePasswordResetToken(resetPasswordTokenDto.Email);

            return HandleResult(tokenResult);
        }

        [HttpPost("Reset", Name = nameof(ResetPassword))]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            var result = await serviceManager.AuthenticationService.ResetPassword(resetPasswordDto.Email, resetPasswordDto.ResetToken, resetPasswordDto.NewPassword);

            return HandleResult(result);
        }
    }
}
