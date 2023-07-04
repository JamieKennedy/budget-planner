using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models
{
    public class Group
    {
        [Key]
        public Guid GroupId { get; set; }
        [ForeignKey(nameof(User))]
        public Guid GroupOwnerId { get; set; }
        public User? User { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();

    }
}
