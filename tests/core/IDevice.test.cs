
using static SimpleGameEngine.Test.TestRunner;

namespace SimpleGameEngine.Test;

public class IDeviceTest
{
    public IDevice device;

    public IDeviceTest(IDevice device)
    {
        this.device = device;
    }

    public void CheckGame()
    {
        var capturedException = false;

        try
        {
            var game = new Game();
            device.Game = game;
            device.Game = device.Game;
        }
        catch
        {
            capturedException = true;
        }

        Assert(capturedException == false);
    }
    public void CheckDimension()
    {
        var capturedException = false;

        try
        {
            var dimension = device.Dimension;
        }
        catch
        {
            capturedException = true;
        }

        Assert(capturedException == false);
    }
    public void CheckMousePosition()
    {
        var capturedException = false;

        try
        {
            var position = device.MousePosition;
        }
        catch
        {
            capturedException = true;
        }

        Assert(capturedException == false);
    }
    public void CheckNotFullScreen()
    {
        var capturedException = false;

        try
        {
            device.IsFullScreen = false;
        }
        catch
        {
            capturedException = true;
        }

        Assert(capturedException == false);
    }
    public void CheckFullScreen()
    {
        var capturedException = false;

        try
        {
            device.IsFullScreen = true;
        }
        catch
        {
            capturedException = true;
        }

        Assert(capturedException == false);
    }
    public void CheckFramesPerSecond()
    {
        var capturedException = false;

        try
        {
            device.FramesPerSecond = 24;
        }
        catch
        {
            capturedException = true;
        }

        Assert(capturedException == false);
    }

    public void GivenDevice_whenSetGame_thenCallGameStart()
    {
        var called = false;
        var game = new Game();
        var entity = new Entity(game);
        var component = new Component { OnStart = (entity) => called = true };
        entity.AttachComponent(component);

        device.Game = game;

        AssertTrue(called, "The device must call the game start when set!");
    }

    public void GivenDevice_whenSetGame_thenFireOnLoad()
    {
        var called = false;
        var game = new Game(){OnLoad = (device) => called = true };

        device.Game = game;

        AssertTrue(called, "The device must fire the event OnLoad at game when set!");
    }

    public void GivenValidPath_whenMakeImage_thenDoesNotThrowsFileNotFoundException()
    {
        var capturedException = false;

        try
        {
            device.MakeImage("resource.png");
        }
        catch (FileNotFoundException)
        {
            capturedException = true;
        }

        Assert(capturedException == false);
    }

    public void GivenInvalidPath_whenMakeImage_thenThrowsFileNotFoundException()
    {
        var capturedException = false;

        try
        {
            device.MakeImage("invalid path");
        }
        catch (FileNotFoundException)
        {
            capturedException = true;
        }

        Assert(capturedException == true);
    }

    public void GivenValidPath_whenMakeSound_thenDoesNotThrowsFileNotFoundException()
    {
        var capturedException = false;

        try
        {
            device.MakeSound("resource.mp3");
        }
        catch (FileNotFoundException)
        {
            capturedException = true;
        }

        Assert(capturedException == false);
    }

    public void GivenInvalidPath_whenMakeSound_thenThrowsFileNotFoundException()
    {
        var capturedException = false;

        try
        {
            device.MakeSound("invalid path");
        }
        catch (FileNotFoundException)
        {
            capturedException = true;
        }

        Assert(capturedException == true);
    }

    public void GivenValidPath_whenMakeFont_thenDoesNotThrowsFileNotFoundException()
    {
        var capturedException = false;

        try
        {
            device.MakeFont("resource.ttf");
        }
        catch (FileNotFoundException)
        {
            capturedException = true;
        }

        Assert(capturedException == false);
    }

    public void GivenInvalidPath_whenMakeFont_thenThrowsFileNotFoundException()
    {
        var capturedException = false;

        try
        {
            device.MakeFont("invalid path");
        }
        catch (FileNotFoundException)
        {
            capturedException = true;
        }

        Assert(capturedException == true);
    }


    public void GivenValidValues_whenMakeColor_thenDoesNotThrowsArgumentException()
    {
        var capturedException = false;

        try
        {
            device.MakeColor(0, 0, 0);
        }
        catch (FileNotFoundException)
        {
            capturedException = true;
        }

        Assert(capturedException == false);
    }

    public void GivenInvalidMaximumValues_whenMakeColor_thenThrowsArgumentException()
    {
        var capturedException = false;

        try
        {
            device.MakeColor(256, 256, 256);
        }
        catch (ArgumentException)
        {
            capturedException = true;
        }

        Assert(capturedException == true);
    }

    public void GivenInvalidMinimumValues_whenMakeColor_thenThrowsArgumentException()
    {
        var capturedException = false;

        try
        {
            device.MakeColor(-1, -1, -1);
        }
        catch (ArgumentException)
        {
            capturedException = true;
        }

        Assert(capturedException == true);
    }

    public void GivenFont_whenMakeText_thenDoesNotThrowsFileNotFoundException()
    {
        var capturedException = false;
        var font = device.MakeFont("resource.ttf");

        try
        {
            device.MakeText("text", font);
        }
        catch (FileNotFoundException)
        {
            capturedException = true;
        }

        Assert(capturedException == false);
    }

    public void GivenImage_whenMakeSpriteSheet_thenDoesNotThrowsFileNotFoundException()
    {
        var capturedException = false;
        var image = device.MakeImage("resource.png");

        try
        {
            device.MakeSpriteSheet(image, new Dimension(32, 32));
        }
        catch (FileNotFoundException)
        {
            capturedException = true;
        }

        Assert(capturedException == false);
    }
}
