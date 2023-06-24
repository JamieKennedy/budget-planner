using System.ComponentModel.DataAnnotations;

namespace Common.Models.Savings.Dto
{
    public class CreateSavingsDto
    {
        [Required]
        public long UserId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal Goal { get; set; }
    }
}
