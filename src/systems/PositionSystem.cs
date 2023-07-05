

namespace SimpleGameEngine;

/// <summary>
/// This system performs discrete position, moviment and collision of <c>LocalizableComponent</c><see cref="PositionableComponent"/>.
/// </summary>
public class PositionSystem : SystemBase<PositionableComponent>
{
    public static readonly Direction UP = new(0, +1);
    public static readonly Direction UP_LEFT = new(-1, +1);
    public static readonly Direction UP_RIGHT = new(+1, +1);
    public static readonly Direction DOWN = new(0, -1);
    public static readonly Direction DOWN_LEFT = new(-1, -1);
    public static readonly Direction DOWN_RIGHT = new(+1, -1);
    public static readonly Direction LEFT = new(-1, 0);
    public static readonly Direction RIGHT = new(+1, 0);

    protected HashSet<Point> _ground;
    protected Dictionary<Point, PositionableComponent> _destinationToComponent;
    protected Dictionary<PositionableComponent, Point> _componentToDestination;
    protected Dictionary<PositionableComponent, Point> _outOfBounds;
    protected HashSet<PositionableComponent> _free;

    public PositionSystem(HashSet<Point> ground)
    {
        _ground = ground;
        _destinationToComponent = new Dictionary<Point, PositionableComponent>();
        _componentToDestination = new Dictionary<PositionableComponent, Point>();
        _outOfBounds = new Dictionary<PositionableComponent, Point>();
        _free = new HashSet<PositionableComponent>();
    }

    protected internal void Move(PositionableComponent comp, Direction dir)
    {
        var destination = dir.Next(comp._position);
        if (_ground.Contains(destination))
        {
            _componentToDestination.Add(comp, destination);
            if (_destinationToComponent.TryGetValue(destination, out PositionableComponent? value))
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

    public override void Process()
    {
        foreach (var pair in _outOfBounds) { pair.Key.OnOutOfBounds?.Invoke(pair.Value); };
        _outOfBounds.Clear();

        MoveWithDependencies();

        foreach (var pair in _componentToDestination)
        {
            var source = pair.Key;
            var other = _destinationToComponent[pair.Value];
            source.OnCollision?.Invoke(other);
            other.OnCollision?.Invoke(source);
        };
        _componentToDestination.Clear();
    }

    private void MoveWithDependencies(){
        var collision = new HashSet<PositionableComponent>();
        while (_free.Count > 0)
        {
            var comp = _free.First();
            _free.Remove(comp);
            var dest = _componentToDestination[comp];
            if (_destinationToComponent.TryGetValue(dest, out PositionableComponent? value))
            {
                var other = value;
                other._dependency.Add(comp);
            }
            else
            {
                FireMove(comp, dest);
                _componentToDestination.Remove(comp);
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

    private void FireMove(PositionableComponent comp, Point dest)
    {
        _destinationToComponent.Remove(comp._position);
        _destinationToComponent.Add(dest, comp);
        var source = new Point();
        source.Copy(comp._position);
        comp._position = dest;
        comp.OnMove?.Invoke(source, dest);
    }

    internal override void AddComponent(PositionableComponent component){
        _destinationToComponent.Add(component._position, component);
    }
}
