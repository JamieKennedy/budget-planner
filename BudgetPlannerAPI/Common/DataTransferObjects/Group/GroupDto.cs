﻿using Common.DataTransferObjects.GroupMember;

namespace Common.DataTransferObjects.Group
{
    public class GroupDto
    {
        public Guid GroupId { get; set; }
        public Guid GroupOwnerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<GroupMemberDto> GroupMembers { get; set; } = new List<GroupMemberDto>();
    }
}