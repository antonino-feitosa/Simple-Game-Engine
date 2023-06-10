
namespace SGE;

public class CameraSystem : System
{
    public Vector2 Position; // TODO double position
    public Dimension Dimension;
    public int PixelsUnit;

    protected List<Render> _components;

    public CameraSystem(Vector2 position, Dimension dimension, int pixelsUnit = 32)
    {
        Position = position;
        Dimension = dimension;
        PixelsUnit = pixelsUnit;
        _components = new List<Render>(); // TODO optimization on delete
    }

    public void Process()
    {
        _components.Sort((a, b) => a.ZIndex - b.ZIndex);
        int cx = (int)(Position.X * PixelsUnit);
        int cy = (int)(Position.Y * PixelsUnit);
        int cWidth = cx + Dimension.Width;
        int cHeight = cy + Dimension.Height;
        foreach (var comp in _components)
        { // TODO correct truncation location
            int x = (int)(comp.Position.X * PixelsUnit);
            int y = (int)(comp.Position.Y * PixelsUnit);
            int width = x + comp.Image.Width;
            int height = y + comp.Image.Height;
            if (!(width < cx || x > cWidth || height < cy || y > cHeight))
            {
                comp.Image.Render(x, y);
            }
        }
    }

    public Render CreateComponent(Entity entity, Image image, Vector2 position)
    {
        var comp = new Render(entity, image, position);
        _components.Add(comp);
        entity.OnDestroy += () => _components.Remove(comp);
        return comp;
    }

    public class Render : Component
    {
        public Vector2 Position;
        public int ZIndex;
        public Image Image;
        public bool Visible;

        public Render(Entity entity, Image image, Vector2 position) : base(entity)
        {
            Image = image;
            ZIndex = 0;
            Position = position;
            Visible = true;
        }
    }
}
