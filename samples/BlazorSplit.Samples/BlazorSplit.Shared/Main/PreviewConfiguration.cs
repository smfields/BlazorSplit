using System.ComponentModel;
using System.Runtime.CompilerServices;
using BlazorSplit.OptionTypes;

namespace BlazorSplit.Shared.Main;

public class PreviewConfiguration : INotifyPropertyChanged
{
    public enum CssHorizontalLayout
    {
        Flex,
        Float
    }

    private Direction           _direction        = Direction.Horizontal;
    private int                 _columns          = 2;
    private int                 _minimumSize      = 100;
    private int                 _maximumSize      = 1000;
    private int                 _gutterSize       = 10;
    private GutterAlign         _gutterAlignment  = GutterAlign.Center;
    private int                 _snapOffset       = 30;
    private int                 _dragInterval     = 1;
    private CssHorizontalLayout _horizontalLayout = CssHorizontalLayout.Flex;

    public Direction Direction
    {
        get => _direction;
        set
        {
            if (value == _direction)
                return;
            _direction = value;
            OnPropertyChanged();
        }
    }

    public CssHorizontalLayout HorizontalLayout
    {
        get => _horizontalLayout;
        set
        {
            if (value == _horizontalLayout)
                return;
            _horizontalLayout = value;
            OnPropertyChanged();
        }
    }

    public int Columns
    {
        get => _columns;
        set
        {
            if (value == _columns)
                return;
            _columns = value;
            OnPropertyChanged();
        }
    }

    public int MinimumSize
    {
        get => _minimumSize;
        set
        {
            if (value == _minimumSize)
                return;
            _minimumSize = value;
            OnPropertyChanged();
        }
    }

    public int MaximumSize
    {
        get => _maximumSize;
        set
        {
            if (value == _maximumSize)
                return;
            _maximumSize = value;
            OnPropertyChanged();
        }
    }

    public int GutterSize
    {
        get => _gutterSize;
        set
        {
            if (value == _gutterSize)
                return;
            _gutterSize = value;
            OnPropertyChanged();
        }
    }

    public GutterAlign GutterAlignment
    {
        get => _gutterAlignment;
        set
        {
            if (value == _gutterAlignment)
                return;
            _gutterAlignment = value;
            OnPropertyChanged();
        }
    }

    public int SnapOffset
    {
        get => _snapOffset;
        set
        {
            if (value == _snapOffset)
                return;
            _snapOffset = value;
            OnPropertyChanged();
        }
    }

    public int DragInterval
    {
        get => _dragInterval;
        set
        {
            if (value == _dragInterval)
                return;
            _dragInterval = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}