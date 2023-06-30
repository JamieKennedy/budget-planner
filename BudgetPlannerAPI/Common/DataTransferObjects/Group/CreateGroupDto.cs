using Common.DataTransferObjects.GroupMember;

namespace Common.DataTransferObjects.Group
{
    public class CreateGroupDto
    {
        public string Name { get; set; } = string.Empty;
        public IEnumerable<CreateGroupMemberDto> GroupMembers { get; set; } = new List<CreateGroupMemberDto>();
    }
}
