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
        public IActionResult CreateUser(CreateUserDto createUserDto)
        {
            var user = _serviceManager.UserService.CreateUser(createUserDto);

            return CreatedAtAction(nameof(GetUserById), routeValues: new { user.UserId }, user);
        }

        [HttpGet("{userId}", Name = nameof(GetUserById))]
        public IActionResult GetUserById(long userId)
        {
            var user = _serviceManager.UserService.SelectById(userId);

            return Ok(user);
        }

        [HttpGet(Name = nameof(GetAllUsers))]
        public IActionResult GetAllUsers()
        {
            var users = _serviceManager.UserService.SelectAll();

            return Ok(users);
        }
    }
}
