

namespace SimpleGameEngine;

/// <summary>
/// This system performs discrete position, moviment and collision of <c>LocalizableComponent</c><see cref="LocalizableComponent"/>.
/// </summary>
public class PositionSystem : SystemBase<LocalizableComponent>
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
    protected Dictionary<Point, LocalizableComponent> _destinationToComponent;
    protected Dictionary<LocalizableComponent, Point> _componentToDestination;
    protected Dictionary<LocalizableComponent, Point> _outOfBounds;
    protected HashSet<LocalizableComponent> _free;

    public PositionSystem(HashSet<Point> ground)
    {
        _ground = ground;
        _destinationToComponent = new Dictionary<Point, LocalizableComponent>();
        _componentToDestination = new Dictionary<LocalizableComponent, Point>();
        _outOfBounds = new Dictionary<LocalizableComponent, Point>();
        _free = new HashSet<LocalizableComponent>();
    }

    protected internal void Move(LocalizableComponent comp, Direction dir)
    {
        var destination = dir.Next(comp._position);
        if (_ground.Contains(destination))
        {
            _componentToDestination.Add(comp, destination);
            if (_destinationToComponent.TryGetValue(destination, out LocalizableComponent? value))
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
            var other = _destinationToComponent[pair.Value];
            pair.Key.OnCollision?.Invoke(other);
        };
        _componentToDestination.Clear();
    }

    private void MoveWithDependencies(){
        var collision = new HashSet<LocalizableComponent>();
        while (_free.Count > 0)
        {
            var comp = _free.First();
            _free.Remove(comp);
            var dest = _componentToDestination[comp];
            if (_destinationToComponent.TryGetValue(dest, out LocalizableComponent? value))
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

    private void FireMove(LocalizableComponent comp, Point dest)
    {
        _destinationToComponent.Remove(comp._position);
        _destinationToComponent.Add(dest, comp);
        comp.OnMove?.Invoke(comp._position, dest);
        comp._position = dest;
    }

    internal override void AddComponent(LocalizableComponent component){
        _destinationToComponent.Add(component._position, component);
    }
}
