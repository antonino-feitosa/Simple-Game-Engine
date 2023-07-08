
namespace SimpleGameEngine;

/// <summary>
/// This system performs the animation of the <c>AnimatableComponent</c>.<see cref="AnimatableComponent"/>
/// </summary>
public class AnimationSystem : SystemBase<AnimatableComponent>
{
    public override void Process()
    {
        foreach (var comp in Components.Where(comp => comp.Running))
        {
            if (comp._count >= comp.UpdatesBetweenFrames)
            {
                comp._count = 0;
                comp._current = (comp._current + 1) % comp.Sequence.Count;
                comp.RenderComponent.Image = comp.Sheet.GetSprite(comp.Sequence[comp._current]);
            }
            else
            {
                comp._count++;
            }
        }
    }
}
