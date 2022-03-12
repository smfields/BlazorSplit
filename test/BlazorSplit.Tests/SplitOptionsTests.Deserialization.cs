using System.Collections.Generic;
using System.Text.Json;
using BlazorSplit.OptionTypes;
using NUnit.Framework;

namespace BlazorSplit.Tests;

public partial class SplitOptionsTests
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
}