
namespace SimpleGameEngine;

/// <summary>
/// This system performs the rendering only of the <c>RenderableComponent</c> in the region.<see cref="RenderableComponent"/>
/// This uses world coordinates mapped to screen throught PixelsUnit, that is, a world unit is mapped to PixelsUnits of the screen.
/// </summary>
public class RenderingSystem : SystemBase<RenderableComponent>
{
    public Point Position;
    public Dimension Dimension;
    public int PixelsUnit;

    public RenderingSystem(Point position, Dimension dimension)
    {
        Position = position;
        Dimension = dimension;
        PixelsUnit = 32;
        _components = new List<RenderableComponent>(); // TODO optimization on delete
    }

    public override void Process()
    {
        int cx = Position.X;
        int cy = Position.Y;
        int cWidth = cx + Dimension.Width;
        int cHeight = cy + Dimension.Height;
        foreach (var component in Components.Where(c => c.Visible).OrderBy(c => c.ZIndex))
        { // TODO correct truncation location
            int x = component.Position.X;
            int y = component.Position.Y;
            int width = x + component.Image.Dimension.Width / PixelsUnit;
            int height = y + component.Image.Dimension.Height / PixelsUnit;
            if (!(width <= cx || x >= cWidth || height <= cy || y >= cHeight))
            {
                component.Image.Render(new Point(x * PixelsUnit, y * PixelsUnit));
            }
        }
    }
}
