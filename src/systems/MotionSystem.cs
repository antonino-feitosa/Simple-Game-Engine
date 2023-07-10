
namespace SimpleGameEngine;

/// <summary>
/// This system performs a continuous moviment between two points of a <c>MoveableComponent</c><see cref="MovableComponent"/>
/// </summary>
public class MotionSystem : SystemBase<MovableComponent>
{
    public MotionSystem()
    {
        _components = new List<MovableComponent>();
    }

    public override void Process()
    {
        foreach (var comp in _components)
        {
            if (comp._fired)
            {
                comp._fired = false;
                if (!comp._moving)
                {
                    comp._moving = true;
                    comp.OnMoveStart?.Invoke(comp._position);
                }
            }
            if (comp._moving)
            {
                if (comp._position.IsCloseEnough(comp._destination))
                {
                    comp._moving = false;
                    comp.OnMoveEnd?.Invoke(comp._position);
                }
                else
                {
                    comp._position.Sum(comp._velocity);
                    comp.OnMoveIncrement?.Invoke(comp._position);
                }
            }
            else
            {
                comp.OnMoveIdle?.Invoke(comp._position);
            }
        }
    }
}
