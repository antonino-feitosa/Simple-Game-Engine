
using static SimpleGameEngine.Test.TestRunner;

namespace SimpleGameEngine.Test;

[TestClass]
public class BoundsTest
{
    public void GivenPointInBounds_whenCallContains_thenReturnTrue()
    {
        var bounds = new Bounds(new Point(5, 5), new Dimension(5, 5));
        var point = new Point(6, 6);

        var contains = bounds.Contains(point);

        AssertTrue(contains, "The point must be at bounds!");
    }

    public void GivenPointAtUpBorder_whenCallContains_thenReturnTrue()
    {
        var bounds = new Bounds(new Point(5, 5), new Dimension(5, 5));
        var point = new Point(6, 5);

        var contains = bounds.Contains(point);

        AssertTrue(contains, "The point must be at bounds!");
    }

    public void GivenPointAtDownBorder_whenCallContains_thenReturnTrue()
    {
        var bounds = new Bounds(new Point(5, 5), new Dimension(5, 5));
        var point = new Point(6, 10);

        var contains = bounds.Contains(point);

        AssertFalse(contains, "The point must not be at bounds!");
    }

    public void GivenPointAtLeftBorder_whenCallContains_thenReturnTrue()
    {
        var bounds = new Bounds(new Point(5, 5), new Dimension(5, 5));
        var point = new Point(5, 6);

        var contains = bounds.Contains(point);

        AssertTrue(contains, "The point must be at bounds!");
    }

    public void GivenPointAtRightBorder_whenCallContains_thenReturnTrue()
    {
        var bounds = new Bounds(new Point(5, 5), new Dimension(5, 5));
        var point = new Point(10, 6);

        var contains = bounds.Contains(point);

        AssertFalse(contains, "The point must not be at bounds!");
    }

    public void GivenPointOutOfUpBorder_whenCallContains_thenReturnFalse()
    {
        var bounds = new Bounds(new Point(5, 5), new Dimension(5, 5));
        var point = new Point(6, 0);

        var contains = bounds.Contains(point);

        AssertFalse(contains, "The point must not be at bounds!");
    }

    public void GivenPointOutOfDownBorder_whenCallContains_thenReturnFalse()
    {
        var bounds = new Bounds(new Point(5, 5), new Dimension(5, 5));
        var point = new Point(6, 12);

        var contains = bounds.Contains(point);

        AssertFalse(contains, "The point must not be at bounds!");
    }

    public void GivenPointOutOfLeftBorder_whenCallContains_thenReturnFalse()
    {
        var bounds = new Bounds(new Point(5, 5), new Dimension(5, 5));
        var point = new Point(0, 6);

        var contains = bounds.Contains(point);

        AssertFalse(contains, "The point must not be at bounds!");
    }

    public void GivenPointOutOfRightBorder_whenCallContains_thenReturnFalse()
    {
        var bounds = new Bounds(new Point(5, 5), new Dimension(5, 5));
        var point = new Point(12, 6);

        var contains = bounds.Contains(point);

        AssertFalse(contains, "The point must not be at bounds!");
    }
}
