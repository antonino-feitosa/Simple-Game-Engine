
namespace SGE;

public class Entity : Identifiable
{
    public Action? OnStart;
    public Action? OnUpdate;
    public Action? OnDestroy;

    private readonly Game _game;
    protected HashSet<Component> _components;

    public Entity(Game game)
    {
        _game = game;
        _components = new HashSet<Component>();
    }

    public T? GetComponent<T>() where T : Component
    {
        return _components.Where<Component>(c => c is T).First<Component>() as T;
    }

    public void Destroy()
    {
        _game.DestroyEntity(this);
    }

    protected internal void AttachComponent(Component comp)
    {
        _components.Add(comp);
    }

    public void DetachComponent(Component comp)
    {
        _components.Remove(comp);
    }

    public override string ToString()
    {
        return "Entity(" + base.ID + ")";
    }
}
