using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Common.Models.User;

namespace Common.Models.Saving
{
    public class SavingModel
    {
        [Key]
        public long SavingId { get; set; }
        [ForeignKey(nameof(User))]
        public long UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Goal { get; set; }
        public DateTime LastModified { get; set; } = DateTime.Now;
        public DateTime Created { get; set; } = DateTime.Now;

        public UserModel? User { get; set; }
    }
}
