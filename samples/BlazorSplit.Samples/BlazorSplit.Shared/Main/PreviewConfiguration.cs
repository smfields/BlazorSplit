using BlazorSplit.OptionTypes;

namespace BlazorSplit.Shared.Main;

public class PreviewConfiguration
{
    public Direction Direction { get; set; } = Direction.Horizontal;

    public int Columns { get; set; } = 2;

    public int MinimumSize { get; set; } = 100;

    public int MaximumSize { get; set; } = 1000;

    public int GutterSize { get; set; } = 10;

    public GutterAlign GutterAlignment { get; set; } = GutterAlign.Center;

    public int SnapOffset { get; set; } = 30;

    public int DragInterval { get; set; } = 1;
}