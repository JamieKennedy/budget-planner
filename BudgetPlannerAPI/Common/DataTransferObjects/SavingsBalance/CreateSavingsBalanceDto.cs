using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace Common.DataTransferObjects.SavingsBalance
{
    public class CreateSavingsBalanceDto
    {
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Balance { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
