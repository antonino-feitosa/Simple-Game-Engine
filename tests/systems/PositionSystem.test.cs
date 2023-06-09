
using static SimpleGameEngine.Test.TestRunner;

namespace SimpleGameEngine.Test;

[TestClass]
public class PositionSystemTest
{
    public void GivenComponent_whenMoveLeft_thenFireOnMove()
    {
        var called = false;
        var sourceCalled = new Point(-1, -1);
        var destinationCalled = new Point(-1, -1);
        var ground = new HashSet<Point> { new Point(0, 0), new Point(-1, 0) };
        var system = new PositionSystem(ground);
        var component = new PositionableComponent(system, new Point(0, 0))
        {
            OnMove = (source, destination) =>
            {
                called = true;
                sourceCalled = source;
                destinationCalled = destination;
            }
        };

        component.Move(PositionSystem.LEFT);
        system.Process();

        AssertTrue(called, "The event OnMove must be called!");
        AssertEquals(component.Position.X, -1, "The component must move on X.");
        AssertEquals(component.Position.Y, 0, "The component must not move on Y.");
        AssertEquals(sourceCalled.X, 0, "The source argument of OnMove is wrong for X.");
        AssertEquals(sourceCalled.Y, 0, "The source argument of OnMove is wrong for Y.");
        AssertEquals(destinationCalled.X, -1, "The destination argument of OnMove is wrong for X.");
        AssertEquals(destinationCalled.Y, 0, "The destination argument of OnMove is wrong for Y.");
    }
    public void GivenComponent_whenMoveRight_thenFireOnMove()
    {
        var called = false;
        var sourceCalled = new Point(-1, -1);
        var destinationCalled = new Point(-1, -1);
        var ground = new HashSet<Point> { new Point(0, 0), new Point(1, 0) };
        var system = new PositionSystem(ground);
        var component = new PositionableComponent(system, new Point(0, 0))
        {
            OnMove = (source, destination) =>
            {
                called = true;
                sourceCalled = source;
                destinationCalled = destination;
            }
        };

        component.Move(PositionSystem.RIGHT);
        system.Process();

        AssertTrue(called, "The event OnMove must be called!");
        AssertEquals(component.Position.X, 1, "The component must move on X.");
        AssertEquals(component.Position.Y, 0, "The component must not move on Y.");
        AssertEquals(sourceCalled.X, 0, "The source argument of OnMove is wrong for X.");
        AssertEquals(sourceCalled.Y, 0, "The source argument of OnMove is wrong for Y.");
        AssertEquals(destinationCalled.X, 1, "The destination argument of OnMove is wrong for X.");
        AssertEquals(destinationCalled.Y, 0, "The destination argument of OnMove is wrong for Y.");
    }
    public void GivenComponent_whenMoveDown_thenFireOnMove()
    {
        var called = false;
        var sourceCalled = new Point(-1, -1);
        var destinationCalled = new Point(-1, -1);
        var ground = new HashSet<Point> { new Point(0, 0), new Point(0, -1) };
        var system = new PositionSystem(ground);
        var component = new PositionableComponent(system, new Point(0, 0))
        {
            OnMove = (source, destination) =>
            {
                called = true;
                sourceCalled = source;
                destinationCalled = destination;
            }
        };

        component.Move(PositionSystem.DOWN);
        system.Process();

        AssertTrue(called, "The event OnMove must be called!");
        AssertEquals(component.Position.X, 0, "The component must move on X.");
        AssertEquals(component.Position.Y, -1, "The component must not move on Y.");
        AssertEquals(sourceCalled.X, 0, "The source argument of OnMove is wrong for X.");
        AssertEquals(sourceCalled.Y, 0, "The source argument of OnMove is wrong for Y.");
        AssertEquals(destinationCalled.X, 0, "The destination argument of OnMove is wrong for X.");
        AssertEquals(destinationCalled.Y, -1, "The destination argument of OnMove is wrong for Y.");
    }
    public void GivenComponent_whenMoveUpLeft_thenFireOnMove()
    {
        var called = false;
        var sourceCalled = new Point(-1, -1);
        var destinationCalled = new Point(-1, -1);
        var ground = new HashSet<Point> { new Point(0, 0), new Point(-1, 1) };
        var system = new PositionSystem(ground);
        var component = new PositionableComponent(system, new Point(0, 0))
        {
            OnMove = (source, destination) =>
            {
                called = true;
                sourceCalled = source;
                destinationCalled = destination;
            }
        };

        component.Move(PositionSystem.UP_LEFT);
        system.Process();

        AssertTrue(called, "The event OnMove must be called!");
        AssertEquals(component.Position.X, -1, "The component must move on X.");
        AssertEquals(component.Position.Y, 1, "The component must not move on Y.");
        AssertEquals(sourceCalled.X, 0, "The source argument of OnMove is wrong for X.");
        AssertEquals(sourceCalled.Y, 0, "The source argument of OnMove is wrong for Y.");
        AssertEquals(destinationCalled.X, -1, "The destination argument of OnMove is wrong for X.");
        AssertEquals(destinationCalled.Y, 1, "The destination argument of OnMove is wrong for Y.");
    }
    public void GivenComponent_whenMoveUpRight_thenFireOnMove()
    {
        var called = false;
        var sourceCalled = new Point(-1, -1);
        var destinationCalled = new Point(-1, -1);
        var ground = new HashSet<Point> { new Point(0, 0), new Point(1, 1) };
        var system = new PositionSystem(ground);
        var component = new PositionableComponent(system, new Point(0, 0))
        {
            OnMove = (source, destination) =>
            {
                called = true;
                sourceCalled = source;
                destinationCalled = destination;
            }
        };

        component.Move(PositionSystem.UP_RIGHT);
        system.Process();

        AssertTrue(called, "The event OnMove must be called!");
        AssertEquals(component.Position.X, 1, "The component must move on X.");
        AssertEquals(component.Position.Y, 1, "The component must not move on Y.");
        AssertEquals(sourceCalled.X, 0, "The source argument of OnMove is wrong for X.");
        AssertEquals(sourceCalled.Y, 0, "The source argument of OnMove is wrong for Y.");
        AssertEquals(destinationCalled.X, 1, "The destination argument of OnMove is wrong for X.");
        AssertEquals(destinationCalled.Y, 1, "The destination argument of OnMove is wrong for Y.");
    }
    public void GivenComponent_whenMoveDownLeft_thenFireOnMove()
    {
        var called = false;
        var sourceCalled = new Point(-1, -1);
        var destinationCalled = new Point(-1, -1);
        var ground = new HashSet<Point> { new Point(0, 0), new Point(-1, -1) };
        var system = new PositionSystem(ground);
        var component = new PositionableComponent(system, new Point(0, 0))
        {
            OnMove = (source, destination) =>
            {
                called = true;
                sourceCalled = source;
                destinationCalled = destination;
            }
        };

        component.Move(PositionSystem.DOWN_LEFT);
        system.Process();

        AssertTrue(called, "The event OnMove must be called!");
        AssertEquals(component.Position.X, -1, "The component must move on X.");
        AssertEquals(component.Position.Y, -1, "The component must not move on Y.");
        AssertEquals(sourceCalled.X, 0, "The source argument of OnMove is wrong for X.");
        AssertEquals(sourceCalled.Y, 0, "The source argument of OnMove is wrong for Y.");
        AssertEquals(destinationCalled.X, -1, "The destination argument of OnMove is wrong for X.");
        AssertEquals(destinationCalled.Y, -1, "The destination argument of OnMove is wrong for Y.");
    }
    public void GivenComponent_whenMoveDownRight_thenFireOnMove()
    {
        var called = false;
        var sourceCalled = new Point(-1, -1);
        var destinationCalled = new Point(-1, -1);
        var ground = new HashSet<Point> { new Point(0, 0), new Point(1, -1) };
        var system = new PositionSystem(ground);
        var component = new PositionableComponent(system, new Point(0, 0))
        {
            OnMove = (source, destination) =>
            {
                called = true;
                sourceCalled = source;
                destinationCalled = destination;
            }
        };

        component.Move(PositionSystem.DOWN_RIGHT);
        system.Process();

        AssertTrue(called, "The event OnMove must be called!");
        AssertEquals(component.Position.X, 1, "The component must move on X.");
        AssertEquals(component.Position.Y, -1, "The component must not move on Y.");
        AssertEquals(sourceCalled.X, 0, "The source argument of OnMove is wrong for X.");
        AssertEquals(sourceCalled.Y, 0, "The source argument of OnMove is wrong for Y.");
        AssertEquals(destinationCalled.X, 1, "The destination argument of OnMove is wrong for X.");
        AssertEquals(destinationCalled.Y, -1, "The destination argument of OnMove is wrong for Y.");
    }

