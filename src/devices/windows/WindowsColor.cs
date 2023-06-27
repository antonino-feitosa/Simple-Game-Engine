
using InternalColor = global::System.Drawing.Color;

namespace SimpleGameEngine;

internal class WindowsColor : IColor
{
    public SolidBrush _drawBrush;

    public WindowsColor(int red, int green, int blue)
    {
        var color = InternalColor.FromArgb(red, green, blue);
        _drawBrush = new SolidBrush(color);
    }

    private WindowsColor(InternalColor color)
    {
        _drawBrush = new SolidBrush(color);
    }

    public void Dispose()
    {
        _drawBrush.Dispose();
    }
}
