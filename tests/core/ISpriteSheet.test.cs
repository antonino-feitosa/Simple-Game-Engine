
using static SimpleGameEngine.Test.TestRunner;

namespace SimpleGameEngine.Test;

public class ISpriteSheetTest
{

    public ISpriteSheet spriteSheet;

    public ISpriteSheetTest(ISpriteSheet spriteSheet)
    {
        this.spriteSheet = spriteSheet;
    }

    public void CheckLenght()
    {
        var length = spriteSheet.Length;

        Assert(length > 0);
    }

    public void CheckGetFirstSprite()
    {
        var capturedException = false;

        try
        {
            spriteSheet.GetSprite(0);
        }
        catch
        {
            capturedException = true;
        }

        Assert(capturedException == false);
    }

    public void CheckGetLastSprite()
    {
        var last = spriteSheet.Length - 1;
        var capturedException = false;

        try
        {
            spriteSheet.GetSprite(last);
        }
        catch
        {
            capturedException = true;
        }

        Assert(capturedException == false);
    }
}
