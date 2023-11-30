﻿using System.Text.Json.Serialization;

using Newtonsoft.Json.Converters;

namespace Common.Constants.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ERecurrenceType
    {
        Daily,
        Weekly,
        Monthly,
        Yearly
    }
}
