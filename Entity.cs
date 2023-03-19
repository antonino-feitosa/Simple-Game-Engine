
public class Entity {

    private Game _game;
    protected HashSet<Component> _components;

    public Entity(Game game){
        _game = game;
        _components = new HashSet<Component>();
    }

    public T? GetComponent<T>() where T: Component {
        return _components.Where<Component>(c => c is T).First<Component>() as T;
    }

    public void Start(){
        foreach(var comp in _components){
            comp._entity = this;
            comp.OnStart();
        }
    }

    public void Update(){
        foreach(var comp in _components){
            comp.OnUpdate();
        }
    }

    public void Finish(){
        foreach(var comp in _components){
            comp.DoDestroy();
        }
    }

    public void AttachComponent(Component comp){
        _components.Add(comp);
    }

    public void DetachComponent(Component comp){
        _components.Remove(comp);
    }

    public void Destroy(){
        _game.DestroyEntity(this);
    }
}
