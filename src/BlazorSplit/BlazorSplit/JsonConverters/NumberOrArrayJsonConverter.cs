using System.Text.Json;
using System.Text.Json.Serialization;
using BlazorSplit.OptionTypes;

namespace BlazorSplit.JsonConverters;

public class NumberOrArrayJsonConverter : JsonConverter<NumberOrArray>
{
    public override NumberOrArray Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.Null:
                return null;
            case JsonTokenType.StartArray:
                var list = new List<Number>();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndArray)
                        break;
                    list.Add(JsonSerializer.Deserialize<Number>(ref reader, options));
                }

                return new NumberOrArray(list);
            default:
                return new List<Number>
                {
                    JsonSerializer.Deserialize<Number>(ref reader, options)
                };
        }
    }

    public override void Write(Utf8JsonWriter writer, NumberOrArray value, JsonSerializerOptions options)
    {
        if (value.Values.Count == 1)
            JsonSerializer.Serialize(writer, value.Values.First(), options);
        else
        {
            writer.WriteStartArray();
            foreach (Number? number in value.Values)
                JsonSerializer.Serialize(writer, number, options);
            writer.WriteEndArray();
        }
    }
}