    public void GivenComponent_whenMoveUp_thenFireOnMove()
    {
        var called = false;
        var sourceCalled = new Point(-1, -1);
        var destinationCalled = new Point(-1, -1);
        var ground = new HashSet<Point> { new Point(0, 0), new Point(0, 1) };
        var system = new PositionSystem(ground);
        var component = new PositionableComponent(system, new Point(0, 0))
        {
            OnMove = (source, destination) =>
            {
                called = true;
                sourceCalled = source;
                destinationCalled = destination;
            }
        };

        component.Move(PositionSystem.UP);
        system.Process();

        AssertTrue(called, "The event OnMove must be called!");
        AssertEquals(component.Position.X, 0, "The component must not move on X.");
        AssertEquals(component.Position.Y, 1, "The component must move on Y.");
        AssertEquals(sourceCalled.X, 0, "The source argument of OnMove must be 0 for X.");
        AssertEquals(sourceCalled.Y, 0, "The source argument of OnMove must be 0 for Y.");
        AssertEquals(destinationCalled.X, 0, "The destination argument of OnMove must be 0 for X.");
        AssertEquals(destinationCalled.Y, 1, "The destination argument of OnMove must be 1 for Y.");
    }

    public void GivenComponentAtUpBorder_whenMoveUp_thenFireOutOfBounds()
    {
        var called = false;
        var destinationCalled = new Point(-1, -1);
        var ground = new HashSet<Point> { new Point(0, 0), new Point(0, 1) };
        var system = new PositionSystem(ground);
        var component = new PositionableComponent(system, new Point(0, 1))
        {
            OnOutOfBounds = (destination) =>
            {
                called = true;
                destinationCalled = destination;
            }
        };

        component.Move(PositionSystem.UP);
        system.Process();

        AssertTrue(called, "The event OnOutOfBounds must be called!");
        AssertEquals(component.Position.X, 0, "The component must not move on X.");
        AssertEquals(component.Position.Y, 1, "The component must not move on Y.");
        AssertEquals(destinationCalled.X, 0, "The destination argument of OnMove must be 0 for X.");
        AssertEquals(destinationCalled.Y, 2, "The destination argument of OnMove must be 2 for Y.");
    }

