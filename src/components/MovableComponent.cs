
namespace SimpleGameEngine;


public class MovableComponent : Component
{
    public Action<Vector2>? OnMoveStart;
    public Action<Vector2>? OnMoveEnd;
    public Action<Vector2>? OnMoveIncrement;
    public Action<Vector2>? OnMoveIdle;

    protected internal bool _fired;
    protected internal bool _moving;
    protected internal Vector2 _position;
    protected internal Vector2 _destination;
    protected internal Vector2 _velocity;
    protected int _framesToMove;
    protected PositionableComponent _positionComponent;

    public int FramesToMove
    {
        get => _framesToMove;
        set
        {
            if (_framesToMove > 0)
                _framesToMove = value;
            else throw new ArgumentException("The value must be positive!", nameof(FramesToMove));
        }
    }

    public Vector2 Position { get => new(_position); }

    public MovableComponent(MotionSystem motionSystem, PositionableComponent comp)
    {
        _position = new Vector2();
        _destination = new Vector2();
        _velocity = new Vector2();
        _framesToMove = 32;
        _positionComponent = comp;
        _positionComponent.OnMove += ApplyPositionIncrement;
        _moving = false;
        _fired = false;
        motionSystem.AddComponent(this);
    }

    private void ApplyPositionIncrement(Point source, Point dest)
    {
        _position = new Vector2(source.X, source.Y);
        _destination = new Vector2(dest.X, dest.Y);
        _velocity = new Vector2((dest.X - source.X) / (double)_framesToMove, (dest.Y - source.Y) / (double)_framesToMove);
        _fired = true;
    }

    public override string ToString()
    {
        return "MovableComponent:" + _positionComponent.ToString();
    }
}
