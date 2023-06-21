
namespace SimpleGameEngine;

public class AnimationSystem : ISystem
{
    protected HashSet<AnimatableComponent> _components;

    public AnimationSystem()
    {
        _components = new HashSet<AnimatableComponent>();
    }

    public void Process()
    {
        foreach (var comp in _components)
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

    internal void AddComponent(AnimatableComponent component){
        _components.Add(component);
        component.OnDestroy += (entity) => _components.Remove(component);
    }
}
