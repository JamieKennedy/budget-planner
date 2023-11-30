using System.ComponentModel.DataAnnotations;

using Common.DataTransferObjects.Base;
using Common.DataTransferObjects.RecurrencePattern;

namespace Common.DataTransferObjects.Income
{
    public class UpdateIncomeDto : UpdateDtoBase
    {
        [Required]
        public Guid AccountId { get; set; }
        public string? Name { get; set; } = null;
        public decimal? Amount { get; set; } = null;
        public UpdateRecurrencePatternDto? RecurrencePattern { get; set; } = null;
    }
}
