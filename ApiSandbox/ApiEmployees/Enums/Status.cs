using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ApiEmployees.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status
    {
        [Description("Criado")]
        Created = 0,

        [Description("Ativo")]
        Active = 1,

        [Description("Inativo")]
        Inactive = 2,

        [Description("Deletado")]
        Deleted = 3
    }
}
