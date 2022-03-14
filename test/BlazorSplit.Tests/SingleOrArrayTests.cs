using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using BlazorSplit.JsonConverters;
using BlazorSplit.OptionTypes;
using NUnit.Framework;

namespace BlazorSplit.Tests;

[TestFixture]
public class SingleOrArrayTests
{
    private class TestObject
    {
        [JsonConverter(typeof(SingleOrArrayJsonConverter<double>))]
        public SingleOrArray<double>? Property { get; set; }
    }

    [Test]
    public void ArrayDeserialization()
    {
        var json       = "{\"Property\":[10,11]}";
        var testObject = JsonSerializer.Deserialize<TestObject>(json);
        Assert.NotNull(testObject);
        Assert.NotNull(testObject!.Property);
        Assert.AreEqual(new[]
        {
            10.0, 11.0
        }, testObject.Property);
    }

    [Test]
    public void ArraySerialization()
    {
        var testObject = new TestObject()
        {
            Property = new List<double>()
            {
                10.0,
                11.0
            }
        };
        var json = JsonSerializer.Serialize(testObject);
        Assert.AreEqual("{\"Property\":[10,11]}", json);
    }

    [Test]
    public void EmptyDeserialization()
    {
        var json       = "{\"Property\":[]}";
        var testObject = JsonSerializer.Deserialize<TestObject>(json);
        Assert.NotNull(testObject);
        Assert.NotNull(testObject!.Property);
        Assert.IsEmpty(testObject.Property!);
    }

    [Test]
    public void EmptySerialization()
    {
        var testObject = new TestObject()
        {
            Property = new List<double>()
        };
        var json = JsonSerializer.Serialize(testObject);
        Assert.AreEqual("{\"Property\":[]}", json);
    }

    [Test]
    public void NullDeserialization()
    {
        var json       = "{\"Property\":null}";
        var testObject = JsonSerializer.Deserialize<TestObject>(json);
        Assert.NotNull(testObject);
        Assert.Null(testObject!.Property);
    }

    [Test]
    public void NullSerialization()
    {
        var testObject = new TestObject()
        {
            Property = null
        };
        var json = JsonSerializer.Serialize(testObject);
        Assert.AreEqual("{\"Property\":null}", json);
    }

    [Test]
    public void SingleDeserialization()
    {
        var json       = "{\"Property\":10}";
        var testObject = JsonSerializer.Deserialize<TestObject>(json);
        Assert.NotNull(testObject);
        Assert.NotNull(testObject!.Property);
        Assert.AreEqual(10.0, testObject.Property![0]);
    }

    [Test]
    public void SingleSerialization()
    {
        var testObject = new TestObject()
        {
            Property = new List<double>()
            {
                10.0
            }
        };
        var json = JsonSerializer.Serialize(testObject);
        Assert.AreEqual("{\"Property\":10}", json);
    }

    [Test]
    public void StandaloneDeserialization()
    {
        var json = "[10,5.5,100]";
        var jsonSettings = new JsonSerializerOptions()
        {
            Converters =
            {
                new SingleOrArrayJsonConverter<double>()
            }
        };
        var standalone = JsonSerializer.Deserialize<SingleOrArray<double>>(json, jsonSettings);
        Assert.NotNull(standalone);
        Assert.IsNotEmpty(standalone!);
        Assert.AreEqual(10.0, standalone![0]);
    }

    [Test]
    public void StandaloneSerialization()
    {
        SingleOrArray<double> standalone = new double[]
        {
            10.0, 5.5, 100
        };
        var jsonSettings = new JsonSerializerOptions()
        {
            Converters =
            {
                new SingleOrArrayJsonConverter<double>()
            }
        };
        var json = JsonSerializer.Serialize(standalone, jsonSettings);
        Assert.AreEqual("[10,5.5,100]", json);
    }
}