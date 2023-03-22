
public class Component
{
    protected internal Entity? _entity;

    protected internal SubSystem? _system;

    public virtual void OnStart() { }

    public virtual void OnUpdate() { }

    public virtual void OnDestroy() { }

    public T? GetSystem<T>() where T : SubSystem
    {
        return _system as T;
    }
}
