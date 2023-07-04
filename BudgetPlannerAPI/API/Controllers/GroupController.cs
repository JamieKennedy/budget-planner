using Common.DataTransferObjects.Group;

using Microsoft.AspNetCore.Mvc;

using Services.Contracts;

namespace API.Controllers
{
    [Route("api/user/{userId}/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public GroupController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost(Name = nameof(CreateGroup))]
        public async Task<IActionResult> CreateGroup(Guid userId, CreateGroupDto createGroupDto)
        {
            var group = await _serviceManager.GroupService.CreateGroup(userId, createGroupDto);

            return CreatedAtRoute(nameof(GetGroup), new { userId, group.GroupId }, group);
        }

        [HttpGet("{groupId}", Name = nameof(GetGroup))]
        public IActionResult GetGroup(Guid groupId)
        {
            var group = _serviceManager.GroupService.SelectById(groupId);

            return Ok(group);
        }
    }
}
