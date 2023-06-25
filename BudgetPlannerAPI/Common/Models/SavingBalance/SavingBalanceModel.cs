using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Common.Models.Saving;

namespace Common.Models.SavingBalance
{
    public class SavingBalanceModel
    {
        [Key]
        public long SavingBalanceId { get; set; }
        [ForeignKey(nameof(Saving))]
        public long SavingId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }
        public DateTime Created { get; set; }
        public SavingModel? Saving { get; set; }
    }
}
