using System.ComponentModel.DataAnnotations;

using Common.Constants.Enums;
using Common.DataTransferObjects.Base;

namespace Common.DataTransferObjects.Income
{
    public class CreateIncomeDto : CreateDtoBase
    {
        [Required]
        public Guid AccountId { get; set; }
        [MinLength(1)]
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public EOccurrence Occurrence { get; set; }
        public EOccurrsOn OccurrsOn { get; set; }
        public int? CustomOccurrsOn { get; set; }
    }
}
