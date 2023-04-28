

namespace SGE;

public class PositionSystem : SubSystem
{
    public readonly Direction UP = new Direction(0, -1);
    public readonly Direction UP_LEFT = new Direction(-1, -1);
    public readonly Direction UP_RIGHT = new Direction(+1, -1);
    public readonly Direction DOWN = new Direction(0, +1);
    public readonly Direction DOWN_LEFT = new Direction(-1, +1);
    public readonly Direction DOWN_RIGHT = new Direction(+1, +1);
    public readonly Direction LEFT = new Direction(-1, 0);
    public readonly Direction RIGHT = new Direction(+1, -0);

    protected HashSet<Position> _ground;
    protected Dictionary<Position, PositionComponent> _components;
    protected Dictionary<PositionComponent, Position> _moving;
    protected Dictionary<PositionComponent, Position> _outOfBounds;
    protected HashSet<PositionComponent> _free;

    public PositionSystem(HashSet<Position> ground)
    {
        _ground = ground;
        _components = new Dictionary<Position, PositionComponent>();
        _moving = new Dictionary<PositionComponent, Position>();
        _outOfBounds = new Dictionary<PositionComponent, Position>();
        _free = new HashSet<PositionComponent>();
    }

    public PositionComponent CreateComponent(Entity entity, int x, int y)
    {
        return new PositionComponent(entity, new Position(x, y), this);
    }

    protected internal void Move(PositionComponent comp, Direction dir)
    {
        var destination = dir.Next(comp._position);
        if (_ground.Contains(destination))
        {
            _moving.Add(comp, destination);
            if (_components.ContainsKey(destination))
            {
                var other = _components[destination];
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

        var collision = new HashSet<PositionComponent>();
        while (_free.Count > 0)
        {
            var comp = _free.First();
            _free.Remove(comp);
            var dest = _moving[comp];
            if (_components.ContainsKey(dest))
            {
                var other = _components[dest];
                other._dependency.Add(comp);
            }
            else
            {
                DoMove(comp, dest);
                _moving.Remove(comp);
                if(comp._dependency.Count > 0){
                    var next = comp._dependency.First();
                    _free.Add(next);
                    comp._dependency.Remove(next);
                    foreach (var d in comp._dependency) { d.OnCollision?.Invoke(next); }
                    comp._dependency.Clear();
                }
            }
        }

        foreach (var pair in _moving) {
            var other = _components[pair.Value];
            pair.Key.OnCollision?.Invoke(other);
        };
        _moving.Clear();
    }

    private void DoMove(PositionComponent comp, Position dest)
    {
        _components.Remove(comp._position);
        _components.Add(dest, comp);
        comp.OnMove?.Invoke(comp._position, dest);
        comp._position = dest;
    }

    public class PositionComponent : Component
    {
        protected PositionSystem _system;
        protected internal Position _position;
        protected internal HashSet<PositionComponent> _dependency;
        public Action<Position, Position>? OnMove; // successful move
        public Action<PositionComponent>? OnCollision; // collision with another component
        public Action<Position>? OnOutOfBounds; // move to out of the ground

        protected internal PositionComponent(Entity entity, Position position, PositionSystem system) : base(entity)
        {
            _system = system;
            _position = position;
            _dependency = new HashSet<PositionComponent>();
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
