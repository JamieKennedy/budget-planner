using Common.DataTransferObjects.Base;

namespace Common.DataTransferObjects.ExpenseCategory
{
    public class ExpenseCategoryDto : DtoBase
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ColourHex { get; set; } = string.Empty;
    }
}
