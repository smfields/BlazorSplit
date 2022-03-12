using System.Text.Json;
using BlazorSplit.OptionTypes;
using NUnit.Framework;

namespace BlazorSplit.Tests;

public partial class NumberTests
{
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
}