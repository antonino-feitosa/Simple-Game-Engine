

namespace SimpleGameEngine;

public class LocalizableComponent : Component
{
    protected PositionSystem _system;
    protected internal Point _position;
    protected internal HashSet<LocalizableComponent> _dependency;
    public Action<Point, Point>? OnMove; // successful move
    public Action<LocalizableComponent>? OnCollision; // collision with another component
    public Action<Point>? OnOutOfBounds; // move to out of the ground

    protected internal LocalizableComponent(Point position, PositionSystem system)
    {
        _system = system;
        _position = position;
        _dependency = new HashSet<LocalizableComponent>();
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