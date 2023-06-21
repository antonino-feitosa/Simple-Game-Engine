
namespace SimpleGameEngine;

public class Point
{
    public int X;
    public int Y;

    public Point(int x = 0, int y = 0)
    {
        X = x;
        Y = y;
    }
    public void Copy(Point point) { X = point.X; Y = point.Y; }
    public override bool Equals(object? obj) { return obj is Point p ? p.X == X && p.Y == Y : base.Equals(obj); }
    public override int GetHashCode() { return HashCode.Combine(X, Y); }
    public override string ToString() { return String.Format("({0},{1})", X, Y); }
}
