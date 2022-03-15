using System.Text.Json.Serialization;
using BlazorSplit.JsonConverters;

namespace BlazorSplit.OptionTypes;

[JsonConverter(typeof(NumberOrArrayJsonConverter))]
public class NumberOrArray : SingleOrArray<Number>
{
    public NumberOrArray(params Number[] values) : base(values)
    {
        
    }
    
    public NumberOrArray(IEnumerable<Number> values) : base(values)
    {
    }

    public NumberOrArray(Number value) : base(value)
    {
    }

    public static implicit operator NumberOrArray(Number[] values)
    {
        return new NumberOrArray(values);
    }

    public static implicit operator NumberOrArray(List<Number> values)
    {
        return new NumberOrArray(values);
    }

    public static implicit operator NumberOrArray(Number value)
    {
        return new NumberOrArray(value);
    }

    public static implicit operator NumberOrArray(int value)
    {
        return new NumberOrArray(value);
    }

    public static implicit operator NumberOrArray(double value)
    {
        return new NumberOrArray(value);
    }
}