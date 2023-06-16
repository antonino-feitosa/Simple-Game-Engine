
namespace SGE;

public interface System
{
    public void Process();
}

public class Component
{
    private Entity? _entity;
    public Action<Entity>? OnStart;
    public Action<Entity>? OnDestroy;
    public Entity Entity
    {
        get => _entity ?? throw new ArgumentException("There is not an Entity for this Component!");
        set => _entity = value;
    }
}

public class Entity
{
    private readonly Game _game;
    protected HashSet<Component> _components;

    private static uint _countId = 0;
    private readonly uint _id;

    public Entity(Game game)
    {
        _id = _countId++;
        _game = game;
        _components = new HashSet<Component>();
    }
    protected uint ID { get { return _id; } }

    public T? GetComponent<T>() where T : Component
    {
        return _components.Where(c => c is T).First() as T;
    }

    public void Destroy() { _game.DestroyEntity(this); }

    protected internal void FireStart()
    {
        foreach (var component in _components) component.OnStart?.Invoke(this);
    }

    protected internal void FireDestroy()
    {
        foreach (var component in _components) component.OnDestroy?.Invoke(this);
    }

    public void AttachComponent(Component comp) { _components.Add(comp); }
    public void DetachComponent(Component comp) { _components.Remove(comp); }

    public override bool Equals(object? obj) { return obj is Entity e ? ID == e.ID : base.Equals(obj); }

    public override int GetHashCode() { return HashCode.Combine(ID); }

    public override string ToString() { return "Entity(" + ID + ")"; }
}
