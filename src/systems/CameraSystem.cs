
namespace SimpleGameEngine;

public class CameraSystem : ISystem
{
    public Point Position;
    public Dimension Dimension;
    public int PixelsUnit;

    protected List<RenderableComponent> _components;

    public CameraSystem(Point position, Dimension dimension)
    {
        Position = position;
        Dimension = dimension;
        PixelsUnit = 32;
        _components = new List<RenderableComponent>(); // TODO optimization on delete
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

    internal void AddComponent(RenderableComponent component)
    {
        _components.Add(component);
        component.OnDestroy += (entity) => _components.Remove(component);
    }
}
