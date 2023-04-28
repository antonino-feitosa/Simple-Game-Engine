
namespace SGE;

using static CameraSystem;

public class AnimationSystem : SubSystem
{
    protected HashSet<AnimationComponent> _components;

    public AnimationSystem()
    {
        _components = new HashSet<AnimationComponent>();
    }

    public void Process()
    {
        foreach (var comp in _components)
        {
            if (comp.Running)
            {
                if (comp._count >= comp.UpdatesByFrames)
                {
                    comp.RenderComponent.Image = comp.Sheet.GetImage(comp._current);
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

    public AnimationComponent CreateComponent(RenderComponent comp, SpriteSheet sheet, params int[] sequence)
    {
        var anim = new AnimationComponent(comp, sheet, sequence);
        _components.Add(anim);
        anim._entity.OnDestroy += () => _components.Remove(anim);
        return anim;
    }

    public class AnimationComponent : Component
    {
        public List<int> Sequence;
        public SpriteSheet Sheet;
        public RenderComponent RenderComponent;
        public bool Running;
        public int UpdatesByFrames;
        protected internal int _current;
        protected internal int _count;
        public AnimationComponent(RenderComponent comp, SpriteSheet sheet, params int[] sequence) : base(comp._entity)
        {
            Sheet = sheet;
            RenderComponent = comp;
            Running = true;
            Sequence = sequence.ToList<int>();
            _current = 0;
            _count = 0;
            UpdatesByFrames = 1;
        }
    }
}
