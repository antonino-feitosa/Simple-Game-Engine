
namespace SimpleGameEngine;

public class Direction
{
    public readonly int X;
    public readonly int Y;

    public Direction Opposite { get => new(-X, -Y); }
    protected internal Direction(int x, int y)
    {
        X = x;
        Y = y;
    }
    public Point Next(Point p)
    {
        return new Point(p.X + X, p.Y + Y);
    }
}
