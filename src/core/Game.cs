
public class Game
{

    protected int _frame_rate_ticks = 1;

    protected SubSystem _render_system;
    protected LinkedList<SubSystem> _systems;

    private bool _running;

    protected LinkedList<Entity> _entities;
    protected LinkedList<Entity> _entities_destroy;
    protected Dictionary<Entity, LinkedListNode<Entity>> _entities_reference;

    public Game(SubSystem render_system, int fps = 1)
    {
        _frame_rate_ticks = 1000 / fps; // 1000 milliseconds
        _render_system = render_system;
        _systems = new LinkedList<SubSystem>();
        _entities = new LinkedList<Entity>();
        _entities_destroy = new LinkedList<Entity>();
        _entities_reference = new Dictionary<Entity, LinkedListNode<Entity>>();
    }

    public Entity CreateEntity(params Component[] components)
    {
        Entity e = new Entity(this);
        var node = _entities.AddLast(e);
        _entities_reference.Add(e, node);
        foreach (var c in components)
            e.AttachComponent(c);
        return e;
    }

    public void DestroyEntity(Entity e)
    {
        _entities_destroy.AddLast(e);
    }

    public void AttachSystem(SubSystem system)
    {
        system.SetGame(this);
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
                del.Finish();
                _entities.Remove(node);
            }
            _entities_destroy.Clear();

            _render_system.Process();

            DateTime endTime = DateTime.Now;
            TimeSpan elapsedTime = endTime - startTime;
            int wait = _frame_rate_ticks - (int)(elapsedTime.TotalMilliseconds);
            if (wait > 0) Thread.Sleep(wait);
            startTime = DateTime.Now;
            Console.WriteLine("Next Step");
        }
    }
}
