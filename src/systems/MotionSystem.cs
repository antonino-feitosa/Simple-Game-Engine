
namespace SimpleGameEngine;

public class MotionSystem : System
{
    protected List<Moveable> _components;

    public MotionSystem()
    {
        _components = new List<Moveable>();
    }

    public void Process()
    {
        foreach (var comp in _components)
        {
            if (comp._fired)
            {
                comp._fired = false;
                if (!comp._moving)
                {
                    comp._moving = true;
                    comp.OnStartMove?.Invoke(comp._position);
                }
            }
            if (comp._moving)
            {
                if (comp._position.IsCloseEnough(comp._destination))
                {
                    comp._moving = false;
                    comp.OnEndMove?.Invoke(comp._position);
                }
                else
                {
                    comp._position.Sum(comp._velocity);
                    comp.OnMoving?.Invoke(comp._position);
                }
            }
            else
            {
                comp.OnIdle?.Invoke(comp._position);
            }
        }
    }

    public Moveable CreateComponent(PositionSystem.Localizable comp, double framesToMove = 32)
    {
        var mc = new Moveable(comp, framesToMove);
        _components.Add(mc);
        mc.OnDestroy += (entity) => { _components.Remove(mc); };
        return mc;
    }

    public class Moveable : Component
    {
        protected internal bool _fired;
        protected internal bool _moving;
        protected internal Vector2 _position;
        protected internal Vector2 _destination;
        protected internal Vector2 _velocity;
        protected double _framesToMove;
        protected PositionSystem.Localizable _positionComponent;
        public Action<Vector2>? OnStartMove;
        public Action<Vector2>? OnEndMove;
        public Action<Vector2>? OnMoving;
        public Action<Vector2>? OnIdle;

        protected internal Moveable(PositionSystem.Localizable comp, double framesToMove)
        {
            _position = new Vector2();
            _destination = new Vector2();
            _velocity = new Vector2();
            _framesToMove = framesToMove;
            _positionComponent = comp;
            _positionComponent.OnMove += DoMove;
            _moving = false;
            _fired = false;
        }

        private void DoMove(Point source, Point dest)
        {
            _position = new Vector2(source.X, source.Y);
            _destination = new Vector2(dest.X, dest.Y);
            _velocity = new Vector2((dest.X - source.X) / _framesToMove, (dest.Y - source.Y) / _framesToMove);
            _fired = true;
        }

        public override string ToString()
        {
            return "MotionComponent:" + _positionComponent.ToString();
        }
    }
}
