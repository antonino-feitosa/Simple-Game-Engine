
public class Entity {

    protected HashSet<Component> _components;

    public Entity(){
        _components = new HashSet<Component>();
    }

    public T? GetComponent<T>() where T: Component {
        return _components.Where<Component>(c => c is T).First<Component>() as T;
    }

    public void Start(){
        foreach(var comp in _components){
            comp.Start();
        }
    }

    public void Update(){
        foreach(var comp in _components){
            comp.Update();
        }
    }

    public void AttachComponent(Component comp){
        _components.Add(comp);
    }

    public void DetachComponent(Component comp){
        _components.Remove(comp);
    }
}
