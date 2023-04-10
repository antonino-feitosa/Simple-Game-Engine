
namespace SGE;

public class Entity {

    private Game _game;
    protected HashSet<Component> _components;

    public Entity(Game game, params Component [] components){
        _game = game;
        _components = new HashSet<Component>();
        foreach(var c in components)
            AttachComponent(c);
    }

    public T? GetComponent<T>() where T: Component {
        return _components.Where<Component>(c => c is T).First<Component>() as T;
    }

    public void Start(){
        foreach(var comp in _components){
            comp._entity = this;
            comp.DoStart();
        }
    }

    public void Update(){
        foreach(var comp in _components){
            comp.DoUpdate();
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
