
namespace SimpleGameEngine;

public class MotionSystem : ISystem
{
    protected List<MoveableComponent> _components;

    public MotionSystem()
    {
        _components = new List<MoveableComponent>();
    }

    public void Process()
    {
        foreach (var comp in _components)
        {
            if (comp._fired)
            {
                comp._fired = false;
                if (!comp._moving)
                {
                    comp._moving = true;
                    comp.OnStartMove?.Invoke(comp._position);
                }
            }
            if (comp._moving)
            {
                if (comp._position.IsCloseEnough(comp._destination))
                {
                    comp._moving = false;
                    comp.OnEndMove?.Invoke(comp._position);
                }
                else
                {
                    comp._position.Sum(comp._velocity);
                    comp.OnMoving?.Invoke(comp._position);
                }
            }
            else
            {
                comp.OnIdle?.Invoke(comp._position);
            }
        }
    }

    internal void AddComponent(MoveableComponent component)
    {
        _components.Add(component);
        component.OnDestroy += (entity) => { _components.Remove(component); };
    }
}
