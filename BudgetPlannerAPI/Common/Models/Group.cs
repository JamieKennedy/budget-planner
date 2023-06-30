using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models
{
    public class Group
    {
        [Key]
        public long GroupId { get; set; }
        [ForeignKey(nameof(User))]
        public long GroupOwnerId { get; set; }
        public User? User { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();

    }
}
