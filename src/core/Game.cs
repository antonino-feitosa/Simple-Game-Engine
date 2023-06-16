
namespace SimpleGameEngine;

public class Game
{
    protected LinkedList<System> _systems;
    protected LinkedList<Entity> _entities;
    protected LinkedList<Entity> _entities_destroy;
    protected Dictionary<Entity, LinkedListNode<Entity>> _entities_reference;

    public Game()
    {
        _systems = new LinkedList<System>();
        _entities = new LinkedList<Entity>();
        _entities_destroy = new LinkedList<Entity>();
        _entities_reference = new Dictionary<Entity, LinkedListNode<Entity>>();
    }

    public Entity MakeEntity()
    {
        var e = new Entity(this);
        var node = _entities.AddLast(e);
        _entities_reference.Add(e, node);
        return e;
    }

    public void DestroyEntity(Entity e)
    {
        _entities_destroy.AddLast(e);
    }

    protected internal void AttachSystem(System system)
    {
        _systems.AddLast(system);
    }

    public void Start()
    {
        foreach (var ent in _entities) { ent.FireStart(); }
    }

    public void Stop()
    {
        foreach (var ent in _entities) { ent.FireDestroy(); }
        _entities.Clear();
    }

    public void Loop()
    {
        foreach (var sys in _systems) { sys.Process(); }

        foreach (var del in _entities_destroy)
        {
            var node = _entities_reference[del];
            del.FireDestroy();
            _entities.Remove(node);
        }
        _entities_destroy.Clear();
    }
}
