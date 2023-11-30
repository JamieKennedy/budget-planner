using System.ComponentModel.DataAnnotations;

using Common.Constants.Enums;
using Common.DataTransferObjects.Base;

namespace Common.DataTransferObjects.RecurrencePattern
{
    public class UpdateRecurrencePatternDto : UpdateDtoBase
    {
        public ERecurrenceType? RecurrenceType { get; set; } = null;
        public int? Seperation { get; set; } = null;
        public int? MaxRecurrencs { get; set; } = null;
        [Range(0, 6)]
        public short? DayOfWeek { get; set; } = null;
        [Range(0, 3)]
        public short? WeekOfMonth { get; set; } = null;
        [Range(0, 30)]
        public short? DayOfMonth { get; set; } = null;
        [Range(0, 11)]
        public short? MonthOfYear { get; set; } = null;
        public ECustomOccurrsOn? CustomOccurrsOn { get; set; } = null;
    }
}
