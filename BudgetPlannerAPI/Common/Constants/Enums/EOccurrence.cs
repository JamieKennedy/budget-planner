using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Common.Constants.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EOccurrence
    {
        OneOff,
        Daily,
        Weekly,
        Monthly,
        Quarterly,
        Yearly
    }
}
