
namespace SimpleGameEngine;

public class AnimatableComponent : Component
{
    public List<int> Sequence;
    public ISpriteSheet Sheet;
    public RenderableComponent RenderComponent;
    public bool Running;
    public int UpdatesByFrames;
    protected internal int _current;
    protected internal int _count;
    public AnimatableComponent(AnimationSystem system, RenderableComponent comp, ISpriteSheet sheet)
    {
        Sheet = sheet;
        RenderComponent = comp;
        Running = true;
        Sequence = Enumerable.Range(0, sheet.Length).ToList();
        _count = 0;
        UpdatesByFrames = 1;
        system.AddComponent(this);
    }
}
