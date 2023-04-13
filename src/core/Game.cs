
namespace SGE;

public class Game
{
    public Platform Device;
    protected LinkedList<SubSystem> _systems;
    protected LinkedList<Entity> _entities;
    protected LinkedList<Entity> _entities_destroy;
    protected Dictionary<Entity, LinkedListNode<Entity>> _entities_reference;

    public Game(Platform device, int fps = 32)
    {
        Device = device;
        Device.RegisterLoop(Loop, fps);
        _systems = new LinkedList<SubSystem>();
        _entities = new LinkedList<Entity>();
        _entities_destroy = new LinkedList<Entity>();
        _entities_reference = new Dictionary<Entity, LinkedListNode<Entity>>();
    }

    public Entity CreateEntity(params Component [] components)
    {
        Entity e = new Entity(this);
        var node = _entities.AddLast(e);
        _entities_reference.Add(e, node);
        foreach(var comp in components){
            e.AttachComponent(comp);
        }
        return e;
    }

    public void DestroyEntity(Entity e)
    {
        _entities_destroy.AddLast(e);
    }

    protected internal void AttachSystem(SubSystem system)
    {
        system._game = this;
        _systems.AddLast(system);
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
