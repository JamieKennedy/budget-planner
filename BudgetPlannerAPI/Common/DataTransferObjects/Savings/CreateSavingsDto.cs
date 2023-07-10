using System.ComponentModel.DataAnnotations;

using Common.DataTransferObjects.SavingsBalance;

namespace Common.DataTransferObjects.Savings
{
    public class CreateSavingsDto
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal Goal { get; set; }
        public DateTime? GoalDate { get; set; }
        public IEnumerable<CreateSavingsBalanceDto> SavingsBalances { get; set; } = new List<CreateSavingsBalanceDto>();
    }
}
