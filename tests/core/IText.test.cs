
using static SimpleGameEngine.Test.TestRunner;

namespace SimpleGameEngine.Test;

public class ITextTest
{

    public IText text;
    public IDevice device;

    public ITextTest(IDevice device, IText text)
    {
        this.text = text;
        this.device = device;
    }

    public void CheckSize()
    {
        text.Size = 10;

        var size = text.Size;

        Assert(size == 10);
    }

    public void CheckFont()
    {
        var font = device.MakeFont("resource.ttf");

        text.Font = font;
        var prop = text.Font;

        Assert(font == prop);
    }

    public void CheckColor()
    {
        var color = device.MakeColor(0, 0, 0);

        text.Color = color;
        var prop = text.Color;

        Assert(color == prop);
    }

    public void CheckText()
    {
        var message = "message";

        text.Text = message;
        var prop = text.Text;

        Assert(message == prop);
    }

    public void CheckRender()
    {
        var point = new Point(0, 0);
        text.Text = "message";
        var capturedException = false;

        try
        {
            text.Render(point);
        }
        catch
        {
            capturedException = true;
        }

        Assert(capturedException == false);
    }

    public void CheckDispose()
    {
        var capturedException = false;

        try
        {
            text.Dispose();
        }
        catch
        {
            capturedException = true;
        }

        Assert(capturedException == false);
    }
}
