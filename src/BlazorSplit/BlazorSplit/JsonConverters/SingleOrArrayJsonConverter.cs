using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorSplit.JsonConverters;

public class SingleOrArrayJsonConverter<TItem> : SingleOrArrayJsonConverter<List<TItem>, TItem>
{
    public SingleOrArrayJsonConverter() : this(true) { }
    public SingleOrArrayJsonConverter(bool canWrite) : base(canWrite) { }
}

public class SingleOrArrayJsonConverter<TCollection, TItem> : JsonConverter<TCollection> where TCollection : class, ICollection<TItem>, new()
{
    public SingleOrArrayJsonConverter() : this(true) { }
    public SingleOrArrayJsonConverter(bool canWrite) => CanWrite = canWrite;

    public bool CanWrite { get; }

    public override TCollection Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.Null:
                return null;
            case JsonTokenType.StartArray:
                var list = new TCollection();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndArray)
                        break;
                    list.Add(JsonSerializer.Deserialize<TItem>(ref reader, options));
                }
                return list;
            default:
                return new TCollection { JsonSerializer.Deserialize<TItem>(ref reader, options) };
        }
    }

    public override void Write(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options)
    {
        if (CanWrite && value.Count == 1)
        {
            JsonSerializer.Serialize(writer, value.First(), options);
        }
        else
        {
            writer.WriteStartArray();
            foreach (var item in value)
                JsonSerializer.Serialize(writer, item, options);
            writer.WriteEndArray();
        }
    }
}