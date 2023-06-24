using System.ComponentModel.DataAnnotations;

namespace Common.Models.SavingsBalance.Dto
{
    public class CreateSavingsBalanceDto
    {
        [Required]
        public long SasvingsId { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Balance { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
