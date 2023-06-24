using Common.Models.User.Dto;

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

        [HttpPost(Name = "CreateUser")]
        public IActionResult CreateUser(CreateUserDto createUserDto)
        {
            var user = _serviceManager.UserService.CreateUser(createUserDto);

            return CreatedAtAction(nameof(Get), routeValues: new { user.UserId }, user);
        }

        [HttpGet("{userId}", Name = "GetUser")]
        public IActionResult Get(long userId)
        {
            var user = _serviceManager.UserService.SelectById(userId);

            return Ok(user);
        }
    }
}
