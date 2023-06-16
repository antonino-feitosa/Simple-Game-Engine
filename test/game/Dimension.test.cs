
using SGE;

namespace test;

public class DimensionTest
{

    public static void GivenDimension800x600_whenWidthIsGet_then800IsReturned()
    {
        var dimension = new Dimension(800, 600);

        var width = dimension.Width;

        Test.Assert(width == 800);
    }

    public static void GivenDimension800x600_whenHeightIsGet_then600IsReturned()
    {
        var dimension = new Dimension(800, 600);

        var height = dimension.Height;

        Test.Assert(height == 600);
    }

    public static void GivenDimension800x600_whenWidthIsSetToZero_thenZeroIsReturned()
    {
        var dimension = new Dimension(800, 600) { Width = 0 };

        var width = dimension.Width;

        Test.Assert(width == 0);
    }

    public static void GivenDimension800x600_whenHeightIsSetToZero_thenZeroIsReturned()
    {
        var dimension = new Dimension(800, 600) { Height = 0 };

        var height = dimension.Height;

        Test.Assert(height == 0);
    }

    public static void GivenDimension800x600_whenWidthIsSetToNegative_then800IsReturned()
    {
        var dimension = new Dimension(800, 600) { Width = -2 };

        var width = dimension.Width;

        Test.Assert(width == 800);
    }

    public static void GivenDimension800x600_whenHeightIsSetToNegative_then600IsReturned()
    {
        var dimension = new Dimension(800, 600) { Height = -3 };

        var height = dimension.Height;

        Test.Assert(height == 600);
    }

    public static void GivenDimension800x600_whenWidthIsSetTo320_thenWidthIs320()
    {
        var dimension = new Dimension(800, 600) { Width = 320 };

        var width = dimension.Width;

        Test.Assert(width == 320);
    }

    public static void GivenDimension800x600_whenHeightIsSetTo320_thenHeightIs320()
    {
        var dimension = new Dimension(800, 600) { Height = 320 };

        var height = dimension.Height;

        Test.Assert(height == 320);
    }

    public static void GivenDimension800x600_whenDimension320x240IsCopied_thenWidthIs320()
    {
        var dest = new Dimension(800, 600);
        var source = new Dimension(320, 240);

        dest.Copy(source);
        var width = dest.Width;

        Test.Assert(width == 320);
    }

    public static void GivenDimension800x600_whenDimension320x240IsCopied_thenHeightIs240()
    {
        var dest = new Dimension(800, 600);
        var source = new Dimension(320, 240);

        dest.Copy(source);
        var height = dest.Height;

        Test.Assert(height == 240);
    }

    public static void Given2Dimension800x600_whenEqualsIsCompared_thenTrueIsReturned()
    {
        var source = new Dimension(800, 600);
        var target = new Dimension(800, 600);

        var compare = source.Equals(target);

        Test.Assert(compare);
    }

    public static void GivenDimension800x600andDimension800x400_whenEqualsIsCompared_thenFalseIsReturned()
    {
        var source = new Dimension(800, 600);
        var target = new Dimension(800, 400);

        var compare = !source.Equals(target);

        Test.Assert(compare);
    }

    public static void GivenDimension800x600andDimension600x600_whenEqualsIsCompared_thenFalseIsReturned()
    {
        var source = new Dimension(800, 600);
        var target = new Dimension(600, 600);

        var compare = !source.Equals(target);

        Test.Assert(compare);
    }

    public static void GivenDimension800x600andDimension400x400_whenEqualsIsCompared_thenFalseIsReturned()
    {
        var source = new Dimension(800, 600);
        var target = new Dimension(400, 400);

        var compare = !source.Equals(target);

        Test.Assert(compare);
    }

    public static void Given2Dimension800x600_whenHashCodeIsCompared_thenTrueIsReturned()
    {
        var source = new Dimension(800, 600);
        var target = new Dimension(800, 600);

        var sourceHash = source.GetHashCode();
        var targetHash = target.GetHashCode();

        Test.Assert(sourceHash == targetHash);
    }

    public static void GivenDimension800x600andDimension800x400_whenHashCodeIsCompared_thenFalseIsReturned()
    {
        var source = new Dimension(800, 600);
        var target = new Dimension(800, 400);

        var sourceHash = source.GetHashCode();
        var targetHash = target.GetHashCode();

        Test.Assert(sourceHash != targetHash);
    }

    public static void GivenDimension800x600andDimension600x600_whenHashCodeIsCompared_thenFalseIsReturned()
    {
        var source = new Dimension(800, 600);
        var target = new Dimension(600, 600);

        var sourceHash = source.GetHashCode();
        var targetHash = target.GetHashCode();

        Test.Assert(sourceHash != targetHash);
    }

    public static void GivenDimension800x600andDimension400x400_whenHashCodeIsCompared_thenFalseIsReturned()
    {
        var source = new Dimension(800, 600);
        var target = new Dimension(400, 400);

        var sourceHash = source.GetHashCode();
        var targetHash = target.GetHashCode();

        Test.Assert(sourceHash != targetHash);
    }
}
