
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

    internal void AddEntity(Entity entity)
    {
        var node = _entities.AddLast(entity);
        _entitiesReferences.Add(entity, node);
    }

    internal void DestroyEntity(Entity entity)
    {
        if (_entities.Contains(entity))
            _entitiesToDestroy.AddLast(entity);
    }

    internal void AttachSystem(ISystem system)
    {
        _systems.AddLast(system);
    }

    public void Start()
    {
        foreach (var entity in _entities) { entity.FireStart(); }
    }

    public void Stop()
    {
        foreach (var entity in _entities) { entity.FireDestroy(); }
        _entities.Clear();
    }

    public void Loop()
    {
        foreach (var system in _systems) { system.Process(); }

        foreach (var destroyedComponent in _entitiesToDestroy)
        {
            var node = _entitiesReferences[destroyedComponent];
            destroyedComponent.FireDestroy();
            _entities.Remove(node);
        }
        _entitiesToDestroy.Clear();
    }
}
