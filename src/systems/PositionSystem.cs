

namespace SimpleGameEngine;

public class PositionSystem : ISystem
{
    public readonly Direction UP = new(0, -1);
    public readonly Direction UP_LEFT = new(-1, -1);
    public readonly Direction UP_RIGHT = new(+1, -1);
    public readonly Direction DOWN = new(0, +1);
    public readonly Direction DOWN_LEFT = new(-1, +1);
    public readonly Direction DOWN_RIGHT = new(+1, +1);
    public readonly Direction LEFT = new(-1, 0);
    public readonly Direction RIGHT = new(+1, -0);

    protected HashSet<Point> _ground;
    protected Dictionary<Point, LocalizableComponent> _components;
    protected Dictionary<LocalizableComponent, Point> _moving;
    protected Dictionary<LocalizableComponent, Point> _outOfBounds;
    protected HashSet<LocalizableComponent> _free;

    // TODO add standby component
    // TODO move only the components in destination different of location
    // TODO Handle enable disable entity
    public PositionSystem(HashSet<Point> ground)
    {
        _ground = ground;
        _components = new Dictionary<Point, LocalizableComponent>();
        _moving = new Dictionary<LocalizableComponent, Point>();
        _outOfBounds = new Dictionary<LocalizableComponent, Point>();
        _free = new HashSet<LocalizableComponent>();
    }

    protected internal void Move(LocalizableComponent comp, Direction dir)
    {
        var destination = dir.Next(comp._position);
        if (_ground.Contains(destination))
        {
            _moving.Add(comp, destination);
            if (_components.TryGetValue(destination, out LocalizableComponent? value))
            {
                var other = value;
                other._dependency.Add(comp);
            }
            else
            {
                _free.Add(comp);
            }
        }
        else
        {
            _outOfBounds.Add(comp, destination);
        }
    }

    private void MoveWithDependencies(){
        var collision = new HashSet<LocalizableComponent>();
        while (_free.Count > 0)
        {
            var comp = _free.First();
            _free.Remove(comp);
            var dest = _moving[comp];
            if (_components.TryGetValue(dest, out LocalizableComponent? value))
            {
                var other = value;
                other._dependency.Add(comp);
            }
            else
            {
                DoMove(comp, dest);
                _moving.Remove(comp);
                if (comp._dependency.Count > 0)
                {
                    var next = comp._dependency.First();
                    _free.Add(next);
                    comp._dependency.Remove(next);
                    foreach (var d in comp._dependency) { d.OnCollision?.Invoke(next); }
                    comp._dependency.Clear();
                }
            }
        }
    }

    public void Process()
    {
        foreach (var pair in _outOfBounds) { pair.Key.OnOutOfBounds?.Invoke(pair.Value); };
        _outOfBounds.Clear();

        MoveWithDependencies();

        foreach (var pair in _moving)
        {
            var other = _components[pair.Value];
            pair.Key.OnCollision?.Invoke(other);
        };
        _moving.Clear();
    }

    private void DoMove(LocalizableComponent comp, Point dest)
    {
        _components.Remove(comp._position);
        _components.Add(dest, comp);
        comp.OnMove?.Invoke(comp._position, dest);
        comp._position = dest;
    }
}
