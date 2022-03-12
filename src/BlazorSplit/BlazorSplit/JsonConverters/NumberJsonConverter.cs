using System.Text.Json;
using System.Text.Json.Serialization;
using BlazorSplit.OptionTypes;

namespace BlazorSplit.JsonConverters;

public class NumberJsonConverter : JsonConverter<Number>
{
    public override Number? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.Null or JsonTokenType.None => null,
            JsonTokenType.Number                     => new Number(reader.GetDouble()),
            JsonTokenType.String                     => ParseSpecialValue(reader.GetString()),
            _                                        => throw new Exception()
        };
    }

    public override void Write(Utf8JsonWriter writer, Number number, JsonSerializerOptions options)
    {
        if (number == null)
        {
            writer.WriteNullValue();
        }
        else if (number.IsPositiveInfinity)
        {
            writer.WriteStringValue("Infinity");
        }
        else if (number.IsNegativeInfinity)
        {
            writer.WriteStringValue("-Infinity");
        }
        else if (number.IsNaN)
        {
            writer.WriteStringValue("NaN");
        }
        else
        {
            writer.WriteNumberValue(number.Value);
        }
    }

    private Number ParseSpecialValue(string? value)
    {
        return value switch
        {
            "Infinity"  => Number.PositiveInfinity,
            "-Infinity" => Number.NegativeInfinity,
            "NaN"       => Number.NaN,
            _           => throw new Exception()
        };
    }
}