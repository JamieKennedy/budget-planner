using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models.SavingsBalance
{
    public class SavingsBalanceModel
    {
        [Key]
        public long SavingsBalanceId { get; set; }
        [ForeignKey(nameof(Savings))]
        public long SavingsId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }
        public DateTime Created { get; set; }
    }
}
