using Common.Exceptions.Base;

namespace Common.Exceptions.GroupMember
{
    public class GroupMemberNotFoundException : NotFoundException
    {
        public GroupMemberNotFoundException(long groupMemberId) : base($"No group member found with GroupMemeberId: {groupMemberId}")
        {
        }
    }
}
