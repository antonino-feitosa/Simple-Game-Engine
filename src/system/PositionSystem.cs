

namespace SGE;

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

    protected HashSet<SGE.Position> _ground;
    protected Dictionary<SGE.Position, Position> _components;
    protected Dictionary<Position, SGE.Position> _moving;
    protected Dictionary<Position, SGE.Position> _outOfBounds;
    protected HashSet<Position> _free;

    public PositionSystem(HashSet<SGE.Position> ground)
    {
        _ground = ground;
        _components = new Dictionary<SGE.Position, Position>();
        _moving = new Dictionary<Position, SGE.Position>();
        _outOfBounds = new Dictionary<Position, SGE.Position>();
        _free = new HashSet<Position>();
    }

    public Position CreateComponent(int x, int y)
    {
        return new Position(new SGE.Position(x, y), this);
    }

    protected internal void Move(Position comp, Direction dir)
    {
        var destination = dir.Next(comp._position);
        if (_ground.Contains(destination))
        {
            _moving.Add(comp, destination);
            if (_components.TryGetValue(destination, out Position? value))
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

    public void Process()
    {
        foreach (var pair in _outOfBounds) { pair.Key.OnOutOfBounds?.Invoke(pair.Value); };
        _outOfBounds.Clear();

        var collision = new HashSet<Position>();
        while (_free.Count > 0)
        {
            var comp = _free.First();
            _free.Remove(comp);
            var dest = _moving[comp];
            if (_components.TryGetValue(dest, out Position? value))
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

        foreach (var pair in _moving)
        {
            var other = _components[pair.Value];
            pair.Key.OnCollision?.Invoke(other);
        };
        _moving.Clear();
    }

    private void DoMove(Position comp, SGE.Position dest)
    {
        _components.Remove(comp._position);
        _components.Add(dest, comp);
        comp.OnMove?.Invoke(comp._position, dest);
        comp._position = dest;
    }

    public class Position : Component
    {
        protected PositionSystem _system;
        protected internal SGE.Position _position;
        protected internal HashSet<Position> _dependency;
        public Action<SGE.Position, SGE.Position>? OnMove; // successful move
        public Action<Position>? OnCollision; // collision with another component
        public Action<SGE.Position>? OnOutOfBounds; // move to out of the ground

        protected internal Position(SGE.Position position, PositionSystem system)
        {
            _system = system;
            _position = position;
            _dependency = new HashSet<Position>();
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
