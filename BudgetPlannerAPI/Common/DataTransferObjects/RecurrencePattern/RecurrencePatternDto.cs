using System.ComponentModel.DataAnnotations;

using Common.Constants.Enums;
using Common.DataTransferObjects.Base;

namespace Common.DataTransferObjects.RecurrencePattern
{
    public class RecurrencePatternDto : DtoModifiableBase
    {
        public required ERecurrenceType RecurrenceType { get; set; }
        public int Seperation { get; set; } = 0;
        public int? MaxRecurrencs { get; set; }
        [Range(0, 6)]
        public short? DayOfWeek { get; set; }
        [Range(0, 3)]
        public short? WeekOfMonth { get; set; }
        [Range(0, 30)]
        public short? DayOfMonth { get; set; }
        [Range(0, 11)]
        public short? MonthOfYear { get; set; }
        public ECustomOccurrsOn? CustomOccurrsOn { get; set; }
    }
}
