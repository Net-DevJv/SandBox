using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ApiEmployees.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ShiftType
    {
        [Description("Manhã")]
        Morning = 1,

        [Description("Tarde")]
        Afternoon = 2,

        [Description("Noite")]
        Night = 3
    }
}
