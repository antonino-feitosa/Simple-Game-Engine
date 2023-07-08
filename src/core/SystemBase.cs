
namespace SimpleGameEngine;

public class SystemBase <TComponent> : ISystem where TComponent : Component {

    protected ICollection <TComponent> _components;

    protected IEnumerable<TComponent> Components { get => _components;}

    public SystemBase()
    {
        _components = new HashSet<TComponent>();
    }
    public virtual void Start(IDevice device){}
    public virtual void Process(){}
    internal virtual void AddComponent(TComponent component){
        if(component is TComponent derived){
            _components.Add(derived);
            component.OnDestroy += (entity) => _components.Remove(derived);
            component.OnEnable += (entity) => _components.Add(derived);
            component.OnDisable += (entity) => _components.Remove(derived);
        }
    }

    internal virtual void RemoveComponent(TComponent component){
        _components.Remove(component);
    }
}
