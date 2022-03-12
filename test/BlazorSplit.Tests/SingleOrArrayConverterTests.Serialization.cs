using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using BlazorSplit.JsonConverters;
using NUnit.Framework;

namespace BlazorSplit.Tests;

public partial class SingleOrArrayConverterTests
{
    [Test]
    public void ArraySerialization()
    {
        var testObject = new TestObject()
        {
            Values = new List<double>()
            {
                10.0,
                11.0
            }
        };
        var json = JsonSerializer.Serialize(testObject);
        Assert.AreEqual("{\"Values\":[10,11]}", json);
    }

    [Test]
    public void EmptySerialization()
    {
        var testObject = new TestObject()
        {
            Values = new List<double>()
        };
        var json = JsonSerializer.Serialize(testObject);
        Assert.AreEqual("{\"Values\":[]}", json);
    }

    [Test]
    public void NullSerialization()
    {
        var testObject = new TestObject()
        {
            Values = null
        };
        var json = JsonSerializer.Serialize(testObject);
        Assert.AreEqual("{\"Values\":null}", json);
    }

    [Test]
    public void SingleSerialization()
    {
        var testObject = new TestObject()
        {
            Values = new List<double>()
            {
                10.0
            }
        };
        var json = JsonSerializer.Serialize(testObject);
        Assert.AreEqual("{\"Values\":10}", json);
    }
}