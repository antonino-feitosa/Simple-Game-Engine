
using System.Diagnostics;

public class Game {

    protected int _fps = 32;

    protected InputSystem _input_system;
    protected RenderSystem _render_system;

    private bool _running;

    protected LinkedList <Entity> _entities;
    protected LinkedList <Entity> _entities_destroy;
    protected Dictionary<Entity, LinkedListNode<Entity>> _entities_reference;

    public Game(){
        _input_system = new InputSystem();
        _render_system = new RenderSystem();
        _entities = new LinkedList<Entity>();
        _entities_destroy = new LinkedList<Entity>();
        _entities_reference = new Dictionary<Entity, LinkedListNode<Entity>>();
    }

    public Entity CreateEntity(){
        Entity e = new Entity();
        var node = _entities.AddLast(e);
        _entities_reference.Add(e, node);
        return e;
    }

    public void DestroyEntity(Entity e){
        _entities_destroy.AddLast(e);
    }

    public void Start (){
        _running = true;
        _input_system.Start();
        _render_system.Start();
    }

    public void Stop(){
        _running = false;
        _input_system.Finish();
        _render_system.Finish();
    }

    public async void Run(){
        long end;
        long start = Stopwatch.GetTimestamp();
        while(_running){
            _input_system.Process();

            foreach(var ent in _entities){
                ent.Update();
            }
            foreach(var del in _entities_destroy){
                var node = _entities_reference[del];
                _entities.Remove(node);
            }
            _entities_destroy.Clear();

            _render_system.Process();
            
            end = Stopwatch.GetTimestamp();
            start = end;
            int wait = (int)((end - start) - _fps * 10); // 10,000 Ticks form a millisecond.
            if(wait > 0){ 
                await Task.Delay(wait); 
            }
        }
    }
}
