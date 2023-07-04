using Common.Exceptions.Base;

namespace Common.Exceptions.GroupMember
{
    public class GroupMemberNotFoundException : NotFoundException
    {
        public GroupMemberNotFoundException(Guid groupMemberId) : base($"No group member found with GroupMemeberId: {groupMemberId}")
        {
        }
    }
}
