using System.ComponentModel.DataAnnotations;

namespace Common.DataTransferObjects.ExpenseCategory
{
    public class CreateExpenseCategoryDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string ColourHex { get; set; } = string.Empty;
    }
}
