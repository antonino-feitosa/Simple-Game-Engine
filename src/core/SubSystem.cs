
public class SubSystem
{

    protected Game? _game;
    protected HashSet<Component> _components;

    public SubSystem()
    {
        _components = new HashSet<Component>();
    }

    public HashSet<T> GetComponents<T>() where T : Component {
        return _components.Cast<T>().ToHashSet();
    }

    public virtual void Register(Component comp)
    {
        _components.Add(comp);
    }

    public virtual void Deregister(Component comp)
    {
        _components.Remove(comp);
    }

    public virtual void SetGame(Game game)
    {
        _game = game;
    }

    public virtual void Start() { }

    public virtual void Process() { }

    public virtual void Finish() { }
}
