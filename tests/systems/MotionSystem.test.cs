
using static SimpleGameEngine.Test.TestRunner;

namespace SimpleGameEngine.Test;

[TestClass]
public class MotionSystemTest
{

    public void GivenMovable_whenNotMoving_thenFireIdle()
    {
        var ground = new HashSet<Point> { new Point(0, 0), new Point(-1, 0) };
        var positionSystem = new PositionSystem(ground);
        var positionComponent = new PositionableComponent(positionSystem, new Point(0, 0));
        var system = new MotionSystem();
        var called = new List<int>();
        var pointsCalled = new List<Vector2>();
        var component = new MovableComponent(system, positionComponent)
        {
            OnMoveIdle = (vec) => { called.Add(0); pointsCalled.Add(new Vector2(vec)); },
            OnMoveStart = (vec) => { called.Add(1); pointsCalled.Add(new Vector2(vec)); },
            OnMoveIncrement = (vec) => { called.Add(2); pointsCalled.Add(new Vector2(vec)); },
            OnMoveEnd = (vec) => { called.Add(3); pointsCalled.Add(new Vector2(vec)); }
        };

        positionSystem.Process();
        system.Process();
        positionSystem.Process();
        system.Process();

        AssertEquals(called.Count, 2, "Only 2 events must be called!");
        AssertEquals(called[0], 0, "Only the Idle event must be called!");
        AssertEquals(called[1], 0, "Only the Idle event must be called!");
        AssertPrecisionEquals(pointsCalled[0].X, 0, "The X position must not change!");
        AssertPrecisionEquals(pointsCalled[0].Y, 0, "The Y position must not change!");
    }

    public void GivenMovable_whenCallMove_thenFireStartMove()
    {
        var ground = new HashSet<Point> { new Point(0, 0), new Point(-1, 0) };
        var positionSystem = new PositionSystem(ground);
        var positionComponent = new PositionableComponent(positionSystem, new Point(0, 0));
        var system = new MotionSystem();
        var called = new List<int>();
        var pointsCalled = new List<Vector2>();
        var component = new MovableComponent(system, positionComponent)
        {
            OnMoveIdle = (vec) => { called.Add(0); pointsCalled.Add(new Vector2(vec)); },
            OnMoveStart = (vec) => { called.Add(1); pointsCalled.Add(new Vector2(vec)); },
            OnMoveIncrement = (vec) => { called.Add(2); pointsCalled.Add(new Vector2(vec)); },
            OnMoveEnd = (vec) => { called.Add(3); pointsCalled.Add(new Vector2(vec)); }
        };

        positionComponent.Move(PositionSystem.LEFT);
        positionSystem.Process();
        system.Process();

        AssertEquals(called.Count, 2, "Only 2 event must be called!");
        AssertEquals(called[0], 1, "The Start event must be the first call!");
        AssertEquals(called[1], 2, "The Increment event must be the second call!");
        AssertPrecisionEquals(pointsCalled[0].X, 0, "The X position must not change!");
        AssertPrecisionEquals(pointsCalled[0].Y, 0, "The Y position must not change!");
    }

    public void GivenMovable_whenMoving_thenCallMoveIncremment()
    {
        var ground = new HashSet<Point> { new Point(0, 0), new Point(-1, 0) };
        var positionSystem = new PositionSystem(ground);
        var positionComponent = new PositionableComponent(positionSystem, new Point(0, 0));
        var system = new MotionSystem();
        var called = new List<int>();
        var pointsCalled = new List<Vector2>();
        var component = new MovableComponent(system, positionComponent)
        {
            FramesToMove = 3,
            OnMoveIdle = (vec) => { called.Add(0); pointsCalled.Add(new Vector2(vec)); },
            OnMoveStart = (vec) => { called.Add(1); pointsCalled.Add(new Vector2(vec)); },
            OnMoveIncrement = (vec) => { called.Add(2); pointsCalled.Add(new Vector2(vec)); },
            OnMoveEnd = (vec) => { called.Add(3); pointsCalled.Add(new Vector2(vec)); }
        };

        positionComponent.Move(PositionSystem.LEFT);
        positionSystem.Process();
        system.Process();
        positionSystem.Process();
        system.Process();

        AssertEquals(called.Count, 3, "Only 3 event must be called!");
        AssertEquals(called[0], 1, "The Start event must be the first call!");
        AssertEquals(called[1], 2, "The Increment event must be the second call!");
        AssertEquals(called[2], 2, "The Increment event must be the third call!");
        AssertPrecisionEquals(pointsCalled[0].X, 0, "The X position must change at Start!");
        AssertPrecisionEquals(pointsCalled[0].Y, 0, "The Y position must not change at Start!");
        AssertTrue(pointsCalled[0].X > pointsCalled[1].X, "The X position must increment!");
        AssertPrecisionEquals(component.Position.X, pointsCalled[2].X, "The X position must update!");
        AssertPrecisionEquals(component.Position.Y, pointsCalled[2].Y, "The Y position must update!");
    }

