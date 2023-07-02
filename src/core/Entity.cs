
namespace SimpleGameEngine;

public class Entity
{
    protected HashSet<Component> _components;

    private readonly Game _game;

    private static uint _countId = 0;
    private readonly uint _id;
    private bool _enabled;

    protected uint ID { get { return _id; } }

    public bool Enabled
    {
        get => _enabled;
        set
        {
            if(value != _enabled){
                foreach (var comp in _components)
                {
                    if (value)
                        comp.OnEnable?.Invoke(this);
                    else
                        comp.OnDisable?.Invoke(this);
                }
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
    public T GetComponent<T>() where T : Component
    {  
        var selected = _components.Where(c => c is T);
        if(selected.Any() && selected.First() is T component){
            return component;
        }
        throw new ArgumentException("There is no component of type " + typeof(T).Name);
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