    public void GivenComponentAWithComponentBUp_whenAMoveUpBMoveUp_thenTheyFireOnMove()
    {
        var aCalled = false;
        var aSourceCalled = new Point(-1, -1);
        var aDestinationCalled = new Point(-1, -1);
        var bCalled = false;
        var bSourceCalled = new Point(-1, -1);
        var bDestinationCalled = new Point(-1, -1);
        var ground = new HashSet<Point> { new Point(0, 0), new Point(0, 1), new Point(0, 2) };
        var system = new PositionSystem(ground);
        var aComponent = new PositionableComponent(system, new Point(0, 0));
        aComponent.OnMove = (source, destination) => { aCalled = true; aSourceCalled = source; aDestinationCalled = destination; };
        var bComponent = new PositionableComponent(system, new Point(0, 1));
        bComponent.OnMove = (source, destination) => { bCalled = true; bSourceCalled = source; bDestinationCalled = destination; };

        aComponent.Move(PositionSystem.UP);
        bComponent.Move(PositionSystem.UP);
        system.Process();

        AssertTrue(aCalled, "The A event OnMove must be called!");
        AssertEquals(aComponent.Position.X, 0, "The A component must not move on X.");
        AssertEquals(aComponent.Position.Y, 1, "The A component must move on Y.");
        AssertEquals(aSourceCalled.X, 0, "The A source argument of OnMove must be 0 for X.");
        AssertEquals(aSourceCalled.Y, 0, "The A source argument of OnMove must be 0 for Y.");
        AssertEquals(aDestinationCalled.X, 0, "The A destination argument of OnMove must be 0 for X.");
        AssertEquals(aDestinationCalled.Y, 1, "The A destination argument of OnMove must be 1 for Y.");

        AssertTrue(bCalled, "The B event OnMove must be called!");
        AssertEquals(bComponent.Position.X, 0, "The B component must not move on X.");
        AssertEquals(bComponent.Position.Y, 2, "The B component must move on Y.");
        AssertEquals(bSourceCalled.X, 0, "The B source argument of OnMove must be 0 for X.");
        AssertEquals(bSourceCalled.Y, 1, "The B source argument of OnMove must be 1 for Y.");
        AssertEquals(bDestinationCalled.X, 0, "The B destination argument of OnMove must be 0 for X.");
        AssertEquals(bDestinationCalled.Y, 2, "The B destination argument of OnMove must be 2 for Y.");
    }

    public void GivenComponentAWithComponentBUp_whenBMoveUpAMoveUp_thenTheyFireOnMove()
    {
        var aCalled = false;
        var aSourceCalled = new Point(-1, -1);
        var aDestinationCalled = new Point(-1, -1);
        var bCalled = false;
        var bSourceCalled = new Point(-1, -1);
        var bDestinationCalled = new Point(-1, -1);
        var ground = new HashSet<Point> { new Point(0, 0), new Point(0, 1), new Point(0, 2) };
        var system = new PositionSystem(ground);
        var aComponent = new PositionableComponent(system, new Point(0, 0));
        aComponent.OnMove = (source, destination) => { aCalled = true; aSourceCalled = source; aDestinationCalled = destination; };
        var bComponent = new PositionableComponent(system, new Point(0, 1));
        bComponent.OnMove = (source, destination) => { bCalled = true; bSourceCalled = source; bDestinationCalled = destination; };

        bComponent.Move(PositionSystem.UP);
        aComponent.Move(PositionSystem.UP);
        system.Process();

        AssertTrue(aCalled, "The A event OnMove must be called!");
        AssertEquals(aComponent.Position.X, 0, "The A component must not move on X.");
        AssertEquals(aComponent.Position.Y, 1, "The A component must move on Y.");
        AssertEquals(aSourceCalled.X, 0, "The A source argument of OnMove must be 0 for X.");
        AssertEquals(aSourceCalled.Y, 0, "The A source argument of OnMove must be 0 for Y.");
        AssertEquals(aDestinationCalled.X, 0, "The A destination argument of OnMove must be 0 for X.");
        AssertEquals(aDestinationCalled.Y, 1, "The A destination argument of OnMove must be 1 for Y.");

        AssertTrue(bCalled, "The B event OnMove must be called!");
        AssertEquals(bComponent.Position.X, 0, "The B component must not move on X.");
        AssertEquals(bComponent.Position.Y, 2, "The B component must move on Y.");
        AssertEquals(bSourceCalled.X, 0, "The B source argument of OnMove must be 0 for X.");
        AssertEquals(bSourceCalled.Y, 1, "The B source argument of OnMove must be 1 for Y.");
        AssertEquals(bDestinationCalled.X, 0, "The B destination argument of OnMove must be 0 for X.");
        AssertEquals(bDestinationCalled.Y, 2, "The B destination argument of OnMove must be 2 for Y.");
    }