    public void GivenMovable_whenStopMoving_thenCallStopMoving()
    {
        var ground = new HashSet<Point> { new Point(0, 0), new Point(-1, 0) };
        var positionSystem = new PositionSystem(ground);
        var positionComponent = new PositionableComponent(positionSystem, new Point(0, 0));
        var system = new MotionSystem();
        var called = new List<int>();
        var pointsCalled = new List<Vector2>();
        var component = new MovableComponent(system, positionComponent)
        {
            FramesToMove = 1,
            OnMoveIdle = (vec) => { called.Add(0); pointsCalled.Add(new Vector2(vec)); },
            OnMoveStart = (vec) => { called.Add(1); pointsCalled.Add(new Vector2(vec)); },
            OnMoveIncrement = (vec) => { called.Add(2); pointsCalled.Add(new Vector2(vec)); },
            OnMoveEnd = (vec) => { called.Add(3); pointsCalled.Add(new Vector2(vec)); }
        };

        positionComponent.Move(PositionSystem.LEFT);
        positionSystem.Process();
        system.Process();
        var numEventsBetween = called.Count;
        positionSystem.Process();
        system.Process();

        AssertEquals(called.Count, 3, "Only 3 events must be called!");
        AssertEquals(numEventsBetween, 2, "Only 2 events must be called between frames!");
        AssertEquals(called[0], 1, "The Start event must be the first call!");
        AssertEquals(called[1], 2, "The Increment event must be the second call!");
        AssertEquals(called[2], 3, "The Stop event must be the third call!");
        AssertPrecisionEquals(pointsCalled[2].X, -1, "The final X position must change at End!");
        AssertPrecisionEquals(pointsCalled[2].Y, 0, "The final Y position must not change at End!");
        AssertPrecisionEquals(component.Position.X, -1, "The X position must update!");
        AssertPrecisionEquals(component.Position.Y, 0, "The Y position must update!");
    }

    public void GivenMovableWith3FramesToMove_whenSMoving_thenCallMoveIncrement3Times()
    {
        var ground = new HashSet<Point> { new Point(0, 0), new Point(-1, 0) };
        var positionSystem = new PositionSystem(ground);
        var positionComponent = new PositionableComponent(positionSystem, new Point(0, 0));
        var system = new MotionSystem();
        var called = new List<int>();
        var pointsCalled = new List<Vector2>();
        var component = new MovableComponent(system, positionComponent)
        {
            FramesToMove = 3,
            OnMoveIdle = (vec) => { called.Add(0); pointsCalled.Add(new Vector2(vec)); },
            OnMoveStart = (vec) => { called.Add(1); pointsCalled.Add(new Vector2(vec)); },
            OnMoveIncrement = (vec) => { called.Add(2); pointsCalled.Add(new Vector2(vec)); },
            OnMoveEnd = (vec) => { called.Add(3); pointsCalled.Add(new Vector2(vec)); }
        };

        positionComponent.Move(PositionSystem.LEFT);
        positionSystem.Process();
        system.Process();
        positionSystem.Process();
        system.Process();
        positionSystem.Process();
        system.Process();
        positionSystem.Process();
        system.Process();

        AssertEquals(called.Count, 5, "Only 3 events must be called!");
        AssertEquals(called[0], 1, "The Start event must be the first call!");
        AssertEquals(called[1], 2, "The Increment event must be the second call!");
        AssertEquals(called[2], 2, "The Increment event must be the third call!");
        AssertEquals(called[3], 2, "The Increment event must be the fourth call!");
        AssertEquals(called[4], 3, "The Stop event must be the fifth call!");
        AssertTrue(pointsCalled[0].X > pointsCalled[1].X, "The X position must increment!");
        AssertTrue(pointsCalled[1].X > pointsCalled[2].X, "The X position must increment!");
    }
}
