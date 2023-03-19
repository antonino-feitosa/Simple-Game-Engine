
using System.Diagnostics;

public class Game
{

    protected int _fps = 32;

    protected ISubSystem _render_system;
    protected LinkedList<ISubSystem> _systems;

    private bool _running;

    protected LinkedList<Entity> _entities;
    protected LinkedList<Entity> _entities_destroy;
    protected Dictionary<Entity, LinkedListNode<Entity>> _entities_reference;

    public Game(ISubSystem render_system, int fps = 32)
    {
        _fps = fps;
        _render_system = render_system;
        _systems = new LinkedList<ISubSystem>();
        _entities = new LinkedList<Entity>();
        _entities_destroy = new LinkedList<Entity>();
        _entities_reference = new Dictionary<Entity, LinkedListNode<Entity>>();
    }

    public Entity CreateEntity()
    {
        Entity e = new Entity();
        var node = _entities.AddLast(e);
        _entities_reference.Add(e, node);
        return e;
    }

    public void DestroyEntity(Entity e)
    {
        _entities_destroy.AddLast(e);
    }

    public void AttachSystem(ISubSystem system)
    {
        _systems.AddLast(system);
    }

    public void Start()
    {
        _running = true;
        _render_system.Start();
        foreach (var sys in _systems)
        {
            sys.Start();
        }
    }

    public void Stop()
    {
        _running = false;
        _render_system.Finish();
        foreach (var sys in _systems)
        {
            sys.Finish();
        }
    }

    public async void Run()
    {
        long end;
        long start = Stopwatch.GetTimestamp();
        while (_running)
        {
            foreach (var sys in _systems)
            {
                sys.Process();
            }

            foreach (var ent in _entities)
            {
                ent.Update();
            }
            foreach (var del in _entities_destroy)
            {
                var node = _entities_reference[del];
                _entities.Remove(node);
            }
            _entities_destroy.Clear();

            _render_system.Process();

            end = Stopwatch.GetTimestamp();
            start = end;
            int wait = (int)((end - start) - _fps * 10); // 10,000 Ticks form a millisecond.
            if (wait > 0)
            {
                await Task.Delay(wait);
            }
        }
    }
}
