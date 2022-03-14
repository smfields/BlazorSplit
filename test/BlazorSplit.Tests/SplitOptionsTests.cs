using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using BlazorSplit.OptionTypes;
using NUnit.Framework;

namespace BlazorSplit.Tests;

public class SplitOptionsTests
{
    [Test]
    public void Deserialization()
    {
        const string json         = "{\"sizes\":[25,25,75],\"minSize\":100,\"maxSize\":\"Infinity\",\"expandToMin\":true,\"gutterSize\":20,\"gutterAlign\":\"end\",\"snapOffset\":0,\"dragInterval\":20,\"direction\":\"vertical\",\"cursor\":\"row-resize\"}";
        var          splitOptions = JsonSerializer.Deserialize<SplitOptions>(json);
        Assert.NotNull(splitOptions);
        Assert.AreEqual(new List<Number>()
        {
            25,
            25,
            75
        }, splitOptions!.Sizes);
        Assert.AreEqual(new List<Number>()
        {
            100
        }, splitOptions.MinSize);
        Assert.AreEqual(new List<Number>()
        {
            Number.PositiveInfinity
        }, splitOptions.MaxSize);
        Assert.IsTrue(splitOptions.ExpandToMin);
        Assert.AreEqual((Number)20, splitOptions.GutterSize);
        Assert.AreEqual(GutterAlign.End, splitOptions.GutterAlign);
        Assert.AreEqual(new List<Number>()
        {
            0
        }, splitOptions.SnapOffset);
        Assert.AreEqual((Number)20, splitOptions.DragInterval);
        Assert.AreEqual(Direction.Vertical, splitOptions.Direction);
        Assert.AreEqual("row-resize", splitOptions.Cursor);
    }

    [Test]
    public void Serialization()
    {
        var splitOptions = new SplitOptions
        {
            Sizes = new Number[]
            {
                25, 25, 75
            },
            MinSize = new Number[]
            {
                100, 200, 300
            },
            MaxSize     = 500,
            ExpandToMin = true,
            GutterAlign = GutterAlign.End,
            SnapOffset = new Number[]
            {
                0
            },
            DragInterval = 20,
            Direction    = Direction.Vertical,
            Cursor       = "row-resize"
        };

        var json = JsonSerializer.Serialize(splitOptions, new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });
        const string expectedJson = "{\"cursor\":\"row-resize\",\"direction\":\"vertical\",\"dragInterval\":20,\"expandToMin\":true,\"gutterAlign\":\"end\",\"maxSize\":500,\"minSize\":[100,200,300],\"sizes\":[25,25,75],\"snapOffset\":0}";
        Assert.AreEqual(expectedJson, json);
    }
}