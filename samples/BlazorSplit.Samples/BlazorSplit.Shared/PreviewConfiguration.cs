using BlazorSplit.OptionTypes;

namespace BlazorSplit.Shared;

public class PreviewConfiguration
{
    public Direction Direction { get; set; }
    
    public int Columns { get; set; }
    
    public int MinimumSize { get; set; }
    
    public int MaximumSize { get; set; }
    
    public int GutterSize { get; set; }
    
    public GutterAlign GutterAlignment { get; set; }
    
    public int SnapOffset { get; set; }
    
    public int DragInterval { get; set; }
}