    public void GivenComponentAWithComponentBUpWithComponentCUp_whenAMoveUpBMoveUpCMoveUp_thenTheyFireOnMove()
    {
        var aCalled = false;
        var aSourceCalled = new Point(-1, -1);
        var aDestinationCalled = new Point(-1, -1);
        var bCalled = false;
        var bSourceCalled = new Point(-1, -1);
        var bDestinationCalled = new Point(-1, -1);
        var cCalled = false;
        var cSourceCalled = new Point(-1, -1);
        var cDestinationCalled = new Point(-1, -1);
        var ground = new HashSet<Point> { new Point(0, 0), new Point(0, 1), new Point(0, 2), new Point(0, 3) };
        var system = new PositionSystem(ground);
        var aComponent = new PositionableComponent(system, new Point(0, 0));
        aComponent.OnMove = (source, destination) => { aCalled = true; aSourceCalled = source; aDestinationCalled = destination; };
        var bComponent = new PositionableComponent(system, new Point(0, 1));
        bComponent.OnMove = (source, destination) => { bCalled = true; bSourceCalled = source; bDestinationCalled = destination; };
        var cComponent = new PositionableComponent(system, new Point(0, 2));
        cComponent.OnMove = (source, destination) => { cCalled = true; cSourceCalled = source; cDestinationCalled = destination; };

        aComponent.Move(PositionSystem.UP);
        bComponent.Move(PositionSystem.UP);
        cComponent.Move(PositionSystem.UP);
        system.Process();

        AssertTrue(aCalled, "The A event OnMove must be called!");
        AssertEquals(aComponent.Position.X, 0, "The A component must not move on X.");
        AssertEquals(aComponent.Position.Y, 1, "The A component must move on Y.");
        AssertEquals(aSourceCalled.X, 0, "The A source argument of OnMove must be 0 for X.");
        AssertEquals(aSourceCalled.Y, 0, "The A source argument of OnMove must be 0 for Y.");
        AssertEquals(aDestinationCalled.X, 0, "The A destination argument of OnMove must be 0 for X.");
        AssertEquals(aDestinationCalled.Y, 1, "The A destination argument of OnMove must be 1 for Y.");

        AssertTrue(bCalled, "The B event OnMove must be called!");
        AssertEquals(bComponent.Position.X, 0, "The B component must not move on X.");
        AssertEquals(bComponent.Position.Y, 2, "The B component must move on Y.");
        AssertEquals(bSourceCalled.X, 0, "The B source argument of OnMove must be 0 for X.");
        AssertEquals(bSourceCalled.Y, 1, "The B source argument of OnMove must be 1 for Y.");
        AssertEquals(bDestinationCalled.X, 0, "The B destination argument of OnMove must be 0 for X.");
        AssertEquals(bDestinationCalled.Y, 2, "The B destination argument of OnMove must be 2 for Y.");

        AssertTrue(cCalled, "The C event OnMove must be called!");
        AssertEquals(cComponent.Position.X, 0, "The C component must not move on X.");
        AssertEquals(cComponent.Position.Y, 3, "The C component must move on Y.");
        AssertEquals(cSourceCalled.X, 0, "The C source argument of OnMove must be 0 for X.");
        AssertEquals(cSourceCalled.Y, 2, "The C source argument of OnMove must be 2 for Y.");
        AssertEquals(cDestinationCalled.X, 0, "The C destination argument of OnMove must be 0 for X.");
        AssertEquals(cDestinationCalled.Y, 3, "The C destination argument of OnMove must be 3 for Y.");
    }

    public void GivenComponentAWithComponentBUpWithComponentCUp_whenAMoveUpCMoveUpBMoveUp_thenTheyFireOnMove()
    {
        var aCalled = false;
        var aSourceCalled = new Point(-1, -1);
        var aDestinationCalled = new Point(-1, -1);
        var bCalled = false;
        var bSourceCalled = new Point(-1, -1);
        var bDestinationCalled = new Point(-1, -1);
        var cCalled = false;
        var cSourceCalled = new Point(-1, -1);
        var cDestinationCalled = new Point(-1, -1);
        var ground = new HashSet<Point> { new Point(0, 0), new Point(0, 1), new Point(0, 2), new Point(0, 3) };
        var system = new PositionSystem(ground);
        var aComponent = new PositionableComponent(system, new Point(0, 0));
        aComponent.OnMove = (source, destination) => { aCalled = true; aSourceCalled = source; aDestinationCalled = destination; };
        var bComponent = new PositionableComponent(system, new Point(0, 1));
        bComponent.OnMove = (source, destination) => { bCalled = true; bSourceCalled = source; bDestinationCalled = destination; };
        var cComponent = new PositionableComponent(system, new Point(0, 2));
        cComponent.OnMove = (source, destination) => { cCalled = true; cSourceCalled = source; cDestinationCalled = destination; };

        aComponent.Move(PositionSystem.UP);
        cComponent.Move(PositionSystem.UP);
        bComponent.Move(PositionSystem.UP);
        system.Process();

        AssertTrue(aCalled, "The A event OnMove must be called!");
        AssertEquals(aComponent.Position.X, 0, "The A component must not move on X.");
        AssertEquals(aComponent.Position.Y, 1, "The A component must move on Y.");
        AssertEquals(aSourceCalled.X, 0, "The A source argument of OnMove must be 0 for X.");
        AssertEquals(aSourceCalled.Y, 0, "The A source argument of OnMove must be 0 for Y.");
        AssertEquals(aDestinationCalled.X, 0, "The A destination argument of OnMove must be 0 for X.");
        AssertEquals(aDestinationCalled.Y, 1, "The A destination argument of OnMove must be 1 for Y.");

        AssertTrue(bCalled, "The B event OnMove must be called!");
        AssertEquals(bComponent.Position.X, 0, "The B component must not move on X.");
        AssertEquals(bComponent.Position.Y, 2, "The B component must move on Y.");
        AssertEquals(bSourceCalled.X, 0, "The B source argument of OnMove must be 0 for X.");
        AssertEquals(bSourceCalled.Y, 1, "The B source argument of OnMove must be 1 for Y.");
        AssertEquals(bDestinationCalled.X, 0, "The B destination argument of OnMove must be 0 for X.");
        AssertEquals(bDestinationCalled.Y, 2, "The B destination argument of OnMove must be 2 for Y.");

        AssertTrue(cCalled, "The C event OnMove must be called!");
        AssertEquals(cComponent.Position.X, 0, "The C component must not move on X.");
        AssertEquals(cComponent.Position.Y, 3, "The C component must move on Y.");
        AssertEquals(cSourceCalled.X, 0, "The C source argument of OnMove must be 0 for X.");
        AssertEquals(cSourceCalled.Y, 2, "The C source argument of OnMove must be 2 for Y.");
        AssertEquals(cDestinationCalled.X, 0, "The C destination argument of OnMove must be 0 for X.");
        AssertEquals(cDestinationCalled.Y, 3, "The C destination argument of OnMove must be 3 for Y.");
    }

