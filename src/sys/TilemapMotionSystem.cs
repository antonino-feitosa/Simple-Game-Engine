
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
        return new TilePosition(X + direction.X, Y = direction.Y);
    }

    public override bool Equals(object? obj)
    {
        return obj is TilePosition d && X == d.X && Y == d.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}

public class TilemapMotionComponent : Component
{
    public int UpdateTime;

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
        foreach (var comp in _collision)
        {
            OnCollision(comp);
        }
        _collision.Clear();
    }

    public void Move(Direction direction)
    {
        GetSystem<TilemapMotionSystem>()?.Move(this, direction);
    }

    public virtual void OnCollision(TilemapMotionComponent other) { }
}

public class TilemapMotionSystem : SubSystem
{
    protected Dictionary<TilePosition, TilemapMotionComponent> _objects;

    public TilemapMotionSystem()
    {
        _objects = new Dictionary<TilePosition, TilemapMotionComponent>();
    }

    public bool CanMoveTo(TilemapMotionComponent comp, Direction direction)
    {
        var destination = comp.Pos.RelativeTo(direction);
        return !_objects.ContainsKey(destination);
    }

    public void Move(TilemapMotionComponent comp, Direction direction)
    {
        var destination = comp.Pos.RelativeTo(direction);
        if (_objects.ContainsKey(destination))
        {
            var other = _objects.GetValueOrDefault(destination);
            if (other != null)
            {
                comp._collision.Add(other);
                other._collision.Add(comp);
            }
            else throw new ArgumentException("There is not object associated with the position " + destination);
        }
        else
        {
            _objects.Remove(destination);
            _objects.Add(destination, comp);
        }
    }

    public override void Register(Component comp)
    {
        base.Register(comp);
        if (comp is TilemapMotionComponent t)
            _objects.Add(t.Pos, t);
    }
}
