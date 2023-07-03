
namespace SimpleGameEngine;

/// <summary>
/// This system performs a continuous moviment between two points of a <c>MoveableComponent</c><see cref="MoveableComponent"/>
/// </summary>
public class MotionSystem : SystemBase<MoveableComponent>
{
    public MotionSystem()
    {
        _components = new List<MoveableComponent>();
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
                    comp.OnMoveIncrement?.Invoke(comp._position);
                }
            }
            else
            {
                comp.OnIdle?.Invoke(comp._position);
            }
        }
    }
}
