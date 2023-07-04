namespace Common.DataTransferObjects.GroupMember
{
    public class GroupMemberDto
    {
        public Guid GroupMemberId { get; set; }
        public Guid GroupId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
