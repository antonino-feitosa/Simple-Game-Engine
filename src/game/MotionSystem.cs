
namespace SGE;

public class MotionSystem : SubSystem
{
    private const double CLOSE_ENOUGH = 0.01;
    protected List<MotionComponent> _components;

    public MotionSystem()
    {
        _components = new List<MotionComponent>();
    }

    public void Start() { }
    public void Finish() { }
    public void Process()
    {
        foreach (var comp in _components)
        {
            if(comp._fired){
                comp._fired = false;
                if(!comp._moving){
                    comp._moving = true;
                    comp.OnStartMove?.Invoke(comp._position);
                }
            }
            if(comp._moving){
                if(comp._position.IsCloseEnough(comp._destination)){
                    comp._moving = false;
                    comp.OnEndMove?.Invoke(comp._position);
                } else {
                    comp._position.Sum(comp._velocity);
                    comp.OnMoving?.Invoke(comp._position);
                }
            } else {
                comp.OnIdle?.Invoke(comp._position);
            }
        }
    }

    public MotionComponent CreateComponent(PositionSystem.PositionComponent comp, double framesToMove)
    {
        var mc = new MotionComponent(comp, framesToMove);
        _components.Add(mc);
        mc._entity.OnDestroy += () => { _components.Remove(mc); };
        return mc;
    }

    public class Vector2
    {
        public double X;
        public double Y;

        public Vector2(double x = 0, double y = 0)
        {
            Set(x, y);
        }
        public void Set(double x, double y){
            X = x;
            Y = y;
        }
        public void Sum(Vector2 vet)
        {
            X += vet.X;
            Y += vet.Y;
        }
        protected internal bool IsCloseEnough(Vector2 vet)
        {
            return Math.Abs(X - vet.X) + Math.Abs(Y - vet.Y) <= CLOSE_ENOUGH;
        }
        protected internal bool IsZero(){
            return X + Y == 0;
        }
        public override bool Equals(object? obj)
        {
            return obj is Vector2 vet ? vet.X == X && vet.Y == Y : base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
        public override string ToString()
        {
            return String.Format("Vector2({0}, {1})", X, Y);
        }
    }

    public class MotionComponent : Component
    {
        protected internal bool _fired;
        protected internal bool _moving;
        protected internal Vector2 _position;
        protected internal Vector2 _destination;
        protected internal Vector2 _velocity;
        protected double _framesToMove;
        protected PositionSystem.PositionComponent _positionComponent;
        public Action<Vector2>? OnStartMove; // successful move
        public Action<Vector2>? OnEndMove; // successful move
        public Action<Vector2>? OnMoving; // successful move
        public Action<Vector2>? OnIdle; // successful move

        protected internal MotionComponent(PositionSystem.PositionComponent comp, double framesToMove) : base(comp._entity)
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

        private void DoMove(PositionSystem.Position source, PositionSystem.Position dest)
        {
            _position = new Vector2(source.X, source.Y);
            _destination = new Vector2(dest.X, dest.Y);
            _velocity = new Vector2();
            _velocity.X = (dest.X - source.X) / (double)_framesToMove;
            _velocity.Y = (dest.Y - source.Y) / (double)_framesToMove;
            _fired = true;
        }

        public override string ToString()
        {
            return "MotionComponent:" + _positionComponent.ToString();
        }
    }
}
