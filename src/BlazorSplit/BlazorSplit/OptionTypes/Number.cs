using System.Globalization;
using System.Text.Json.Serialization;
using BlazorSplit.JsonConverters;

namespace BlazorSplit.OptionTypes;

[JsonConverter(typeof(NumberJsonConverter))]
public sealed class Number : IEquatable<Number>
{
    public static readonly Number PositiveInfinity = double.PositiveInfinity;
    public static readonly Number NegativeInfinity = double.NegativeInfinity;
    public static readonly Number NaN              = double.NaN;

    public bool   IsInfinity         { get; }
    public bool   IsPositiveInfinity { get; }
    public bool   IsNegativeInfinity { get; }
    public bool   IsNaN              { get; }
    public double Value              { get; }

    public Number(double value)
    {
        IsInfinity         = double.IsInfinity(value);
        IsPositiveInfinity = double.IsPositiveInfinity(value);
        IsNegativeInfinity = double.IsNegativeInfinity(value);
        IsNaN              = double.IsNaN(value);
        Value              = value;
    }

    public Number(int value)
    {
        Value           = value;
    }

    public static implicit operator Number(int value)
    {
        return new Number(value);
    }

    public static explicit operator int(Number number)
    {
        return (int)number.Value;
    }

    public static implicit operator Number(double value)
    {
        return new Number(value);
    }

    public static explicit operator double(Number number)
    {
        return number.Value;
    }

    public bool Equals(double? other)
    {
        return Value == other;
    }

    public bool Equals(Number? other)
    {
        if (ReferenceEquals(null, other))
            return false;
        if (ReferenceEquals(this, other))
            return true;
        return IsInfinity == other.IsInfinity && 
               IsPositiveInfinity == other.IsPositiveInfinity && 
               IsNegativeInfinity == other.IsNegativeInfinity && 
               IsNaN == other.IsNaN && 
               Value.Equals(other.Value);
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is Number other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static bool operator ==(Number? left, Number? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Number? left, Number? right)
    {
        return !Equals(left, right);
    }

    public override string ToString()
    {
        return Value.ToString(CultureInfo.InvariantCulture);
    }
}