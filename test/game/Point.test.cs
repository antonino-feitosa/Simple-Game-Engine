
namespace SimpleGameEngine.Test;

[TestClass]
public class PointTest {

    public static void GivenPoint10x20_whenXIsGet_then10IsReturned(){
        var Point = new Point(10, 20);

        var x = Point.X;

        Test.Assert(x == 10);
    }

    public static void GivenPoint10x20_whenYIsGet_then20IsReturned(){
        var Point = new Point(10, 20);

        var y = Point.Y;

        Test.Assert(y == 20);
    }

    public static void GivenPoint10x20_whenXIsSetTo15_thenXIs15(){
        var Point = new Point(10, 20){ X = 15};

        var x = Point.X;

        Test.Assert(x == 15);
    }

    public static void GivenPoint10x20_whenYIsSetTo4_thenYIs4(){
        var Point = new Point(10, 20){ Y = 4};

        var y = Point.Y;

        Test.Assert(y == 4);
    }

    public static void GivenPoint10x20_whenPoint30x23IsCopied_thenXIs30(){
        var source = new Point(10, 20);
        var target = new Point(30, 23);

        source.Copy(target);
        var x = source.X;

        Test.Assert(x == 30);
    }

    public static void GivenPoint10x20_whenPoint30x23IsCopied_thenYIs23(){
        var source = new Point(10, 20);
        var target = new Point(30, 23);

        source.Copy(target);
        var y = source.Y;

        Test.Assert(y == 23);
    }

    public static void Given2Point10x20_whenEqualsIsCompared_thenTrueIsReturned(){
        var source = new Point(10, 20);
        var target = new Point(10, 20);

        var compare = source.Equals(target);

        Test.Assert(compare);
    }

    public static void GivenPoint10x20AndPoint1x5_whenEqualsIsCompared_thenFalseIsReturned(){
        var source = new Point(10, 20);
        var target = new Point(1, 5);

        var compare = !source.Equals(target);

        Test.Assert(compare);
    }

    public static void GivenPoint10x20AndPoint10x5_whenEqualsIsCompared_thenFalseIsReturned(){
        var source = new Point(10, 20);
        var target = new Point(10, 5);

        var compare = !source.Equals(target);

        Test.Assert(compare);
    }

    public static void GivenPoint10x20AndPoint7x20_whenEqualsIsCompared_thenFalseIsReturned(){
        var source = new Point(10, 20);
        var target = new Point(7, 20);

        var compare = !source.Equals(target);

        Test.Assert(compare);
    }

    public static void Given2Point10x20_whenHashCodeIsCompared_thenTrueIsReturned(){
        var source = new Point(10, 20);
        var target = new Point(10, 20);

        var compare = source.GetHashCode() == target.GetHashCode();

        Test.Assert(compare);
    }

    public static void GivenPoint10x20AndPoint1x5_whenHashCodeIsCompared_thenFalseIsReturned(){
        var source = new Point(10, 20);
        var target = new Point(1, 5);

        var compare = source.GetHashCode() != target.GetHashCode();

        Test.Assert(compare);
    }

    public static void GivenPoint10x20AndPoint10x5_whenHashCodeIsCompared_thenFalseIsReturned(){
        var source = new Point(10, 20);
        var target = new Point(10, 5);

        var compare = source.GetHashCode() != target.GetHashCode();

        Test.Assert(compare);
    }

    public static void GivenPoint10x20AndPoint7x20_whenHashCodeIsCompared_thenFalseIsReturned(){
        var source = new Point(10, 20);
        var target = new Point(7, 20);

        var compare = source.GetHashCode() != target.GetHashCode();

        Test.Assert(compare);
    }
}
