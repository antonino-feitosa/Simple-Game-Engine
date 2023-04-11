
namespace SGE;

class SortSystem : IComparer<SubSystem>
{
    public int Compare(SubSystem? x, SubSystem? y)
    {
        return x != null && y != null ? x._priority - y._priority : -1;
    }
}

public class Game
{
    public Platform Device;
    protected SortedSet<SubSystem> _systems;
    protected LinkedList<Entity> _entities;
    protected LinkedList<Entity> _entities_destroy;
    protected Dictionary<Entity, LinkedListNode<Entity>> _entities_reference;

    public Game(Platform device, int fps = 32)
    {
        Device = device;
        Device.RegisterLoop(Loop, fps);
        _systems = new SortedSet<SubSystem>(new SortSystem());
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
        _systems.Add(system);
    }

    public void Start()
    {
        Device.Start();
        foreach (var sys in _systems) { sys.Start(); }
        foreach (var ent in _entities) { ent.Start(); }
    }

    public void Stop()
    {
        foreach (var sys in _systems) { sys.Finish(); }
        Device.Finish();
    }

    public void Loop()
    {
        foreach (var sys in _systems) { sys.Process(); }

        foreach (var ent in _entities) { ent.Update(); }

        foreach (var del in _entities_destroy)
        {
            var node = _entities_reference[del];
            del.Finish();
            _entities.Remove(node);
        }
        _entities_destroy.Clear();
    }
}
