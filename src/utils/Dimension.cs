
namespace SimpleGameEngine;

public class Dimension
{
    public int _width;
    public int _height;

    public Dimension(int width, int height)
    {
        Width = width;
        Height = height;
    }
    public Dimension(Dimension dimension) : this(dimension.Width, dimension.Height) { }

    public int Width
    {
        get => _width;
        set
        {
            {
                if (value >= 0)
                    _width = value;
                else
                    throw new ArgumentOutOfRangeException(nameof(Width), "The value can not be negative!");
            }
        }
    }
    public int Height
    {
        get => _height;
        set
        {
            if (value >= 0)
                _height = value;
            else
                throw new ArgumentOutOfRangeException(nameof(Height), "The value can not be negative!");
        }
    }

    public bool Contains(Point point) { return point.X < _width && point.Y < Height && point.X >= 0 && point.Y >= 0; }
    public bool Contains(Dimension dimension) { return dimension.Width <= _width && dimension.Height <= _height; }

    public void Copy(Dimension dimension) { Width = dimension.Width; Height = dimension.Height; }
    public override bool Equals(object? obj) { return obj is Dimension d ? d.Width == Width && d.Height == Height : base.Equals(obj); }
    public override int GetHashCode() { return HashCode.Combine(Width, Height); }
    public override string ToString() { return String.Format("({0},{1})", Width, Height); }
}
