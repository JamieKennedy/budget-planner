using Common.DataTransferObjects.Authentication;
using Common.DataTransferObjects.User;
using Common.Exceptions.Base;
using Common.Exceptions.Configuration;

using LoggerService.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using Services.Contracts;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IConfiguration _configuration;

        public UserController(IServiceManager serviceManager, ILoggerManager loggerManager, IHttpContextAccessor contextAccessor, IConfiguration configuration) : base(serviceManager, loggerManager, contextAccessor)
        {
            _configuration = configuration;
        }

        [HttpPost(Name = nameof(CreateUser))]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            var result = await serviceManager.UserService.CreateUser(createUserDto);

            if (result.Succeeded)
            {
                var user = await serviceManager.UserService.SelectByEmail(createUserDto.Email);



                var token = await serviceManager.AuthenticationService.CreateToken(user.Email);

                var authCookie = new AuthenticationCookieDto()
                {
                    RefreshToken = token.RefreshToken,
                    KeepLoggedIn = createUserDto.KeepLoggedIn
                };

                var refreshExpiry = _configuration.GetSection("JwtSettings")["RefreshTokenExpiryDays"];

                if (string.IsNullOrEmpty(refreshExpiry))
                {
                    throw new ConfigurationItemNotFoundException("JwtSettings:Secret");
                }

                Response.Cookies.Append("AuthCookie", JsonConvert.SerializeObject(authCookie), new CookieOptions()
                {
                    Expires = createUserDto.KeepLoggedIn ? DateTimeOffset.Now.AddDays(int.Parse(refreshExpiry)) : null,
                    HttpOnly = true,
                    SameSite = SameSiteMode.None,
                    Secure = true
                });

                return CreatedAtAction(nameof(GetUserById), routeValues: new { UserId = user.Id }, token.AccessToken);
            }

            return BadRequest(result.Errors);
        }

        [Authorize]
        [HttpGet(Name = nameof(GetCurrentUser))]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await serviceManager.UserService.SelectById(AuthIdentity.Id);

            return Ok(user);
        }

        [Authorize]
        [HttpGet("{userId}", Name = nameof(GetUserById))]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            if (userId != AuthIdentity.Id) throw new UnauthorisedException("Cannot retreive other users");

            var user = await serviceManager.UserService.SelectById(AuthIdentity.Id);

            return Ok(user);
        }

        [Authorize]
        [HttpGet("email/{emailAddress}", Name = nameof(GetUserByEmail))]
        public async Task<IActionResult> GetUserByEmail(string emailAddress)
        {
            var user = await serviceManager.UserService.SelectByEmail(emailAddress);

            return Ok(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all", Name = nameof(GetUsers))]
        public async Task<IActionResult> GetUsers()
        {
            var users = await serviceManager.UserService.GetAll();

            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{id}/role/{roleName}", Name = nameof(AssignUserToRole))]
        public async Task<IActionResult> AssignUserToRole(Guid id, string roleName)
        {
            return Ok(await serviceManager.UserService.AssignRole(id, roleName));
        }
    }
}
