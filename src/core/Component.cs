
public class Component
{
    protected internal Entity? _entity;

    protected internal SubSystem? _system;

    protected internal virtual void DoStart() {
        OnStart();
    }

    protected internal virtual void DoUpdate() {
        OnUpdate();
    }

    protected internal virtual void DoDestroy() {
        OnDestroy();
    }

    protected virtual void OnStart() { }

    protected virtual void OnUpdate() { }

    protected virtual void OnDestroy() { }

    public T? GetSystem<T>() where T : SubSystem
    {
        return _system as T;
    }
}