    public void GivenComponentAWithComponentBUpWithComponentCUp_whenBMoveUpAMoveUpCMoveUp_thenTheyFireOnMove()
    {
        var aCalled = false;
        var aSourceCalled = new Point(-1, -1);
        var aDestinationCalled = new Point(-1, -1);
        var bCalled = false;
        var bSourceCalled = new Point(-1, -1);
        var bDestinationCalled = new Point(-1, -1);
        var cCalled = false;
        var cSourceCalled = new Point(-1, -1);
        var cDestinationCalled = new Point(-1, -1);
        var ground = new HashSet<Point> { new Point(0, 0), new Point(0, 1), new Point(0, 2), new Point(0, 3) };
        var system = new PositionSystem(ground);
        var aComponent = new PositionableComponent(system, new Point(0, 0));
        aComponent.OnMove = (source, destination) => { aCalled = true; aSourceCalled = source; aDestinationCalled = destination; };
        var bComponent = new PositionableComponent(system, new Point(0, 1));
        bComponent.OnMove = (source, destination) => { bCalled = true; bSourceCalled = source; bDestinationCalled = destination; };
        var cComponent = new PositionableComponent(system, new Point(0, 2));
        cComponent.OnMove = (source, destination) => { cCalled = true; cSourceCalled = source; cDestinationCalled = destination; };

        bComponent.Move(PositionSystem.UP);
        aComponent.Move(PositionSystem.UP);
        cComponent.Move(PositionSystem.UP);
        system.Process();

        AssertTrue(aCalled, "The A event OnMove must be called!");
        AssertEquals(aComponent.Position.X, 0, "The A component must not move on X.");
        AssertEquals(aComponent.Position.Y, 1, "The A component must move on Y.");
        AssertEquals(aSourceCalled.X, 0, "The A source argument of OnMove must be 0 for X.");
        AssertEquals(aSourceCalled.Y, 0, "The A source argument of OnMove must be 0 for Y.");
        AssertEquals(aDestinationCalled.X, 0, "The A destination argument of OnMove must be 0 for X.");
        AssertEquals(aDestinationCalled.Y, 1, "The A destination argument of OnMove must be 1 for Y.");

        AssertTrue(bCalled, "The B event OnMove must be called!");
        AssertEquals(bComponent.Position.X, 0, "The B component must not move on X.");
        AssertEquals(bComponent.Position.Y, 2, "The B component must move on Y.");
        AssertEquals(bSourceCalled.X, 0, "The B source argument of OnMove must be 0 for X.");
        AssertEquals(bSourceCalled.Y, 1, "The B source argument of OnMove must be 1 for Y.");
        AssertEquals(bDestinationCalled.X, 0, "The B destination argument of OnMove must be 0 for X.");
        AssertEquals(bDestinationCalled.Y, 2, "The B destination argument of OnMove must be 2 for Y.");

        AssertTrue(cCalled, "The C event OnMove must be called!");
        AssertEquals(cComponent.Position.X, 0, "The C component must not move on X.");
        AssertEquals(cComponent.Position.Y, 3, "The C component must move on Y.");
        AssertEquals(cSourceCalled.X, 0, "The C source argument of OnMove must be 0 for X.");
        AssertEquals(cSourceCalled.Y, 2, "The C source argument of OnMove must be 2 for Y.");
        AssertEquals(cDestinationCalled.X, 0, "The C destination argument of OnMove must be 0 for X.");
        AssertEquals(cDestinationCalled.Y, 3, "The C destination argument of OnMove must be 3 for Y.");
    }

