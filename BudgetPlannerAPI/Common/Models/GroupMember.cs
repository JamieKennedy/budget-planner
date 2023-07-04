﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models
{
    public class GroupMember
    {
        [Key]
        public Guid GroupMemberId { get; set; }
        [ForeignKey(nameof(Group))]
        public Guid GroupId { get; set; }
        public Group? Group { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
