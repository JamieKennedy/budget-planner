using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models
{
    public class SavingsBalance
    {
        [Key]
        public long SavingsBalanceId { get; set; }
        [ForeignKey(nameof(Savings))]
        public long SavingsId { get; set; }
        public Savings? Savings { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }
        public DateTime Created { get; set; }

    }
}