    public void GivenComponentAWithComponentBUpWithComponentCUp_whenBMoveUpCMoveUpAMoveUp_thenTheyFireOnMove()
    {
        var aCalled = false;
        var aSourceCalled = new Point(-1, -1);
        var aDestinationCalled = new Point(-1, -1);
        var bCalled = false;
        var bSourceCalled = new Point(-1, -1);
        var bDestinationCalled = new Point(-1, -1);
        var cCalled = false;
        var cSourceCalled = new Point(-1, -1);
        var cDestinationCalled = new Point(-1, -1);
        var ground = new HashSet<Point> { new Point(0, 0), new Point(0, 1), new Point(0, 2), new Point(0, 3) };
        var system = new PositionSystem(ground);
        var aComponent = new PositionableComponent(system, new Point(0, 0));
        aComponent.OnMove = (source, destination) => { aCalled = true; aSourceCalled = source; aDestinationCalled = destination; };
        var bComponent = new PositionableComponent(system, new Point(0, 1));
        bComponent.OnMove = (source, destination) => { bCalled = true; bSourceCalled = source; bDestinationCalled = destination; };
        var cComponent = new PositionableComponent(system, new Point(0, 2));
        cComponent.OnMove = (source, destination) => { cCalled = true; cSourceCalled = source; cDestinationCalled = destination; };

        bComponent.Move(PositionSystem.UP);
        cComponent.Move(PositionSystem.UP);
        aComponent.Move(PositionSystem.UP);
        system.Process();

        AssertTrue(aCalled, "The A event OnMove must be called!");
        AssertEquals(aComponent.Position.X, 0, "The A component must not move on X.");
        AssertEquals(aComponent.Position.Y, 1, "The A component must move on Y.");
        AssertEquals(aSourceCalled.X, 0, "The A source argument of OnMove must be 0 for X.");
        AssertEquals(aSourceCalled.Y, 0, "The A source argument of OnMove must be 0 for Y.");
        AssertEquals(aDestinationCalled.X, 0, "The A destination argument of OnMove must be 0 for X.");
        AssertEquals(aDestinationCalled.Y, 1, "The A destination argument of OnMove must be 1 for Y.");

        AssertTrue(bCalled, "The B event OnMove must be called!");
        AssertEquals(bComponent.Position.X, 0, "The B component must not move on X.");
        AssertEquals(bComponent.Position.Y, 2, "The B component must move on Y.");
        AssertEquals(bSourceCalled.X, 0, "The B source argument of OnMove must be 0 for X.");
        AssertEquals(bSourceCalled.Y, 1, "The B source argument of OnMove must be 1 for Y.");
        AssertEquals(bDestinationCalled.X, 0, "The B destination argument of OnMove must be 0 for X.");
        AssertEquals(bDestinationCalled.Y, 2, "The B destination argument of OnMove must be 2 for Y.");

        AssertTrue(cCalled, "The C event OnMove must be called!");
        AssertEquals(cComponent.Position.X, 0, "The C component must not move on X.");
        AssertEquals(cComponent.Position.Y, 3, "The C component must move on Y.");
        AssertEquals(cSourceCalled.X, 0, "The C source argument of OnMove must be 0 for X.");
        AssertEquals(cSourceCalled.Y, 2, "The C source argument of OnMove must be 2 for Y.");
        AssertEquals(cDestinationCalled.X, 0, "The C destination argument of OnMove must be 0 for X.");
        AssertEquals(cDestinationCalled.Y, 3, "The C destination argument of OnMove must be 3 for Y.");
    }

    public void GivenComponentAWithComponentBUpWithComponentCUp_whenCMoveUpAMoveUpBMoveUp_thenTheyFireOnMove()
    {
        var aCalled = false;
        var aSourceCalled = new Point(-1, -1);
        var aDestinationCalled = new Point(-1, -1);
        var bCalled = false;
        var bSourceCalled = new Point(-1, -1);
        var bDestinationCalled = new Point(-1, -1);
        var cCalled = false;
        var cSourceCalled = new Point(-1, -1);
        var cDestinationCalled = new Point(-1, -1);
        var ground = new HashSet<Point> { new Point(0, 0), new Point(0, 1), new Point(0, 2), new Point(0, 3) };
        var system = new PositionSystem(ground);
        var aComponent = new PositionableComponent(system, new Point(0, 0));
        aComponent.OnMove = (source, destination) => { aCalled = true; aSourceCalled = source; aDestinationCalled = destination; };
        var bComponent = new PositionableComponent(system, new Point(0, 1));
        bComponent.OnMove = (source, destination) => { bCalled = true; bSourceCalled = source; bDestinationCalled = destination; };
        var cComponent = new PositionableComponent(system, new Point(0, 2));
        cComponent.OnMove = (source, destination) => { cCalled = true; cSourceCalled = source; cDestinationCalled = destination; };

        cComponent.Move(PositionSystem.UP);
        aComponent.Move(PositionSystem.UP);
        bComponent.Move(PositionSystem.UP);
        system.Process();

        AssertTrue(aCalled, "The A event OnMove must be called!");
        AssertEquals(aComponent.Position.X, 0, "The A component must not move on X.");
        AssertEquals(aComponent.Position.Y, 1, "The A component must move on Y.");
        AssertEquals(aSourceCalled.X, 0, "The A source argument of OnMove must be 0 for X.");
        AssertEquals(aSourceCalled.Y, 0, "The A source argument of OnMove must be 0 for Y.");
        AssertEquals(aDestinationCalled.X, 0, "The A destination argument of OnMove must be 0 for X.");
        AssertEquals(aDestinationCalled.Y, 1, "The A destination argument of OnMove must be 1 for Y.");

        AssertTrue(bCalled, "The B event OnMove must be called!");
        AssertEquals(bComponent.Position.X, 0, "The B component must not move on X.");
        AssertEquals(bComponent.Position.Y, 2, "The B component must move on Y.");
        AssertEquals(bSourceCalled.X, 0, "The B source argument of OnMove must be 0 for X.");
        AssertEquals(bSourceCalled.Y, 1, "The B source argument of OnMove must be 1 for Y.");
        AssertEquals(bDestinationCalled.X, 0, "The B destination argument of OnMove must be 0 for X.");
        AssertEquals(bDestinationCalled.Y, 2, "The B destination argument of OnMove must be 2 for Y.");

        AssertTrue(cCalled, "The C event OnMove must be called!");
        AssertEquals(cComponent.Position.X, 0, "The C component must not move on X.");
        AssertEquals(cComponent.Position.Y, 3, "The C component must move on Y.");
        AssertEquals(cSourceCalled.X, 0, "The C source argument of OnMove must be 0 for X.");
        AssertEquals(cSourceCalled.Y, 2, "The C source argument of OnMove must be 2 for Y.");
        AssertEquals(cDestinationCalled.X, 0, "The C destination argument of OnMove must be 0 for X.");
        AssertEquals(cDestinationCalled.Y, 3, "The C destination argument of OnMove must be 3 for Y.");
    }

