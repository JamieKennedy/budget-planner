using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models
{
    public class SavingBalance
    {
        [Key]
        public long SavingBalanceId { get; set; }
        [ForeignKey(nameof(Models.Saving))]
        public long SavingId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }
        public DateTime Created { get; set; }
        public Saving? Saving { get; set; }
    }
}
