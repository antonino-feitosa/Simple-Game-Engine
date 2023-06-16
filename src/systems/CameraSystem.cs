
namespace SimpleGameEngine;

public class CameraSystem : System
{
    public Point Position;
    public Dimension Dimension;
    public int PixelsUnit;

    protected List<Renderable> _components;

    public CameraSystem(Point position, Dimension dimension)
    {
        Position = position;
        Dimension = dimension;
        PixelsUnit = 32;
        _components = new List<Renderable>(); // TODO optimization on delete
    }

    public void Process()
    {
        _components.Sort((a, b) => a.ZIndex - b.ZIndex);
        int cx = Position.X * PixelsUnit;
        int cy = Position.Y * PixelsUnit;
        int cWidth = cx + Dimension.Width;
        int cHeight = cy + Dimension.Height;
        foreach (var comp in _components)
        { // TODO correct truncation location
            int x = (int)(comp.Position.X * PixelsUnit);
            int y = (int)(comp.Position.Y * PixelsUnit);
            int width = x + comp.Image.Dimension.Width;
            int height = y + comp.Image.Dimension.Height;
            if (!(width < cx || x > cWidth || height < cy || y > cHeight))
            {
                comp.Image.Render(new Point(x, y));
            }
        }
    }

    public Renderable CreateComponent(Image image, Vector2 position)
    {
        var comp = new Renderable(image, position);
        _components.Add(comp);
        comp.OnDestroy += (entity) => _components.Remove(comp);
        return comp;
    }

    public class Renderable : Component
    {
        public Vector2 Position;
        public int ZIndex;
        public Image Image;
        public bool Visible;

        public Renderable(Image image, Vector2 position)
        {
            Image = image;
            ZIndex = 0;
            Position = position;
            Visible = true;
        }
    }
}
