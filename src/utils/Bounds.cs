
namespace SimpleGameEngine;

public class Bounds
{
    public Point Position;
    public Dimension Dimension;
    public Bounds(Point position, Dimension dimension)
    {
        Position = new Point(position);
        Dimension = new Dimension(dimension);
    }
    public Bounds(Bounds bounds) : this(bounds.Position, bounds.Dimension) { }

    public bool Contains(Point point)
    {
        return !(point.X < Position.X
            || point.Y < Position.Y
            || point.X >= Dimension.Width + Position.X
            || point.Y >= Dimension.Height + Position.Y);
    }

    public void Copy(Bounds bounds) { Position.Copy(bounds.Position); Dimension.Copy(bounds.Dimension); }
    public override bool Equals(object? obj) { return obj is Bounds b ? Position.Equals(b.Position) && Dimension.Equals(b.Dimension) : base.Equals(obj); }
    public override int GetHashCode() { return HashCode.Combine(Position.GetHashCode(), Dimension.GetHashCode()); }
    public override string ToString() { return String.Format("({0},{1})", Position, Dimension); }
}
