using Common.DataTransferObjects.Base;
using Common.DataTransferObjects.RecurrencePattern;

namespace Common.DataTransferObjects.Income
{
    public class IncomeDto : DtoModifiableBase
    {
        public Guid UserId { get; set; }
        public Guid AccountId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public RecurrencePatternDto? RecurrencePattern { get; set; }
    }
}
