
namespace SimpleGameEngine;

public class Component
{
    public readonly Action<Entity> OnStart;
    public Action<Entity> OnDestroy;
    public Action<Entity> OnEnable;
    public Action<Entity> OnDisable;
    private Entity? _entity;
    public Entity Entity
    {
        get => _entity ?? throw new ArgumentException("There is not an Entity for this Component!");
        set => _entity = value;
    }

    public Component()
    {
        OnStart = (Entity) => { };
        OnDestroy = (Entity) => { };
        OnEnable = (Entity) => { };
        OnDisable = (Entity) => { };
    }
}

public class Entity
{
    private readonly Game _game;
    protected HashSet<Component> _components;

    private static uint _countId = 0;
    private readonly uint _id;
    private bool _enabled;

    protected uint ID { get { return _id; } }

    public bool Enabled
    {
        get => _enabled;
        set
        {
            foreach (var comp in _components)
            {
                if (value)
                    comp.OnEnable(this);
                else
                    comp.OnDisable(this);
            }
            _enabled = value;
        }
    }
    public Entity(Game game)
    {
        _id = _countId++;
        _game = game;
        _enabled = true;
        _components = new HashSet<Component>();
        game.AddEntity(this);
    }
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
