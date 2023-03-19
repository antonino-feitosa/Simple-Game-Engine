
public class Component
{
    protected internal Entity? _entity;

    protected internal HashSet <SubSystem> _systems;

    public Component(){
        _systems = new HashSet<SubSystem>();
    }

    public virtual void OnStart() { }

    public virtual void OnUpdate() { }

    public virtual void OnDestroy() { }

    protected internal void DoDestroy(){
        OnDestroy();
        foreach(var sys in _systems){
            sys.Deregister(this);
        }
    }
}
