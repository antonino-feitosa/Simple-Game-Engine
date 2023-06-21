
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

    public int Width
    {
        get => _width;
        set
        {
            {
                if (value >= 0)
                    _width = value;
                else
                    throw new ArgumentException("The value can not be negative!", nameof(Width));
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
                throw new ArgumentException("The value can not be negative!", nameof(Height));
        }
    }

    public void Copy(Dimension dimension) { Width = dimension.Width; Height = dimension.Height; }
    public override bool Equals(object? obj) { return obj is Dimension d ? d.Width == Width && d.Height == Height : base.Equals(obj); }
    public override int GetHashCode() { return HashCode.Combine(Width, Height); }
    public override string ToString() { return String.Format("({0},{1})", Width, Height); }
}
