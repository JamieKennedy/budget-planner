using Common.Exceptions.Base;

namespace Common.Exceptions.GroupMember
{
    public class InvalidGroupIdForGroupMember : BadRequestException
    {
        public InvalidGroupIdForGroupMember(long groupMemberId, long groupId) : base($"The group member with Id: {groupMemberId} does not have GroupId: {groupId}")
        {
        }
    }
}
