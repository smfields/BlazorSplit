using System.Text.Json.Serialization;
using BlazorSplit.JsonConverters;
using BlazorSplit.OptionTypes;

namespace BlazorSplit;

public class SplitOptions
{
    /// <summary>
    /// Initial sizes of each element in percents or CSS values.
    /// </summary>
    [JsonPropertyName("sizes")]
    [JsonConverter(typeof(SingleOrArrayJsonConverter<Number>))]
    public List<Number>?    Sizes       { get; init; }
    
    /// <summary>
    /// Minimum size of each element.
    /// </summary>
    [JsonPropertyName("minSize")]
    [JsonConverter(typeof(SingleOrArrayJsonConverter<Number>))]
    public List<Number>? MinSize { get; init; }
    
    /// <summary>
    /// Maximum size of each element.
    /// </summary>
    [JsonPropertyName("maxSize")]
    [JsonConverter(typeof(SingleOrArrayJsonConverter<Number>))]
    public List<Number>? MaxSize { get; init; }
    
    /// <summary>
    /// When the split is created, if ExpandToMin is true, the minSize for each element overrides the percentage value from the sizes option.
    /// </summary>
    [JsonPropertyName("expandToMin")]
    public bool?         ExpandToMin { get; init; }
    
    /// <summary>
    /// Gutter size in pixels.
    /// </summary>
    [JsonPropertyName("gutterSize")]
    public Number? GutterSize { get; init; } 
    
    /// <summary>
    /// Determines how the gutter aligns between the two elements.
    /// </summary>
    [JsonPropertyName("gutterAlign")]
    public GutterAlign? GutterAlign { get; init; }
    
    /// <summary>
    /// Snap to minimum size offset in pixels.
    /// </summary>
    [JsonPropertyName("snapOffset")]
    [JsonConverter(typeof(SingleOrArrayJsonConverter<Number>))]
    public List<Number>? SnapOffset { get; init; }
    
    /// <summary>
    /// Drag this number of pixels at a time. Defaults to 1 for smooth dragging, but can be set to a pixel value to give more control over the resulting sizes.
    /// Works particularly well when the gutterSize is set to the same size.
    /// </summary>
    [JsonPropertyName("dragInterval")]
    public Number? DragInterval { get; init; }
    
    /// <summary>
    /// Direction to split.
    /// </summary>
    [JsonPropertyName("direction")]
    public Direction? Direction { get; init; }
    
    /// <summary>
    /// Cursor to display while dragging.
    /// </summary>
    [JsonPropertyName("cursor")]
    public string? Cursor { get; init; }
}