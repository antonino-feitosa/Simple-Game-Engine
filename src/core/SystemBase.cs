
namespace SimpleGameEngine;

public class SystemBase <TComponent> : ISystem where TComponent : Component {

    protected ICollection <TComponent> _components;

    protected IEnumerable<TComponent> Components { get => _components;}

    public SystemBase()
    {
        _components = new HashSet<TComponent>();
    }
    public virtual void Process(){}
    internal void AddComponent(TComponent component){
        _components.Add(component);
        component.OnDestroy += (entity) => _components.Remove(component);
        component.OnEnable += (entity) => _components.Add(component);
        component.OnDisable += (entity) => _components.Remove(component);
    }
}
