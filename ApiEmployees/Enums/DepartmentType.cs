using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ApiEmployees.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DepartmentType
    {
        [Description("Recursos Humanos")]
        RH = 0,

        [Description("Financeiro")]
        Financial = 1,

        [Description("Compras")]
        Purchasing = 2,

        [Description("Suporte")]
        Service = 3,

        [Description("Manutenção")]
        Maintenance = 4
    }
}
