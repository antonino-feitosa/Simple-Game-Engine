
using static SimpleGameEngine.Test.TestRunner;

namespace SimpleGameEngine.Test;

public class IDeviceTest
{

    public IDevice device;

    public IDeviceTest(IDevice device)
    {
        this.device = device;
    }

    public void GivenValidPath_whenMakeImage_thenDoesNotThrowsResourceNotFoundException()
    {
        var capturedException = false;

        try
        {
            device.MakeImage("resource.png");
        }
        catch (ResourceNotFoundException)
        {
            capturedException = true;
        }

        Assert(capturedException == false);
    }

    public void GivenInvalidPath_whenMakeImage_thenThrowsResourceNotFoundException()
    {
        var capturedException = false;

        try
        {
            device.MakeImage("invalid path");
        }
        catch (ResourceNotFoundException)
        {
            capturedException = true;
        }

        Assert(capturedException == true);
    }

    public void GivenValidPath_whenMakeSound_thenDoesNotThrowsResourceNotFoundException()
    {
        var capturedException = false;

        try
        {
            device.MakeSound("resource.mp3");
        }
        catch (ResourceNotFoundException)
        {
            capturedException = true;
        }

        Assert(capturedException == false);
    }

    public void GivenInvalidPath_whenMakeSound_thenThrowsResourceNotFoundException()
    {
        var capturedException = false;

        try
        {
            device.MakeSound("invalid path");
        }
        catch (ResourceNotFoundException)
        {
            capturedException = true;
        }

        Assert(capturedException == true);
    }

    public void GivenValidPath_whenMakeFont_thenDoesNotThrowsResourceNotFoundException()
    {
        var capturedException = false;

        try
        {
            device.MakeFont("resource.ttf");
        }
        catch (ResourceNotFoundException)
        {
            capturedException = true;
        }

        Assert(capturedException == false);
    }

    public void GivenInvalidPath_whenMakeFont_thenThrowsResourceNotFoundException()
    {
        var capturedException = false;

        try
        {
            device.MakeFont("invalid path");
        }
        catch (ResourceNotFoundException)
        {
            capturedException = true;
        }

        Assert(capturedException == true);
    }


    public void GivenValidValues_whenMakeColor_thenDoesNotThrowsResourceNotFoundException()
    {
        var capturedException = false;

        try
        {
            device.MakeColor(0, 0, 0);
        }
        catch (ResourceNotFoundException)
        {
            capturedException = true;
        }

        Assert(capturedException == false);
    }

    public void GivenInvalidValues_whenMakeColor_thenThrowsResourceNotFoundException()
    {
        var capturedException = false;

        try
        {
            device.MakeColor(5000, 255, 0);
        }
        catch (ResourceNotFoundException)
        {
            capturedException = true;
        }

        Assert(capturedException == true);
    }

    public void GivenFont_whenMakeText_thenDoesNotThrowsResourceNotFoundException()
    {
        var capturedException = false;
        var font = device.MakeFont("resource.ttf");

        try
        {
            device.MakeText("text", font);
        }
        catch (ResourceNotFoundException)
        {
            capturedException = true;
        }

        Assert(capturedException == true);
    }

    public void GivenImage_whenMakeSpriteSheet_thenDoesNotThrowsResourceNotFoundException()
    {
        var capturedException = false;
        var image = device.MakeImage("resource.png");

        try
        {
            device.MakeSpriteSheet(image, new Dimension(32, 32));
        }
        catch (ResourceNotFoundException)
        {
            capturedException = true;
        }

        Assert(capturedException == true);
    }
}
