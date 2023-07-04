

namespace SimpleGameEngine;

public class LocalizableComponent : Component
{
    public Action<Point, Point>? OnMove; // successful move(source, destination)
    public Action<LocalizableComponent>? OnCollision; // collision with another component
    public Action<Point>? OnOutOfBounds; // move to out of the ground (point destination)

    protected PositionSystem _system;
    protected internal Point _position;
    protected internal HashSet<LocalizableComponent> _dependency;

    public Point Position { get => new(_position.X, _position.Y); }

    public LocalizableComponent(PositionSystem system, Point position)
    {
        _system = system;
        _position = position;
        _dependency = new HashSet<LocalizableComponent>();
        system.AddComponent(this);
    }

    public void Move(Direction dir)
    {
        _system.Move(this, dir);
    }

    public override string ToString()
    {
        return "PositionComponent:" + base.ToString() + _position.ToString();
    }
}
