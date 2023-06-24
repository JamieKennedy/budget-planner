using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Common.Models.SavingsBalance;
using Common.Models.User;

namespace Common.Models.Savings
{
    public class SavingsModel
    {
        [Key]
        public long SavingsId { get; set; }
        [ForeignKey(nameof(User))]
        public long UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Goal { get; set; }
        public UserModel? User { get; set; }
        public IEnumerable<SavingsBalanceModel>? Balances { get; set; }
        public DateTime LastModified { get; set; } = DateTime.Now;
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