    public void GivenComponentAWithComponentBUpWithComponentCUp_whenCMoveUpBMoveUpCMoveUp_thenTheyFireOnMove()
    {
        var aCalled = false;
        var aSourceCalled = new Point(-1, -1);
        var aDestinationCalled = new Point(-1, -1);
        var bCalled = false;
        var bSourceCalled = new Point(-1, -1);
        var bDestinationCalled = new Point(-1, -1);
        var cCalled = false;
        var cSourceCalled = new Point(-1, -1);
        var cDestinationCalled = new Point(-1, -1);
        var ground = new HashSet<Point> { new Point(0, 0), new Point(0, 1), new Point(0, 2), new Point(0, 3) };
        var system = new PositionSystem(ground);
        var aComponent = new PositionableComponent(system, new Point(0, 0));
        aComponent.OnMove = (source, destination) => { aCalled = true; aSourceCalled = source; aDestinationCalled = destination; };
        var bComponent = new PositionableComponent(system, new Point(0, 1));
        bComponent.OnMove = (source, destination) => { bCalled = true; bSourceCalled = source; bDestinationCalled = destination; };
        var cComponent = new PositionableComponent(system, new Point(0, 2));
        cComponent.OnMove = (source, destination) => { cCalled = true; cSourceCalled = source; cDestinationCalled = destination; };

        cComponent.Move(PositionSystem.UP);
        bComponent.Move(PositionSystem.UP);
        aComponent.Move(PositionSystem.UP);
        system.Process();

        AssertTrue(aCalled, "The A event OnMove must be called!");
        AssertEquals(aComponent.Position.X, 0, "The A component must not move on X.");
        AssertEquals(aComponent.Position.Y, 1, "The A component must move on Y.");
        AssertEquals(aSourceCalled.X, 0, "The A source argument of OnMove must be 0 for X.");
        AssertEquals(aSourceCalled.Y, 0, "The A source argument of OnMove must be 0 for Y.");
        AssertEquals(aDestinationCalled.X, 0, "The A destination argument of OnMove must be 0 for X.");
        AssertEquals(aDestinationCalled.Y, 1, "The A destination argument of OnMove must be 1 for Y.");

        AssertTrue(bCalled, "The B event OnMove must be called!");
        AssertEquals(bComponent.Position.X, 0, "The B component must not move on X.");
        AssertEquals(bComponent.Position.Y, 2, "The B component must move on Y.");
        AssertEquals(bSourceCalled.X, 0, "The B source argument of OnMove must be 0 for X.");
        AssertEquals(bSourceCalled.Y, 1, "The B source argument of OnMove must be 1 for Y.");
        AssertEquals(bDestinationCalled.X, 0, "The B destination argument of OnMove must be 0 for X.");
        AssertEquals(bDestinationCalled.Y, 2, "The B destination argument of OnMove must be 2 for Y.");

        AssertTrue(cCalled, "The C event OnMove must be called!");
        AssertEquals(cComponent.Position.X, 0, "The C component must not move on X.");
        AssertEquals(cComponent.Position.Y, 3, "The C component must move on Y.");
        AssertEquals(cSourceCalled.X, 0, "The C source argument of OnMove must be 0 for X.");
        AssertEquals(cSourceCalled.Y, 2, "The C source argument of OnMove must be 2 for Y.");
        AssertEquals(cDestinationCalled.X, 0, "The C destination argument of OnMove must be 0 for X.");
        AssertEquals(cDestinationCalled.Y, 3, "The C destination argument of OnMove must be 3 for Y.");
    }

    public void GivenStandComponentA_whenComponentBMoveUpToA_thenTheyFireCollision()
    {
        var aCalled = false;
        PositionableComponent? aArgument = null;
        var bCalled = false;
        PositionableComponent? bArgument = null;
        var ground = new HashSet<Point> { new Point(0, 0), new Point(0, 1) };
        var system = new PositionSystem(ground);
        var aComponent = new PositionableComponent(system, new Point(0, 1));
        aComponent.OnCollision = (other) => { aCalled = true; aArgument = other; };
        var bComponent = new PositionableComponent(system, new Point(0, 0));
        bComponent.OnCollision = (other) => { bCalled = true; bArgument = other; };

        bComponent.Move(PositionSystem.UP);
        system.Process();

        AssertTrue(aCalled, "The A event OnCollision must be called!");
        AssertEquals(aComponent.Position.X, 0, "The A component must not move on X.");
        AssertEquals(aComponent.Position.Y, 1, "The A component must not move on Y.");
        AssertEquals(aArgument, bComponent, "The A argument of OnCollision must be B.");

        AssertTrue(bCalled, "The A event OnCollision must be called!");
        AssertEquals(bComponent.Position.X, 0, "The A component must not move on X.");
        AssertEquals(bComponent.Position.Y, 0, "The A component must not move on Y.");
        AssertEquals(bArgument, aComponent, "The A argument of OnCollision must be A.");
    }

