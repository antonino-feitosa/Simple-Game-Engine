
using static SimpleGameEngine.Test.TestRunner;

namespace SimpleGameEngine.Test;

[TestClass]
public class InterfaceSystemTest
{
    public void GivenInterfaciable_whenMouseDownAtBounds_thenFireMouseDown()
    {
        var actions = new List<Action<Point>>();
        var system = new InterfaceSystem();
        var device = new IDeviceStub()
        {
            OnRegisterMouseDown = (button, command) => actions.Add(command)
        };
        var bounds = new Bounds(new Point(10, 10), new Dimension(10, 10));
        var component = new InterfaciableComponent(system, bounds);
        var calledMouseDown = false;
        var calledMouseUp = false;
        var calledReset = false;
        component.OnMouseDown += () => calledMouseDown = true;
        component.OnMouseUp += () => calledMouseUp = true;
        component.OnReset += () => calledReset = true;

        system.Start(device);
        var point = new Point(15, 15);
        foreach (var act in actions) act.Invoke(point);
        system.Process();

        AssertTrue(calledMouseDown, "The mouse down must be called!");
        AssertFalse(calledMouseUp, "The mouse up must not be called!");
        AssertFalse(calledReset, "The reset must not be called!");
    }

    public void GivenInterfaciable_whenMouseDownOuOfBounds_thenDoNotFireMouseDown()
    {
        var actions = new List<Action<Point>>();
        var system = new InterfaceSystem();
        var device = new IDeviceStub()
        {
            OnRegisterMouseDown = (button, command) => actions.Add(command)
        };
        var bounds = new Bounds(new Point(10, 10), new Dimension(10, 10));
        var component = new InterfaciableComponent(system, bounds);
        var calledMouseDown = false;
        var calledMouseUp = false;
        var calledReset = false;
        component.OnMouseDown += () => calledMouseDown = true;
        component.OnMouseUp += () => calledMouseUp = true;
        component.OnReset += () => calledReset = true;

        system.Start(device);
        var point = new Point(5, 5);
        foreach (var act in actions) act.Invoke(point);
        system.Process();

        AssertFalse(calledMouseDown, "The mouse down must not be called!");
        AssertFalse(calledMouseUp, "The mouse up must not be called!");
        AssertFalse(calledReset, "The reset must not be called!");
    }

    public void GivenInterfaciableWithFiredMouseDown_whenMouseUpAtBounds_thenFireMouseUp()
    {
        var actionsMouseDown = new List<Action<Point>>();
        var actionsMouseUp = new List<Action<Point>>();
        var system = new InterfaceSystem();
        var device = new IDeviceStub()
        {
            OnRegisterMouseDown = (Mouse, command) => actionsMouseDown.Add(command),
            OnRegisterMouseUp = (Mouse, command) => actionsMouseUp.Add(command)
        };
        var bounds = new Bounds(new Point(10, 10), new Dimension(10, 10));
        var component = new InterfaciableComponent(system, bounds);
        var calledMouseDown = false;
        var calledMouseUp = false;
        var calledReset = false;
        component.OnMouseDown += () => calledMouseDown = true;
        component.OnMouseUp += () => calledMouseUp = true;
        component.OnReset += () => calledReset = true;

        system.Start(device);
        var point = new Point(15, 15);
        foreach (var act in actionsMouseDown) act.Invoke(point);
        foreach (var act in actionsMouseUp) act.Invoke(point);
        system.Process();

        AssertTrue(calledMouseDown, "The mouse down must be called!");
        AssertTrue(calledMouseUp, "The mouse up must be called!");
        AssertFalse(calledReset, "The reset must not be called!");
    }

