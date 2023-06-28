
using static SimpleGameEngine.Test.TestRunner;

namespace SimpleGameEngine.Test;

public class IImageTest
{
    public IImage image;
    public IImageTest(IImage image)
    {
        this.image = image;
    }

    public void CheckDimensionWidth()
    {
        var width = image.Dimension.Width;

        Assert(width > 0);
    }

    public void CheckDimensionHeight()
    {
        var width = image.Dimension.Height;

        Assert(width > 0);
    }

    public void CheckRender()
    {
        var capturedException = false;

        try
        {
            image.Render(new Point(0, 0));
        }
        catch
        {
            capturedException = true;
        }

        Assert(capturedException == false);
    }

    public void CheckResize()
    {
        var capturedException = false;

        try
        {
            image.Resize(new Dimension(32, 32));
        }
        catch
        {
            capturedException = true;
        }

        Assert(capturedException == false);
    }

    public void CheckCrop()
    {
        var capturedException = false;

        try
        {
            image.Crop(new Point(0, 0), new Dimension(1, 1));
        }
        catch
        {
            capturedException = true;
        }

        Assert(capturedException == false);
    }

    public void GivenImage_whenResizeDimensionZeroWidth_thenThrowsAgumentException()
    {
        var capturedException = false;

        try
        {
            image.Resize(new Dimension(0, 32));
        }
        catch
        {
            capturedException = true;
        }

        Assert(capturedException == true);
    }

    public void GivenImage_whenResizeDimensionZeroHeight_thenThrowsAgumentException()
    {
        var capturedException = false;

        try
        {
            image.Resize(new Dimension(32, 0));
        }
        catch
        {
            capturedException = true;
        }

        Assert(capturedException == true);
    }

    public void GivenImage_whenCropNegativePointX_thenThrowsAgumentException()
    {
        var capturedException = false;

        try
        {
            image.Crop(new Point(-1, 0), new Dimension(1, 1));
        }
        catch
        {
            capturedException = true;
        }

        Assert(capturedException == true);
    }

    public void GivenImage_whenCropNegativePointY_thenThrowsAgumentException()
    {
        var capturedException = false;

        try
        {
            image.Crop(new Point(0, -1), new Dimension(1, 1));
        }
        catch
        {
            capturedException = true;
        }

        Assert(capturedException == true);
    }

    public void GivenImage_whenCropDimensionZeroWidth_thenThrowsAgumentException()
    {
        var capturedException = false;

        try
        {
            image.Crop(new Point(0, 0), new Dimension(0, 1));
        }
        catch
        {
            capturedException = true;
        }

        Assert(capturedException == true);
    }

    public void GivenImage_whenCropDimensionZeroHeight_thenThrowsAgumentException()
    {
        var capturedException = false;

        try
        {
            image.Crop(new Point(0, 0), new Dimension(1, 0));
        }
        catch
        {
            capturedException = true;
        }

        Assert(capturedException == true);
    }
}