    public void GivenComponentAWithComponentB_whenAMoveUpAndBTowardsA_thenTheyNotMove()
    {
        var aCalled = false;
        PositionableComponent? aArgument = null;
        var bCalled = false;
        PositionableComponent? bArgument = null;
        var ground = new HashSet<Point> { new Point(0, 0), new Point(0, 1) };
        var system = new PositionSystem(ground);
        var aComponent = new PositionableComponent(system, new Point(0, 0));
        aComponent.OnCollision = (other) => { aCalled = true; aArgument = other; };
        var bComponent = new PositionableComponent(system, new Point(0, 1));
        bComponent.OnCollision = (other) => { bCalled = true; bArgument = other; };

        aComponent.Move(PositionSystem.UP);
        bComponent.Move(PositionSystem.UP.Opposite);
        system.Process();

        AssertTrue(aCalled, "The A event OnCollision must be called!");
        AssertEquals(aComponent.Position.X, 0, "The A component must not move on X.");
        AssertEquals(aComponent.Position.Y, 0, "The A component must not move on Y.");
        AssertEquals(aArgument, bComponent, "The A argument of OnCollision must be B.");

        AssertTrue(bCalled, "The B event OnCollision must be called!");
        AssertEquals(bComponent.Position.X, 0, "The B component must not move on X.");
        AssertEquals(bComponent.Position.Y, 1, "The B component must not move on Y.");
        AssertEquals(bArgument, aComponent, "The B argument of OnCollision must be A.");
    }

    public void GivenComponentAWithComponentBTwoUp_whenAMoveUpAndBTowardsA_thenOnlyAMove()
    {
        var aCalled = false;
        var aSource = new Point(-1, -1);
        var aDestination = new Point(-1, -1);
        var bCalled = false;
        PositionableComponent? bArgument = null;
        var ground = new HashSet<Point> { new Point(0, 0), new Point(0, 1), new Point(0, 2) };
        var system = new PositionSystem(ground);
        var aComponent = new PositionableComponent(system, new Point(0, 0));
        aComponent.OnMove = (source, destination) => { aCalled = true; aSource = source; aDestination = destination; };
        var bComponent = new PositionableComponent(system, new Point(0, 2));
        bComponent.OnCollision = (other) => { bCalled = true; bArgument = other; };

        aComponent.Move(PositionSystem.UP);
        bComponent.Move(PositionSystem.UP.Opposite);
        system.Process();

        AssertTrue(aCalled, "The A event OnMove must be called!");
        AssertEquals(aComponent.Position.X, 0, "The A component must not move on X.");
        AssertEquals(aComponent.Position.Y, 1, "The A component must move on Y.");
        AssertEquals(aSource.X, 0, "The A argument source X must be 0.");
        AssertEquals(aSource.Y, 0, "The A argument source Y must be 0.");
        AssertEquals(aDestination.X, 0, "The A argument destination X must be 0.");
        AssertEquals(aDestination.Y, 1, "The A argument destination Y must be 1.");

        AssertTrue(bCalled, "The B event OnCollision must be called!");
        AssertEquals(bComponent.Position.X, 0, "The A component must not move on X.");
        AssertEquals(bComponent.Position.Y, 2, "The A component must not move on Y.");
        AssertEquals(bArgument, aComponent, "The B argument of OnCollision must be A.");
    }

    public void GivenComponentAWithComponentBTwoUp_whenBMoveUpAndATowardsB_thenOnlyBMove()
    {
        var aCalled = false;
        PositionableComponent? aArgument = null;
        var bCalled = false;
        var bSource = new Point(-1, -1);
        var bDestination = new Point(-1, -1);
        var ground = new HashSet<Point> { new Point(0, 0), new Point(0, 1), new Point(0, 2) };
        var system = new PositionSystem(ground);
        var aComponent = new PositionableComponent(system, new Point(0, 0));
        aComponent.OnCollision = (other) => { aCalled = true; aArgument = other; };
        var bComponent = new PositionableComponent(system, new Point(0, 2));
        bComponent.OnMove = (source, destination) => { bCalled = true; bSource = source; bDestination = destination; };

        bComponent.Move(PositionSystem.UP.Opposite);
        aComponent.Move(PositionSystem.UP);
        system.Process();

        AssertTrue(bCalled, "The B event OnMove must be called!");
        AssertEquals(bComponent.Position.X, 0, "The B component must not move on X.");
        AssertEquals(bComponent.Position.Y, 1, "The B component must move on Y.");
        AssertEquals(bSource.X, 0, "The B argument source X must be 0.");
        AssertEquals(bSource.Y, 2, "The B argument source Y must be 2.");
        AssertEquals(bDestination.X, 0, "The B argument destination X must be 0.");
        AssertEquals(bDestination.Y, 1, "The B argument destination Y must be 1.");

        AssertTrue(aCalled, "The A event OnCollision must be called!");
        AssertEquals(aComponent.Position.X, 0, "The A component must not move on X.");
        AssertEquals(aComponent.Position.Y, 0, "The A component must not move on Y.");
        AssertEquals(aArgument, bComponent, "The A argument of OnCollision must be B.");
    }
}
