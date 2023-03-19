public interface ISubSystem
{
    void Finish();
    void Process();
    void Start();
}

public class SubSystem<T> : ISubSystem where T : Component
{

    protected HashSet<T> _components;

    public SubSystem()
    {
        _components = new HashSet<T>();
    }

    public virtual void Register(T comp)
    {
        _components.Add(comp);
    }

    public virtual void Deregister(T comp)
    {
        _components.Remove(comp);
    }

    public virtual void Start()
    {

    }

    public virtual void Process()
    {

    }

    public virtual void Finish()
    {

    }
}
