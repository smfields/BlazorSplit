using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorSplit.JsonConverters;

public class EnumJsonConverter : JsonStringEnumConverter
{
    public EnumJsonConverter() : base(JsonNamingPolicy.CamelCase)
    {
        
    }
}