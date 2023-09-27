using System.ComponentModel.DataAnnotations;

using Common.Constants.Enums;

namespace Common.DataTransferObjects.Income
{
    public class UpdateIncomeDto
    {
        [Required]
        public Guid AccountId { get; set; }
        public string? Name { get; set; } = null;
        public decimal? Amount { get; set; } = null;
        public EOccurrence? Occurrence { get; set; } = null;
        public EOccurrsOn? OccurrsOn { get; set; } = null;
        public int? CustomOccurrsOn { get; set; } = null;
    }
}
