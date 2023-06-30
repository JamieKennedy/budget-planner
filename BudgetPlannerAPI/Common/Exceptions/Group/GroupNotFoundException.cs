using Common.Exceptions.Base;

namespace Common.Exceptions.Group
{
    public class GroupNotFoundException : NotFoundException
    {
        public GroupNotFoundException(long groupId) : base($"No group found with GroupId: {groupId}")
        {
        }
    }
}
