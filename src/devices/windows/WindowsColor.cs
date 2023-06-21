
using InternalColor = global::System.Drawing.Color;

namespace SimpleGameEngine;

internal class WindowsColor : IColor
{
    public SolidBrush _drawBrush;

    public WindowsColor()
    {
        var color = InternalColor.FromName("black");
        _drawBrush = new SolidBrush(color);
    }

    private WindowsColor(InternalColor color)
    {
        _drawBrush = new SolidBrush(color);
    }

    public static IColor FromName(string name)
    {
        var color = InternalColor.FromName(name);
        return new WindowsColor(color);
    }

    public static IColor FromRGB(int red, int green, int blue)
    {
        var color = InternalColor.FromArgb(red, green, blue);
        return new WindowsColor(color);
    }

    public void Dispose()
    {
        _drawBrush.Dispose();
    }
}
