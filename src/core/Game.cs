
namespace SimpleGameEngine;

public class Game
{
    protected LinkedList<ISystem> _systems;
    protected LinkedList<Entity> _entities;
    protected LinkedList<Entity> _entitiesToDestroy;
    protected Dictionary<Entity, LinkedListNode<Entity>> _entitiesReferences;

    public Game()
    {
        _systems = new LinkedList<ISystem>();
        _entities = new LinkedList<Entity>();
        _entitiesToDestroy = new LinkedList<Entity>();
        _entitiesReferences = new Dictionary<Entity, LinkedListNode<Entity>>();
    }

    internal void AddEntity(Entity e)
    {
        var node = _entities.AddLast(e);
        _entitiesReferences.Add(e, node);
    }

    internal void DestroyEntity(Entity e)
    {
        _entitiesToDestroy.AddLast(e);
    }

    internal void AttachSystem(ISystem system)
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

        foreach (var del in _entitiesToDestroy)
        {
            var node = _entitiesReferences[del];
            del.FireDestroy();
            _entities.Remove(node);
        }
        _entitiesToDestroy.Clear();
    }
}
