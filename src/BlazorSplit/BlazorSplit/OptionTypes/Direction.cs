using System.Text.Json.Serialization;
using BlazorSplit.JsonConverters;

namespace BlazorSplit.OptionTypes;

[JsonConverter(typeof(EnumJsonConverter))]
public enum Direction
{
    Horizontal,
    Vertical
}