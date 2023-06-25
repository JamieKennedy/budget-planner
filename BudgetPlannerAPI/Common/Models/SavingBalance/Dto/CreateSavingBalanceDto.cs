using System.ComponentModel.DataAnnotations;

namespace Common.Models.SavingBalance.Dto
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
