
namespace SimpleGameEngine;

/// <summary>
/// This system performs the rendering only of the <c>RenderableComponent</c> in the region.<see cref="RenderableComponent"/>
/// </summary>
public class CameraSystem : SystemBase<RenderableComponent>
{
    public Point Position;
    public Dimension Dimension;
    public int PixelsUnit;

    public CameraSystem(Point position, Dimension dimension)
    {
        Position = position;
        Dimension = dimension;
        PixelsUnit = 32;
        _components = new List<RenderableComponent>(); // TODO optimization on delete
    }

    public override void Process()
    {
        int cx = Position.X * PixelsUnit;
        int cy = Position.Y * PixelsUnit;
        int cWidth = cx + Dimension.Width;
        int cHeight = cy + Dimension.Height;
        foreach (var component in Components.OrderBy(c => c.ZIndex))
        { // TODO correct truncation location
            int x = (int)(component.Position.X * PixelsUnit);
            int y = (int)(component.Position.Y * PixelsUnit);
            int width = x + component.Image.Dimension.Width;
            int height = y + component.Image.Dimension.Height;
            if (!(width < cx || x > cWidth || height < cy || y > cHeight))
            {
                component.Image.Render(new Point(x, y));
            }
        }
    }
}
