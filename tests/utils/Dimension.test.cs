
using static SimpleGameEngine.Test.TestRunner;

namespace SimpleGameEngine.Test;

[TestClass]
public class DimensionTest
{
    // Dimension must have no negative and indepedent values
    // Setting negative values results in error ArgumentException

    public void Given_whenCreateDimensionNegativeWidth_thenThrowsArgumentOutOfRangeException()
    {
        var capturedException = false;

        try
        {
            var dimension = new Dimension(-1, 0);
        }
        catch (ArgumentOutOfRangeException)
        {
            capturedException = true;
        }

        Assert(capturedException == true);
    }

    public void Given_whenCreateDimensionNegativeHeight_thenThrowsArgumentOutOfRangeException()
    {
        var capturedException = false;

        try
        {
            var dimension = new Dimension(0, -1);
        }
        catch (ArgumentOutOfRangeException)
        {
            capturedException = true;
        }

        Assert(capturedException == true);
    }

    public void GivenDimension800x600_whenWidthIsGet_then800IsReturned()
    {
        var dimension = new Dimension(800, 600);

        var width = dimension.Width;

        Assert(width == 800);
    }

    public void GivenDimension800x600_whenHeightIsGet_then600IsReturned()
    {
        var dimension = new Dimension(800, 600);

        var height = dimension.Height;

        Assert(height == 600);
    }

    public void GivenDimension800x600_whenWidthIsSetToZero_thenWidthIsZero()
    {
        var dimension = new Dimension(800, 600);

        dimension.Width = 0;
        var width = dimension.Width;

        Assert(width == 0);
    }

    public void GivenDimension800x600_whenHeightIsSetToZero_thenHeightIsZero()
    {
        var dimension = new Dimension(800, 600);

        dimension.Height = 0;
        var height = dimension.Height;

        Assert(height == 0);
    }

    public void GivenDimension800x600_whenWidthIsSetTo1_thenWidthIs1()
    {
        var dimension = new Dimension(800, 600);

        dimension.Width = 1;
        var width = dimension.Width;

        Assert(width == 1);
    }

    public void GivenDimension800x600_whenHeightIsSetTo1_thenHeightIs1()
    {
        var dimension = new Dimension(800, 600);

        dimension.Height = 1;
        var height = dimension.Height;

        Assert(height == 1);
    }

    public void GivenDimension800x600_whenWidthIsSetToNegative_thenThrowsArgumentOutOfRangeException()
    {
        var dimension = new Dimension(800, 600);
        bool capturedException = false;

        try
        {
            dimension.Width = -1;
        }
        catch (ArgumentOutOfRangeException)
        {
            capturedException = true;
        }

        Assert(capturedException);
    }

    public void GivenDimension800x600_whenHeightIsSetToNegative_thenThrowsArgumentOutOfRangeException()
    {
        var dimension = new Dimension(800, 600);
        bool capturedException = false;

        try
        {
            dimension.Height = -1;
        }
        catch (ArgumentOutOfRangeException)
        {
            capturedException = true;
        }

        Assert(capturedException);
    }

    public void GivenDimension800x600_whenWidthIsSetToNegativeThrowingArgumentOutOfRangeException_thenWidthDontChange()
    {
        var dimension = new Dimension(800, 600);

        try
        {
            dimension.Width = -1;
        }
        catch (ArgumentOutOfRangeException) { }
        var width = dimension.Width;

        Assert(width == 800);
    }

    public void GivenDimension800x600_whenHeightIsSetToNegativeThrowingArgumentOutOfRangeException_thenHeightDontChange()
    {
        var dimension = new Dimension(800, 600);

        try
        {
            dimension.Height = -1;
        }
        catch (ArgumentOutOfRangeException) { }
        var height = dimension.Height;

        Assert(height == 600);
    }

    public void GivenDimension800x600_whenWidthIsSetTo320_thenWidthIs320()
    {
        var dimension = new Dimension(800, 600);

        dimension.Width = 320;
        var width = dimension.Width;

        Assert(width == 320);
    }

    public void GivenDimension800x600_whenWidthIsSetTo320_thenHeightIs600()
    {
        var dimension = new Dimension(800, 600);

        dimension.Width = 320;
        var height = dimension.Height;

        Assert(height == 600);
    }

    public void GivenDimension800x600_whenHeightIsSetTo320_thenHeightIs320()
    {
        var dimension = new Dimension(800, 600);

        dimension.Height = 320;
        var height = dimension.Height;

        Assert(height == 320);
    }

    public void GivenDimension800x600_whenHeightIsSetTo320_thenWidthIs800()
    {
        var dimension = new Dimension(800, 600);

        dimension.Height = 320;
        var width = dimension.Width;

        Assert(width == 800);
    }

    public void GivenDimension800x600_whenDimension320x240IsCopied_thenWidthIs320()
    {
        var dest = new Dimension(800, 600);
        var source = new Dimension(320, 240);

        dest.Copy(source);
        var width = dest.Width;

        Assert(width == 320);
    }

    public void GivenDimension800x600_whenDimension320x240IsCopied_thenHeightIs240()
    {
        var dest = new Dimension(800, 600);
        var source = new Dimension(320, 240);

        dest.Copy(source);
        var height = dest.Height;

        Assert(height == 240);
    }

    public void Given2Dimension800x600_whenEqualsIsCompared_thenTrueIsReturned()
    {
        var source = new Dimension(800, 600);
        var target = new Dimension(800, 600);

        var compare = source.Equals(target);

        Assert(compare == true);
    }

    public void GivenDimension800x600andDimension800x400_whenEqualsIsCompared_thenFalseIsReturned()
    {
        var source = new Dimension(800, 600);
        var target = new Dimension(800, 400);

        var compare = source.Equals(target);

        Assert(compare == false);
    }

    public void GivenDimension800x600andDimension600x600_whenEqualsIsCompared_thenFalseIsReturned()
    {
        var source = new Dimension(800, 600);
        var target = new Dimension(600, 600);

        var compare = source.Equals(target);

        Assert(compare == false);
    }

    public void GivenDimension800x600andDimension400x400_whenEqualsIsCompared_thenFalseIsReturned()
    {
        var source = new Dimension(800, 600);
        var target = new Dimension(400, 400);

        var compare = source.Equals(target);

        Assert(compare == false);
    }

    public void Given2Dimension800x600_whenHashCodeIsCompared_thenTrueIsReturned()
    {
        var source = new Dimension(800, 600);
        var target = new Dimension(800, 600);

        var sourceHash = source.GetHashCode();
        var targetHash = target.GetHashCode();

        Assert(sourceHash == targetHash);
    }

    public void GivenDimension800x600andDimension800x400_whenHashCodeIsCompared_thenFalseIsReturned()
    {
        var source = new Dimension(800, 600);
        var target = new Dimension(800, 400);

        var sourceHash = source.GetHashCode();
        var targetHash = target.GetHashCode();

        Assert(sourceHash != targetHash);
    }

    public void GivenDimension800x600andDimension600x600_whenHashCodeIsCompared_thenFalseIsReturned()
    {
        var source = new Dimension(800, 600);
        var target = new Dimension(600, 600);

        var sourceHash = source.GetHashCode();
        var targetHash = target.GetHashCode();

        Assert(sourceHash != targetHash);
    }

    public void GivenDimension800x600andDimension400x400_whenHashCodeIsCompared_thenFalseIsReturned()
    {
        var source = new Dimension(800, 600);
        var target = new Dimension(400, 400);

        var sourceHash = source.GetHashCode();
        var targetHash = target.GetHashCode();

        Assert(sourceHash != targetHash);
    }
}
