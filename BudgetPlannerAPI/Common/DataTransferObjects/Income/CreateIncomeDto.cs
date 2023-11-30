using System.ComponentModel.DataAnnotations;

using Common.DataTransferObjects.Base;
using Common.DataTransferObjects.RecurrencePattern;

namespace Common.DataTransferObjects.Income
{
    public class CreateIncomeDto : CreateDtoBase
    {
        [Required]
        public Guid AccountId { get; set; }
        [MinLength(1)]
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public CreateRecurrencePatternDto? RecurrencePattern { get; set; }
    }
}
