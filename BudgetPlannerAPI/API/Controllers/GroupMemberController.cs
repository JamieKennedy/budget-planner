using Common.DataTransferObjects.GroupMember;
using Common.Exceptions.GroupMember;

using Microsoft.AspNetCore.Mvc;

using Services.Contracts;

namespace API.Controllers
{
    [Route("api/group/{groupId}/[controller]")]
    [ApiController]
    public class GroupMemberController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public GroupMemberController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost(Name = nameof(CreateGroupMember))]
        public IActionResult CreateGroupMember(long groupId, [FromBody] CreateGroupMemberDto createGroupMemberDto)
        {
            var groupMember = _serviceManager.GroupMemberService.CreateGroupMember(groupId, createGroupMemberDto);

            return CreatedAtRoute(nameof(GetGroupMember), new { groupId, groupMember.GroupMemberId }, groupMember);
        }

        [HttpGet("{groupMemberId}", Name = nameof(GetGroupMember))]
        public IActionResult GetGroupMember(long groupId, long groupMemberId)
        {
            var groupMember = _serviceManager.GroupMemberService.SelectById(groupMemberId);

            if (groupMember.GroupId != groupId) throw new InvalidGroupIdForGroupMember(groupMemberId, groupId);

            return Ok(groupMember);
        }

        [HttpGet(Name = nameof(GetGroupMembersByGroupId))]
        public IActionResult GetGroupMembersByGroupId(long groupId)
        {
            var groupMembers = _serviceManager.GroupMemberService.SelectByGroupId(groupId);

            return Ok(groupMembers);
        }
    }
}
