
namespace SGE;

public class Vector2
{
    private const double CLOSE_ENOUGH = 0.01;
    public double X;
    public double Y;

    public Vector2(double x = 0, double y = 0)
    {
        Set(x, y);
    }
    public void Set(double x, double y)
    {
        X = x;
        Y = y;
    }
    public void Sum(Vector2 vet)
    {
        X += vet.X;
        Y += vet.Y;
    }
    protected internal bool IsCloseEnough(Vector2 vet)
    {
        return Math.Abs(X - vet.X) + Math.Abs(Y - vet.Y) <= CLOSE_ENOUGH;
    }
    protected internal bool IsZero()
    {
        return X + Y == 0;
    }
    public override bool Equals(object? obj)
    {
        return obj is Vector2 vet ? vet.X == X && vet.Y == Y : base.Equals(obj);
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
    public override string ToString()
    {
        return String.Format("Vector2({0}, {1})", X, Y);
    }
}


public class Direction
{
    public readonly int X;
    public readonly int Y;
    protected internal Direction(int x, int y)
    {
        X = x;
        Y = y;
    }
    public Position Next(Position p)
    {
        return new Position(p.X + X, p.Y + Y);
    }
}

public class Position
{
    public int X;
    public int Y;

    public Position(int x = 0, int y = 0)
    {
        X = x;
        Y = y;
    }
    public void Copy(Position point) { X = point.X; Y = point.Y; }
    public override bool Equals(object? obj) { return obj is Position p ? p.X == X && p.Y == Y : base.Equals(obj); }
    public override int GetHashCode() { return HashCode.Combine(X, Y); }
    public override string ToString() { return String.Format("({0},{1})", X, Y); }
}

public class Dimension
{
    public int _width;
    public int _height;

    public Dimension(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public int Width { get => _width; set => _width = value >= 0 ? _width = value : _width; }
    public int Height { get => _height; set => _height = value >= 0 ? _height = value : _height; }

    public void Copy(Dimension dimension) { Width = dimension.Width; Height = dimension.Height; }
    public override bool Equals(object? obj) { return obj is Dimension d ? d.Width == Width && d.Height == Height : base.Equals(obj); }
    public override int GetHashCode() { return HashCode.Combine(Width, Height); }
    public override string ToString() { return String.Format("({0},{1})", Width, Height); }
}
