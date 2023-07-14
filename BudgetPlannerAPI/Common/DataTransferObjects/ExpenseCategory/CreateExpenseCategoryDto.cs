using System.ComponentModel.DataAnnotations;

namespace Common.DataTransferObjects.ExpenseCategory
{
    public class CreateExpenseCategoryDto
    {
        [Required]
        public string Name { get; set; }
        public string ColourHex { get; set; }
    }
}
