
public class Vector2
{

    public double X;
    public double Y;

    public Vector2(double x = 0, double y = 0)
    {
        X = x;
        Y = y;
    }

    public Vector2(Vector2 vet)
    {
        X = vet.X;
        Y = vet.Y;
    }

    public void Copy(Vector2 vet)
    {
        X = vet.X;
        Y = vet.Y;
    }

    public void Addition(Vector2 vet)
    {
        X += vet.X;
        Y += vet.Y;
    }

    public void Subtract(Vector2 vet)
    {
        X -= vet.X;
        Y -= vet.Y;
    }

    public double Distance(Vector2 vet)
    {
        return Math.Sqrt(Math.Pow(X - vet.X, 2) + Math.Pow(Y - vet.Y, 2));
    }
}

public class Collision2Component : Component
{
    public Action? OnEndMoving;
    public Action<Collision2Component>? OnCollision;

    protected internal List<Collision2Component> _collision;

    protected internal double _radius;
    protected internal Vector2 _position;
    protected internal Vector2 _velocity;
    protected internal Vector2 _destination;

    public Collision2Component(Vector2 position, double radius)
    {
        _radius = radius;
        _position = new Vector2(position);
        _destination = _position;
        _velocity = new Vector2();
        _collision = new List<Collision2Component>();
    }

    public void MoveTo(Vector2 destination)
    {
        _velocity.Copy(_position);
        _destination.Copy(destination);
        _velocity.Subtract(destination);
        GetSystem<Collision2System>()?.StartMoving(this);
    }

    public void Stop()
    {
        _velocity.X = 0;
        _velocity.Y = 0;
        _destination.Copy(_position);
    }

    protected internal virtual void DoCollision()
    {
        if (OnCollision != null)
        {
            foreach (var comp in _collision)
            {
                OnCollision.Invoke(comp);
            }
        }
    }

    protected internal virtual void DoEndMove()
    {
        OnEndMoving?.Invoke();
    }
}

public class Collision2System : SubSystem
{

    private const double EPSILON = 0.01;
    protected HashSet<Collision2Component> _moving;

    public Collision2System()
    {
        _moving = new HashSet<Collision2Component>();
    }

    public void StartMoving(Collision2Component comp)
    {
        _moving.Add(comp);
    }

    public override void Process()
    {
        var components = GetComponents<Collision2Component>();
        foreach (var comp in _moving)
        {
            Vector2 old = new Vector2(comp._position);
            bool moved = true;
            comp._position.Addition(comp._velocity);
            foreach (var other in components)
            {
                if (InCollision(comp, other))
                {
                    moved = false;
                    comp._collision.Add(other);
                }
            }
            if (moved)
            {
                if (comp._position.Distance(comp._destination) < EPSILON)
                {
                    comp.Stop();
                    comp.DoEndMove();
                    _moving.Remove(comp);
                }
            }
            else
            {
                comp.Stop();
                comp.DoEndMove();
                comp.DoCollision();
                _moving.Remove(comp);
            }
        }
    }

    private bool InCollision(Collision2Component a, Collision2Component b)
    {
        return a._position.Distance(b._position) < a._radius + b._radius;
    }

    public override void Deregister(Component comp)
    {
        base.Deregister(comp);
        if (comp is Collision2Component c && _moving.Contains(c))
        {
            _moving.Remove(c);
        }
    }
}
