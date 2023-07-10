using static SimpleGameEngine.Test.TestRunner;

namespace SimpleGameEngine.Test;

[TestClass]
public class Vector2Test
{
    public void GivenTwoVector_whenSumAnother_thenUpdateCoordinates(){
        var vector = new Vector2(4,6);
        var sumVector = new Vector2(1,2);

        vector.Sum(sumVector);

        AssertEquals(vector.X, 5, "The X coodinate must be 5!");
        AssertEquals(vector.Y, 8, "The Y coodinate must be 8!");
        AssertEquals(sumVector.X, 1, "The X coodinate must not change!");
        AssertEquals(sumVector.Y, 2, "The Y coodinate must not change!");
    }

    public void GivenVector_whenBothCoordinatesZero_thenIsZeroReturnTrue(){
        var vector = new Vector2(0,0);

        var result = vector.IsZero();

        AssertTrue(result, "The vector must be zero!");
    }

    public void GivenVector_whenOnlyXCoordinatesZero_thenIsZeroReturnFalse(){
        var vector = new Vector2(0,1);

        var result = vector.IsZero();

        AssertFalse(result, "The vector must not be zero!");
    }

    public void GivenVector_whenOnlyYCoordinatesZero_thenIsZeroReturnFalse(){
        var vector = new Vector2(1,0);

        var result = vector.IsZero();

        AssertFalse(result, "The vector must not be zero!");
    }

    public void GivenVector_whenTwoVectorAreTheSame_thenCloseEnoughReturnTrue(){
        var vector = new Vector2(4,6);
        var other = new Vector2(4,6);

        var result = vector.IsCloseEnough(other);

        AssertTrue(result, "The vectors must be close!");
    }

    public void GivenVector_whenTwoVectorAreCloseBy1DecimalPlace_thenCloseEnoughReturnTrue(){
        var vector = new Vector2(4,6.01);
        var other = new Vector2(4,6);

        var result = vector.IsCloseEnough(other);

        AssertTrue(result, "The vectors must be close!");
    }
}
