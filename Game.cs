
using System.Diagnostics;

public class Game
{

    protected int _frame_rate_ticks = 1;

    protected ISubSystem _render_system;
    protected LinkedList<ISubSystem> _systems;

    private bool _running;

    protected LinkedList<Entity> _entities;
    protected LinkedList<Entity> _entities_destroy;
    protected Dictionary<Entity, LinkedListNode<Entity>> _entities_reference;

    public Game(ISubSystem render_system, int fps = 1)
    {
        _frame_rate_ticks = 1000 / fps; // 1000 milliseconds
        _render_system = render_system;
        _systems = new LinkedList<ISubSystem>();
        _entities = new LinkedList<Entity>();
        _entities_destroy = new LinkedList<Entity>();
        _entities_reference = new Dictionary<Entity, LinkedListNode<Entity>>();
    }

    public Entity RegisterEntity(Entity e)
    {
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

    public void Run()
    {   
        while (_running)
        {
            DateTime startTime = DateTime.Now;
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

            DateTime endTime = DateTime.Now;
            TimeSpan elapsedTime = endTime - startTime;
            int wait = _frame_rate_ticks - (int)(elapsedTime.TotalMilliseconds);
            if (wait > 0) Thread.Sleep(wait);
            startTime = DateTime.Now;
        }
    }
}
