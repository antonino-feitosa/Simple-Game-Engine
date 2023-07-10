
namespace SimpleGameEngine;

public class Vector2
{
    private const double CLOSE_ENOUGH = 0.01;
    public double X;
    public double Y;

    public Vector2(double x = 0, double y = 0)
    {
        X = x;
        Y = y;
    }
    public Vector2(Vector2 other) : this(other.X, other.Y) { }
    public void Sum(Vector2 vet)
    {
        X += vet.X;
        Y += vet.Y;
    }
    protected internal bool IsCloseEnough(Vector2 vet) { return Math.Abs(X - vet.X) + Math.Abs(Y - vet.Y) <= CLOSE_ENOUGH; }
    protected internal bool IsZero() { return X + Y == 0; }
    public override bool Equals(object? obj) { return obj is Vector2 vet ? vet.X == X && vet.Y == Y : base.Equals(obj); }
    public override int GetHashCode() { return HashCode.Combine(X, Y); }
    public override string ToString() { return String.Format("Vector2({0}, {1})", X, Y); }
}
