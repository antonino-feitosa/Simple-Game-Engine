
namespace SimpleGameEngine;

public class InterfaciableComponent : Component
{
    public Bounds Bounds;

    public Action? OnMouseDown;
    public Action? OnMouseUp;
    public Action? OnReset;

    public InterfaciableComponent(InterfaceSystem system, Bounds bounds)
    {
        Bounds = new Bounds(bounds);
        system.AddComponent(this);
    }
}
