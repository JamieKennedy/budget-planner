using Common.Constants.Enums;
using Common.DataTransferObjects.Base;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Common.DataTransferObjects.Income
{
    public class IncomeDto : DtoBase
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid AccountId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public EOccurrence Occurrence { get; set; }

        public EOccurrsOn OccurrsOn { get; set; }
        public int? CustomOccurrsOn { get; set; }
    }
}
