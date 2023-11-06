using System.ComponentModel.DataAnnotations.Schema;

using Common.Models.Base;

namespace Common.Models
{
    public class SavingsBalance : ModelBase
    {
        [ForeignKey(nameof(Savings))]
        public Guid SavingsId { get; set; }
        public Savings? Savings { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }

    }
}
