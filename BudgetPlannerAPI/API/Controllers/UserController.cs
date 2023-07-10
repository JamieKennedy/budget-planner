using Common.DataTransferObjects.User;

using Microsoft.AspNetCore.Mvc;

using Services.Contracts;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public UserController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost(Name = nameof(CreateUser))]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            var result = await _serviceManager.UserService.CreateUser(createUserDto);

            if (result.Succeeded)
            {
                var user = await _serviceManager.UserService.SelectByEmail(createUserDto.Email);

                return CreatedAtAction(nameof(GetUserById), routeValues: new { UserId = user.Id }, user);
            }

            return BadRequest(result.Errors);
        }

        [HttpGet("{userId}", Name = nameof(GetUserById))]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            var user = await _serviceManager.UserService.SelectById(userId);

            return Ok(user);
        }

        [HttpGet("email/{emailAddress}", Name = nameof(GetUserByEmail))]
        public async Task<IActionResult> GetUserByEmail(string emailAddress)
        {
            var user = await _serviceManager.UserService.SelectByEmail(emailAddress);

            return Ok(user);
        }

        [HttpGet(Name = nameof(GetUsers))]
        public IActionResult GetUsers()
        {
            var users = _serviceManager.UserService.GetAll();

            return Ok(users);
        }
    }
}
