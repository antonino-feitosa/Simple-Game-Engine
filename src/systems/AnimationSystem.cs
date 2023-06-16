
namespace SimpleGameEngine;

public class AnimationSystem : System
{
    protected HashSet<Animatable> _components;

    public AnimationSystem()
    {
        _components = new HashSet<Animatable>();
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

    public Animatable CreateComponent(CameraSystem.Renderable comp, SpriteSheet sheet, params int[] sequence)
    {
        var anim = new Animatable(comp, sheet, sequence);
        _components.Add(anim);
        anim.OnDestroy += (entity) => _components.Remove(anim);
        return anim;
    }

    public class Animatable : Component
    {
        public List<int> Sequence;
        public SpriteSheet Sheet;
        public CameraSystem.Renderable RenderComponent;
        public bool Running;
        public int UpdatesByFrames;
        protected internal int _current;
        protected internal int _count;
        public Animatable(CameraSystem.Renderable comp, SpriteSheet sheet, params int[] sequence)
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
