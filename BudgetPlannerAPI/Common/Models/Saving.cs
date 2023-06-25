using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models
{
    public class Saving
    {
        [Key]
        public long SavingId { get; set; }
        [ForeignKey(nameof(Models.User))]
        public long UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Goal { get; set; }
        public DateTime LastModified { get; set; } = DateTime.Now;
        public DateTime Created { get; set; } = DateTime.Now;

        public User? User { get; set; }
        public List<SavingBalance> SavingBalances { get; set; } = new List<SavingBalance>();
    }
}
