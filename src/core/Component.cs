
public class Component
{
    protected internal Entity? _entity;

    protected internal SubSystem? _system;

    
    public Action? OnStart;
    public Action? OnUpdate;
    public Action? OnDestroy;

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
}
