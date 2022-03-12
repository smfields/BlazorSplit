using System.Globalization;
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
    public void IntegerSerialization(int number)
    {
        var json = JsonSerializer.Serialize(new Number(number));
        Assert.AreEqual(number.ToString(CultureInfo.InvariantCulture), json);
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