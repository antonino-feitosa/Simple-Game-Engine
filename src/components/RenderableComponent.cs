
namespace SimpleGameEngine;

public class RenderableComponent : Component
{
    public Vector2 Position;
    public int ZIndex;
    public IImage Image;
    public bool Visible;

    public RenderableComponent(IImage image, Vector2 position)
    {
        Image = image;
        ZIndex = 0;
        Position = position;
        Visible = true;
    }
}