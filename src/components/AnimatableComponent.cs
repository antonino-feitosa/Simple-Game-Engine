
namespace SimpleGameEngine;

public class AnimatableComponent : Component
{
    public List<int> _sequence;
    public ISpriteSheet Sheet;
    public RenderableComponent RenderComponent;
    public bool Running;
    public int UpdatesBetweenFrames;

    protected internal int _current;
    protected internal int _count;

    public List<int> Sequence
    {
        get => _sequence;
        set
        {
            if (value.Count == 0) throw new ArgumentException("The list can not be empty!");
            if (value.Any(index => index < 0)) throw new ArgumentException("The list can not have a negative index!");
            if (value.Any(index => index >= Sheet.Length)) throw new ArgumentException("The list can not have an index out of the sheet!");
            _sequence = value;
            Reset();
        }
    }

    public AnimatableComponent(AnimationSystem system, RenderableComponent comp, ISpriteSheet sheet)
    {
        Sheet = sheet;
        RenderComponent = comp;
        Running = true;
        _sequence = Enumerable.Range(0, sheet.Length).ToList();
        _count = 0;
        UpdatesBetweenFrames = 1;
        system.AddComponent(this);
    }

    public void Reset()
    {
        _count = 0;
        _current = 0;
        RenderComponent.Image = Sheet.GetSprite(Sequence[0]);
    }
}
