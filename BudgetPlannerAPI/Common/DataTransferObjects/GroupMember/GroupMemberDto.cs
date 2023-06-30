namespace Common.DataTransferObjects.GroupMember
{
    public class GroupMemberDto
    {
        public long GroupMemberId { get; set; }
        public long GroupId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
