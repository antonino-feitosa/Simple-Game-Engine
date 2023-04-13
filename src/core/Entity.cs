
namespace SGE;

public class Entity
{
    public Action? OnStart;
    public Action? OnUpdate;
    public Action? OnDestroy;

    private static int _countId = 0;
    private readonly int _id;

    private Game _game;
    protected HashSet<Component> _components;

    public Entity(Game game)
    {
        _game = game;
        _id = _countId++;
        _components = new HashSet<Component>();
    }

    public T? GetComponent<T>() where T : Component
    {
        return _components.Where<Component>(c => c is T).First<Component>() as T;
    }

    public void Start()
    {
        OnStart?.Invoke();
    }

    public void Update()
    {
        OnUpdate?.Invoke();
    }

    public void Finish()
    {
        OnDestroy?.Invoke();
    }

    public void Destroy()
    {
        _game.DestroyEntity(this);
    }

    public void AttachComponent(Component comp)
    {
        comp._entity = this;
        _components.Add(comp);
    }

    public void DetachComponent(Component comp)
    {
        _components.Remove(comp);
        comp._entity = null;
    }

    public override bool Equals(object? obj)
    {
        return obj is Entity e ? _id == e._id : base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_id);
    }

    public override string ToString()
    {
        return "Entity(" + _id + ")";
    }
}
