using Common.Exceptions.Base;

namespace Common.Exceptions.GroupMember
{
    public class InvalidGroupIdForGroupMember : BadRequestException
    {
        public InvalidGroupIdForGroupMember(Guid groupMemberId, Guid groupId) : base($"The group member with Id: {groupMemberId} does not have GroupId: {groupId}")
        {
        }
    }
}
