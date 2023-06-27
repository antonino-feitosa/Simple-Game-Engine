
namespace SimpleGameEngine;

public class Component
{
    public Action<Entity> OnStart;
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
