
public class Direction
{
    public static Direction UP = new Direction(0, -1);
    public static Direction DOWN = new Direction(0, +1);
    public static Direction LEFT = new Direction(-1, 0);
    public static Direction RIGHT = new Direction(+1, 0);
    public static Direction UP_LEFT = new Direction(-1, -1);
    public static Direction UP_RIGHT = new Direction(+1, -1);
    public static Direction DOWN_LEFT = new Direction(-1, +1);
    public static Direction DOWN_RIGHT = new Direction(+1, +1);

    public readonly int X;
    public readonly int Y;

    private Direction(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class TilePosition
{
    public int X;
    public int Y;

    public TilePosition(int x = 0, int y = 0)
    {
        X = x;
        Y = y;
    }

    public TilePosition RelativeTo(Direction direction)
    {
        return new TilePosition(X + direction.X, Y + direction.Y);
    }

    public override bool Equals(object? obj)
    {
        return obj is TilePosition d && X == d.X && Y == d.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public override string ToString()
    {
        return "<" + X + "," + Y + ">";
    }
}

public class TilemapMotionComponent : Component
{
    public int UpdateTime;

    public Action<TilemapMotionComponent>? OnCollision;

    protected internal TilePosition Pos;

    protected internal HashSet<TilemapMotionComponent> _collision;

    private int _count;

    public TilemapMotionComponent(TilePosition position, int updateTime = 0)
    {
        _count = 0;
        Pos = position;
        UpdateTime = updateTime;
        _collision = new HashSet<TilemapMotionComponent>();
    }

    protected internal override void DoUpdate()
    {
        if (_count-- <= 0)
        {
            _count = UpdateTime;
            base.DoUpdate();
        }
    }

    protected internal override void DoDestroy()
    {
        var sys = GetSystem<TilemapMotionSystem>();
        sys?.Deregister(this);
        base.DoDestroy();
    }

    public void Move(Direction direction)
    {
        GetSystem<TilemapMotionSystem>()?.Move(this, direction);
    }
}

public class TilemapMotionSystem : SubSystem
{
    protected Dictionary<TilePosition, TilemapMotionComponent> _objects;
    protected Dictionary<TilemapMotionComponent, TilePosition> _moving;

    protected HashSet<TilemapMotionComponent> _free;
    protected HashSet<TilemapMotionComponent> _collision;

    public TilemapMotionSystem()
    {
        _free = new HashSet<TilemapMotionComponent>();
        _collision = new HashSet<TilemapMotionComponent>();
        _moving = new Dictionary<TilemapMotionComponent, TilePosition>();
        _objects = new Dictionary<TilePosition, TilemapMotionComponent>();
    }

    public override void Process()
    {
        while (_free.Count() > 0)
        {
            var node = _free.First();
            _free.Remove(node);
            var dest = _moving[node];
            if (_objects.ContainsKey(dest))
            {
                DoCollision(node, dest);
            }
            else
            {
                _objects.Remove(node.Pos);
                _objects.Add(dest, node);
                node.Pos = dest;
                foreach (var c in node._collision)
                {
                    dest = _moving[c];
                    if (!_objects.ContainsKey(dest))
                    {
                        _free.Add(c);
                    }
                }
                _collision.Remove(node);
                node._collision.Clear();
            }
        }
        foreach (var comp in _collision)
        {
            foreach (var c in comp._collision)
            {
                comp.OnCollision?.Invoke(c);
            }
        }
        _collision.Clear();
        _moving.Clear();
    }

    public bool CanMoveTo(TilemapMotionComponent comp, Direction direction)
    {
        var destination = comp.Pos.RelativeTo(direction);
        return !_objects.ContainsKey(destination);
    }

    private void DoCollision(TilemapMotionComponent comp, TilePosition destination)
    {
        var other = _objects[destination];
        other._collision.Add(comp);
        _collision.Add(comp);
        _collision.Add(other);
    }

    public void Move(TilemapMotionComponent comp, Direction direction)
    {

        var destination = comp.Pos.RelativeTo(direction);
        _moving.Add(comp, destination);
        if (_objects.ContainsKey(destination))
        {
            DoCollision(comp, destination);
        }
        else
        {
            _free.Add(comp);
        }
    }

    public override void Register(Component comp)
    {
        base.Register(comp);
        if (comp is TilemapMotionComponent t)
        {
            if (_objects.ContainsKey(t.Pos))
            {
                throw new ArgumentException("The position is taken: " + t.Pos);
            }
            _objects.Add(t.Pos, t);
        }
    }

    public override void Deregister(Component comp)
    {
        base.Deregister(comp);
        if (comp is TilemapMotionComponent t)
            _objects.Remove(t.Pos);
    }
}
