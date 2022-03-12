using System.Collections.Generic;
using System.Text.Json;
using BlazorSplit.OptionTypes;
using NUnit.Framework;

namespace BlazorSplit.Tests;

public partial class SplitOptionsTests
{
    [Test]
    public void Serialization()
    {
        var splitOptions = new SplitOptions
        {
            Sizes = new List<Number>()
            {
                25,
                25,
                75
            },
            MinSize = new List<Number>()
            {
                100,
                200,
                300
            },
            MaxSize = new List<Number>()
            {
                500
            },
            ExpandToMin = true,
            GutterSize  = 20,
            GutterAlign = GutterAlign.End,
            SnapOffset = new List<Number>()
            {
                0
            },
            DragInterval = 20,
            Direction    = Direction.Vertical,
            Cursor       = "row-resize"
        };

        var          json         = JsonSerializer.Serialize(splitOptions);
        const string expectedJson = "{\"sizes\":[25,25,75],\"minSize\":[100,200,300],\"maxSize\":500,\"expandToMin\":true,\"gutterSize\":20,\"gutterAlign\":\"end\",\"snapOffset\":0,\"dragInterval\":20,\"direction\":\"vertical\",\"cursor\":\"row-resize\"}";
        Assert.AreEqual(expectedJson, json);
    }
}