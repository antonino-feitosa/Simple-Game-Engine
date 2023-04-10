
namespace SGE;

public class Component
{
    private static int _countId = 0;
    private readonly int _id;
    protected internal Entity? _entity;

    protected internal SubSystem? _system;

    
    public Action? OnStart;
    public Action? OnUpdate;
    public Action? OnDestroy;

    public Component (){
        _id = _countId++;
    }

    protected internal virtual void DoStart() {
        OnStart?.Invoke();
    }

    protected internal virtual void DoUpdate() {
        OnUpdate?.Invoke();
    }

    protected internal virtual void DoDestroy() {
        OnDestroy?.Invoke();
    }

    public T? GetSystem<T>() where T : SubSystem
    {
        return _system as T;
    }

    public override bool Equals(object? obj)
    {
        return obj is Component c ? _id == c._id : base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_id);
    }

    public override string ToString()
    {
        return "Component(" + GetType().Name + ", " + _id + ")";
    }
}
