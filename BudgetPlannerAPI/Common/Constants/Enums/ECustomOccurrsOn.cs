using System.Text.Json.Serialization;

using Newtonsoft.Json.Converters;

namespace Common.Constants.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ECustomOccurrsOn
    {
        FirstOf,
        LastOf,
        FirstWorkingDay,
        LastWorkingDay
    }
}
