using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using BlazorSplit.JsonConverters;
using NUnit.Framework;

namespace BlazorSplit.Tests;

public partial class SingleOrArrayConverterTests
{
    [Test]
    public void ArrayDeserialization()
    {
        var json       = "{\"Values\":[10,11]}";
        var testObject = JsonSerializer.Deserialize<TestObject>(json);
        Assert.NotNull(testObject);
        Assert.NotNull(testObject!.Values);
        Assert.AreEqual(new[]
        {
            10.0, 11.0
        }, testObject.Values);
    }

    [Test]
    public void EmptyDeserialization()
    {
        var json       = "{\"Values\":[]}";
        var testObject = JsonSerializer.Deserialize<TestObject>(json);
        Assert.NotNull(testObject);
        Assert.NotNull(testObject!.Values);
        Assert.IsEmpty(testObject.Values!);
    }

    [Test]
    public void NullDeserialization()
    {
        var json       = "{\"Values\":null}";
        var testObject = JsonSerializer.Deserialize<TestObject>(json);
        Assert.NotNull(testObject);
        Assert.Null(testObject!.Values);
    }

    [Test]
    public void SingleDeserialization()
    {
        var json       = "{\"Values\":10}";
        var testObject = JsonSerializer.Deserialize<TestObject>(json);
        Assert.NotNull(testObject);
        Assert.NotNull(testObject!.Values);
        Assert.AreEqual(10.0, testObject.Values![0]);
    }
}