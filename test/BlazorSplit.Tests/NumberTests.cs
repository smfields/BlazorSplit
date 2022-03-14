using System.Globalization;
using System.Text.Json;
using BlazorSplit.OptionTypes;
using NUnit.Framework;

namespace BlazorSplit.Tests;

[TestFixture]
public class NumberTests
{
    private class TestNumberContainer
    {
        public Number Number { get; set; }
    }

    [Test]
    [TestCase(0.0)]
    [TestCase(0.5)]
    [TestCase(-1.0)]
    [TestCase(-1.333)]
    [TestCase(1.0)]
    [TestCase(1.756)]
    [TestCase(5.0)]
    [TestCase(5.555555)]
    [TestCase(double.MinValue)]
    [TestCase(double.MaxValue)]
    [TestCase(double.PositiveInfinity)]
    [TestCase(double.NegativeInfinity)]
    [TestCase(double.NaN)]
    public void DoubleDeserialization(double value)
    {
        var number             = new Number(value);
        var json               = JsonSerializer.Serialize(number);
        var deserializedNumber = JsonSerializer.Deserialize<Number>(json);
        Assert.AreEqual(number, deserializedNumber);
    }

    [Test]
    [TestCase(0.0)]
    [TestCase(0.5)]
    [TestCase(-1.0)]
    [TestCase(-1.333)]
    [TestCase(1.0)]
    [TestCase(1.756)]
    [TestCase(5.0)]
    [TestCase(5.555555)]
    [TestCase(double.MinValue)]
    [TestCase(double.MaxValue)]
    public void DoubleSerialization(double number)
    {
        var json = JsonSerializer.Serialize(new Number(number));
        Assert.AreEqual(number.ToString(CultureInfo.InvariantCulture), json);
    }

    [Test]
    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(1)]
    [TestCase(5)]
    [TestCase(int.MinValue)]
    [TestCase(int.MaxValue)]
    public void IntegerDeserialization(int value)
    {
        var number             = new Number(value);
        var json               = JsonSerializer.Serialize(number);
        var deserializedNumber = JsonSerializer.Deserialize<Number>(json);
        Assert.AreEqual(number, deserializedNumber);
    }

    [Test]
    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(1)]
    [TestCase(5)]
    [TestCase(int.MinValue)]
    [TestCase(int.MaxValue)]
    public void IntegerSerialization(int number)
    {
        var json = JsonSerializer.Serialize(new Number(number));
        Assert.AreEqual(number.ToString(CultureInfo.InvariantCulture), json);
    }

    [Test]
    [TestCase(0.0)]
    [TestCase(0.5)]
    [TestCase(-1.0)]
    [TestCase(-1.333)]
    [TestCase(1.0)]
    [TestCase(1.756)]
    [TestCase(5.0)]
    [TestCase(5.555555)]
    [TestCase(double.PositiveInfinity)]
    [TestCase(double.NegativeInfinity)]
    [TestCase(double.MinValue)]
    [TestCase(double.MaxValue)]
    public void NumberFromDouble(double value)
    {
        Number number = value;
        Assert.True(number.Value == value, "number.Value == value");
    }

    [Test]
    public void NumberFromInfinity()
    {
        Number number = Number.PositiveInfinity;
        Assert.True(number.IsInfinity, "IsInfinity(Number.PositiveInfinity)");
        Assert.True(number.IsPositiveInfinity, "IsPositiveInfinity(Number.PositiveInfinity)");
        Assert.False(number.IsNegativeInfinity, "IsNegativeInfinity(Number.PositiveInfinity)");

        number = Number.NegativeInfinity;
        Assert.True(number.IsInfinity, "IsInfinity(Number.NegativeInfinity)");
        Assert.True(number.IsNegativeInfinity, "IsNegativeInfinity(Number.NegativeInfinity)");
        Assert.False(number.IsPositiveInfinity, "IsPositiveInfinity(Number.NegativeInfinity)");

        number = double.PositiveInfinity;
        Assert.True(number.IsInfinity, "IsInfinity(double.PositiveInfinity)");

        number = double.NegativeInfinity;
        Assert.True(number.IsInfinity, "IsInfinity(double.NegativeInfinity)");
    }

    [Test]
    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(1)]
    [TestCase(5)]
    [TestCase(int.MinValue)]
    [TestCase(int.MaxValue)]
    public void NumberFromInteger(int value)
    {
        Number number = value;
        Assert.True(number.Value == value, "number.Value == value");
    }

    [Test]
    public void NumberFromNaN()
    {
        Number number = Number.NaN;
        Assert.True(number.IsNaN, "IsNaN(Number.NaN)");

        number = double.NaN;
        Assert.True(number.IsNaN, "IsNaN(double.NaN)");

        number = 5.0;
        Assert.False(number.IsNaN, "IsNaN(5.0)");
    }

    [Test]
    public void SpecialValueDeserialization()
    {
        Number number             = Number.PositiveInfinity;
        var    json               = JsonSerializer.Serialize(number);
        var    deserializedNumber = JsonSerializer.Deserialize<Number>(json);
        Assert.AreEqual(number, deserializedNumber);

        number             = Number.NegativeInfinity;
        json               = JsonSerializer.Serialize(number);
        deserializedNumber = JsonSerializer.Deserialize<Number>(json);
        Assert.AreEqual(number, deserializedNumber);

        number             = Number.NaN;
        json               = JsonSerializer.Serialize(number);
        deserializedNumber = JsonSerializer.Deserialize<Number>(json);
        Assert.AreEqual(number, deserializedNumber);
    }

    [Test]
    public void SpecialValueSerialization()
    {
        Number positiveInfinity = Number.PositiveInfinity;
        var    json             = JsonSerializer.Serialize(positiveInfinity);
        Assert.AreEqual("\"Infinity\"", json);

        Number negativeInfinity = Number.NegativeInfinity;
        json = JsonSerializer.Serialize(negativeInfinity);
        Assert.AreEqual("\"-Infinity\"", json);

        Number nan = Number.NaN;
        json = JsonSerializer.Serialize(nan);
        Assert.AreEqual("\"NaN\"", json);
    }
}