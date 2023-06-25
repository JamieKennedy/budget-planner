using System.ComponentModel.DataAnnotations;

namespace Common.DataTransferObjects.SavingBalance
{
    public class CreateSavingBalanceDto
    {
        [Required]
        public long SavingId { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Balance { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
