

namespace SimpleGameEngine;

public class PositionSystem : System
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
    protected Dictionary<Point, Localizable> _components;
    protected Dictionary<Localizable, Point> _moving;
    protected Dictionary<Localizable, Point> _outOfBounds;
    protected HashSet<Localizable> _free;

    public PositionSystem(HashSet<Point> ground)
    {
        _ground = ground;
        _components = new Dictionary<Point, Localizable>();
        _moving = new Dictionary<Localizable, Point>();
        _outOfBounds = new Dictionary<Localizable, Point>();
        _free = new HashSet<Localizable>();
    }

    public Localizable CreateComponent(int x, int y)
    {
        return new Localizable(new Point(x, y), this);
    }

    protected internal void Move(Localizable comp, Direction dir)
    {
        var destination = dir.Next(comp._position);
        if (_ground.Contains(destination))
        {
            _moving.Add(comp, destination);
            if (_components.TryGetValue(destination, out Localizable? value))
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
        var collision = new HashSet<Localizable>();
        while (_free.Count > 0)
        {
            var comp = _free.First();
            _free.Remove(comp);
            var dest = _moving[comp];
            if (_components.TryGetValue(dest, out Localizable? value))
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

    private void DoMove(Localizable comp, Point dest)
    {
        _components.Remove(comp._position);
        _components.Add(dest, comp);
        comp.OnMove?.Invoke(comp._position, dest);
        comp._position = dest;
    }

    public class Localizable : Component
    {
        protected PositionSystem _system;
        protected internal Point _position;
        protected internal HashSet<Localizable> _dependency;
        public Action<Point, Point>? OnMove; // successful move
        public Action<Localizable>? OnCollision; // collision with another component
        public Action<Point>? OnOutOfBounds; // move to out of the ground

        protected internal Localizable(Point position, PositionSystem system)
        {
            _system = system;
            _position = position;
            _dependency = new HashSet<Localizable>();
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
}
