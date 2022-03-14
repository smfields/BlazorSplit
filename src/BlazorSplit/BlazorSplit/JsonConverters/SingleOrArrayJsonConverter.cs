using System.Text.Json;
using System.Text.Json.Serialization;
using BlazorSplit.OptionTypes;

namespace BlazorSplit.JsonConverters;

public class SingleOrArrayJsonConverter<TItem> : JsonConverter<SingleOrArray<TItem>>
{
    public bool CanWrite { get; }

    public SingleOrArrayJsonConverter() : this(true)
    {
    }

    public SingleOrArrayJsonConverter(bool canWrite)
    {
        CanWrite = canWrite;
    }

    public override SingleOrArray<TItem> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.Null:
                return null;
            case JsonTokenType.StartArray:
                var list = new List<TItem>();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndArray)
                        break;
                    list.Add(JsonSerializer.Deserialize<TItem>(ref reader, options));
                }

                return new SingleOrArray<TItem>(list);
            default:
                return new List<TItem>
                {
                    JsonSerializer.Deserialize<TItem>(ref reader, options)
                };
        }
    }

    public override void Write(Utf8JsonWriter writer, SingleOrArray<TItem> value, JsonSerializerOptions options)
    {
        if (CanWrite && value.Values.Count == 1)
            JsonSerializer.Serialize(writer, value.Values.First(), options);
        else
        {
            writer.WriteStartArray();
            foreach (TItem? item in value.Values)
                JsonSerializer.Serialize(writer, item, options);
            writer.WriteEndArray();
        }
    }
}