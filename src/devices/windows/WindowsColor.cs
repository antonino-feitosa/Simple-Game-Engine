
using InternalColor = global::System.Drawing.Color;

namespace SimpleGameEngine;

internal class WindowsColor : IColor
{
    public SolidBrush _drawBrush;

    public WindowsColor(int red8bits, int green8bits, int blue8bits, int alpha8bits)
    {
        var color = InternalColor.FromArgb(red8bits, green8bits, blue8bits, alpha8bits);
        _drawBrush = new SolidBrush(color);
    }

    public void Dispose()
    {
        _drawBrush.Dispose();
    }
}
