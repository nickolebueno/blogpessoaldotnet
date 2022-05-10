using System.Text.Json.Serialization;

namespace BlogPessoal.src.utils
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserType
    {
        NORMAL,
        ADMIN
    }
}
