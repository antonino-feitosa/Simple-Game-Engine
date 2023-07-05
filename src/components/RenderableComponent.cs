
namespace SimpleGameEngine;

public class RenderableComponent : Component
{
    public Point Position;
    public int ZIndex;
    public IImage Image;
    public bool Visible;

    public RenderableComponent(RenderingSystem system, IImage image, Point position)
    {
        Image = image;
        ZIndex = 0;
        Position = position;
        Visible = true;
        system.AddComponent(this);
    }
}
