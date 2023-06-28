
using static SimpleGameEngine.Test.TestRunner;

namespace SimpleGameEngine.Test;

public class IColorTest
{
    public IColor color;
    public IColorTest(IColor color)
    {
        this.color = color;
    }

    public void CheckDispose()
    {
        var capturedException = false;

        try
        {
            color.Dispose();
        }
        catch
        {
            capturedException = true;
        }

        Assert(capturedException == false);
    }
}