    public void GivenInterfaciableWithFiredMouseDown_whenMouseUpOutOfBounds_thenFireReset()
    {
        var actionsMouseDown = new List<Action<Point>>();
        var actionsMouseUp = new List<Action<Point>>();
        var system = new InterfaceSystem();
        var device = new IDeviceStub()
        {
            OnRegisterMouseDown = (Mouse, command) => actionsMouseDown.Add(command),
            OnRegisterMouseUp = (Mouse, command) => actionsMouseUp.Add(command)
        };
        var bounds = new Bounds(new Point(10, 10), new Dimension(10, 10));
        var component = new InterfaciableComponent(system, bounds);
        var calledMouseDown = false;
        var calledMouseUp = false;
        var calledReset = false;
        component.OnMouseDown += () => calledMouseDown = true;
        component.OnMouseUp += () => calledMouseUp = true;
        component.OnReset += () => calledReset = true;

        system.Start(device);
        var point = new Point(15, 15);
        foreach (var act in actionsMouseDown) act.Invoke(point);
        var outPoint = new Point(5, 5);
        foreach (var act in actionsMouseUp) act.Invoke(outPoint);
        system.Process();

        AssertTrue(calledMouseDown, "The mouse down must be called!");
        AssertFalse(calledMouseUp, "The mouse up must not be called!");
        AssertTrue(calledReset, "The reset must be called!");
    }

    public void GivenInterfaciableAfterFireUp_whenMouseDownAtBounds_thenFireMouseDown()
    {
        var actionsMouseDown = new List<Action<Point>>();
        var actionsMouseUp = new List<Action<Point>>();
        var system = new InterfaceSystem();
        var device = new IDeviceStub()
        {
            OnRegisterMouseDown = (Mouse, command) => actionsMouseDown.Add(command),
            OnRegisterMouseUp = (Mouse, command) => actionsMouseUp.Add(command)
        };
        var bounds = new Bounds(new Point(10, 10), new Dimension(10, 10));
        var component = new InterfaciableComponent(system, bounds);
        var called = new List<int>();
        component.OnMouseDown += () => called.Add(0);
        component.OnMouseUp += () => called.Add(1);
        component.OnReset += () => called.Add(2);

        system.Start(device);
        var point = new Point(15, 15);
        foreach (var act in actionsMouseDown) act.Invoke(point);
        foreach (var act in actionsMouseUp) act.Invoke(point);
        system.Process();
        foreach (var act in actionsMouseDown) act.Invoke(point);
        system.Process();

        AssertEquals(called.Count, 3, "Three events must be called!");
        AssertEquals(called[0], 0, "The mouse down must be the first call!");
        AssertEquals(called[1], 1, "The mouse up must be the second call!");
        AssertEquals(called[2], 0, "The mouse down must be the third call!");
    }

    public void GivenInterfaciableWithNotFiredMouseDown_whenMouseUpAtBounds_thenDoNotFireEvents()
    {
        var actionsMouseDown = new List<Action<Point>>();
        var actionsMouseUp = new List<Action<Point>>();
        var system = new InterfaceSystem();
        var device = new IDeviceStub()
        {
            OnRegisterMouseDown = (Mouse, command) => actionsMouseDown.Add(command),
            OnRegisterMouseUp = (Mouse, command) => actionsMouseUp.Add(command)
        };
        var bounds = new Bounds(new Point(10, 10), new Dimension(10, 10));
        var component = new InterfaciableComponent(system, bounds);
        var calledMouseDown = false;
        var calledMouseUp = false;
        var calledReset = false;
        component.OnMouseDown += () => calledMouseDown = true;
        component.OnMouseUp += () => calledMouseUp = true;
        component.OnReset += () => calledReset = true;

        system.Start(device);
        foreach (var act in actionsMouseDown) act.Invoke(new Point(5, 5));
        foreach (var act in actionsMouseUp) act.Invoke(new Point(15, 15));
        system.Process();

        AssertFalse(calledMouseDown, "The mouse down must not be called!");
        AssertFalse(calledMouseUp, "The mouse up must not be called!");
        AssertFalse(calledReset, "The reset must not be called!");
    }
}
