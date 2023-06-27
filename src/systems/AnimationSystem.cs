
namespace SimpleGameEngine;

public class AnimationSystem : SystemBase<AnimatableComponent>
{
    public override void Process()
    {
        foreach (var comp in Components)
        {
            if (comp.Running)
            {
                if (comp._count >= comp.UpdatesByFrames)
                {
                    comp.RenderComponent.Image = comp.Sheet.GetSprite(comp._current);
                    comp._current = (comp._current + 1) % comp.Sequence.Count;
                    comp._count = 0;
                }
                else
                {
                    comp._count++;
                }
            }
        }
    }
}
