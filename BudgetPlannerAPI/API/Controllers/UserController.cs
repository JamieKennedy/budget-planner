using Common.DataTransferObjects.User;
using Common.Exceptions.Base;

using LoggerService.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Services.Contracts;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        public UserController(IServiceManager serviceManager, ILoggerManager loggerManager, IHttpContextAccessor contextAccessor) : base(serviceManager, loggerManager, contextAccessor) { }

        [HttpPost(Name = nameof(CreateUser))]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            var result = await serviceManager.UserService.CreateUser(createUserDto);

            if (result.Succeeded)
            {
                var user = await serviceManager.UserService.SelectByEmail(createUserDto.Email);

                return CreatedAtAction(nameof(GetUserById), routeValues: new { UserId = user.Id }, user);
            }

            return BadRequest(result.Errors);
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

        [Authorize]
        [HttpGet("all", Name = nameof(GetUsers))]
        public IActionResult GetUsers()
        {
            var users = serviceManager.UserService.GetAll();

            return Ok(users);
        }
    }
}
