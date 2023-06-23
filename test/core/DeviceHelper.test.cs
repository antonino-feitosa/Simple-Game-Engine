
using static SimpleGameEngine.Test.TestRunner;

namespace SimpleGameEngine.Test;

[TestClass]
public class DeviceHelperTest
{

    // The frames per second must be positive
    public void GivenDeviceHelperWhenFramesPerSecondIsSetToNegativeThenThrowsArgumentOutOfRangeException()
    {
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy) { FramesPerSecond = 32 };
        var capturedException = false;

        try
        {
            device.FramesPerSecond = -1;
        }
        catch (ArgumentOutOfRangeException)
        {
            capturedException = true;
        }

        Assert(capturedException == true);
    }

    public void GivenDeviceHelper_whenFramesPerSecondIsSetToZero_thenThrowsArgumentOutOfRangeException()
    {
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy) { FramesPerSecond = 32 };
        var capturedException = false;

        try
        {
            device.FramesPerSecond = 0;
        }
        catch (ArgumentOutOfRangeException)
        {
            capturedException = true;
        }

        Assert(capturedException == true);
    }

    public void GivenDeviceHelper_whenFramesPerSecondSetThrowsArgumentOutOfRangeException_thenFramesPerSecondNotChange()
    {
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy) { FramesPerSecond = 32 };

        try
        {
            device.FramesPerSecond = 0;
        }
        catch (ArgumentOutOfRangeException) { }
        var frames = device.FramesPerSecond;

        Assert(frames == 32);
    }

    public void GivenDeviceHelper_whenFramesPerSecondIsSetTo64_thenFramesPerSecondIs64()
    {
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy) { FramesPerSecond = 64 };

        device.FramesPerSecond = 64;
        var frames = device.FramesPerSecond;

        Assert(frames == 64);
    }

    // The dimension get must not modify the instance

    public void GivenDimension800x600_whenGetDimensionWidthIsChanged_thenDeviceDimensionWidthIsNotChanged()
    {
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy) { Dimension = new Dimension(800, 600) };

        device.Dimension.Width = 100;
        var width = device.Dimension.Width;

        Assert(width == 800);
    }

    public void GivenDimension800x600_whenGetDimensionWidthIsChanged_thenDeviceDimensionHeightIsNotChanged()
    {
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy) { Dimension = new Dimension(800, 600) };

        device.Dimension.Width = 100;
        var height = device.Dimension.Height;

        Assert(height == 600);
    }

    public void GivenDimension800x600_whenGetDimensionHeightIsChanged_thenDeviceDimensionHeightIsNotChanged()
    {
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy) { Dimension = new Dimension(800, 600) };

        device.Dimension.Height = 100;
        var height = device.Dimension.Height;

        Assert(height == 600);
    }

    public void GivenDimension800x600_whenGetDimensionHeightIsChanged_thenDeviceDimensionWidthIsNotChanged()
    {
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy) { Dimension = new Dimension(800, 600) };

        device.Dimension.Height = 100;
        var width = device.Dimension.Width;

        Assert(width == 800);
    }

    public void GivenDeviceHelper_whenSetDimensionAandAWidthIsChanged_thenDeviceDimensionWidthIsNotChanged()
    {
        var dimension = new Dimension(800, 600);
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);

        device.Dimension = dimension;
        dimension.Width = 100;
        var width = device.Dimension.Width;

        Assert(width == 800);
    }

    public void GivenDeviceHelper_whenSetDimensionAandAWidthIsChanged_thenDeviceDimensionHeightIsNotChanged()
    {
        var dimension = new Dimension(800, 600);
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);

        device.Dimension = dimension;
        dimension.Width = 100;
        var height = device.Dimension.Height;

        Assert(height == 600);
    }

    public void GivenDeviceHelper_whenSetDimensionAandAHeightIsChanged_thenDeviceDimensionHeightIsNotChanged()
    {
        var dimension = new Dimension(800, 600);
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);

        device.Dimension = dimension;
        dimension.Height = 100;
        var height = device.Dimension.Height;

        Assert(height == 600);
    }

    public void GivenDeviceHelper_whenSetDimensionAandAHeightIsChanged_thenDeviceDimensionWidthIsNotChanged()
    {
        var dimension = new Dimension(800, 600);
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);

        device.Dimension = dimension;
        dimension.Height = 100;
        var width = device.Dimension.Width;

        Assert(width == 800);
    }

    // The mouse position get must not modify the instance

    public void GivenMousePosition10x20_whenGetMousePositionXIsChanged_thenDeviceMousePositionXIsNotChanged()
    {
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy) { MousePosition = new Point(10, 20) };

        device.MousePosition.X = 0;
        var x = device.MousePosition.X;

        Assert(x == 10);
    }

    public void GivenMousePosition10x20_whenGetMousePositionXIsChanged_thenDeviceMousePositionYIsNotChanged()
    {
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy) { MousePosition = new Point(10, 20) };

        device.MousePosition.X = 0;
        var y = device.MousePosition.Y;

        Assert(y == 20);
    }

    public void GivenMousePosition10x20_whenGetMousePositionYIsChanged_thenDeviceMousePositionYIsNotChanged()
    {
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy) { MousePosition = new Point(10, 20) };

        device.MousePosition.Y = 0;
        var y = device.MousePosition.Y;

        Assert(y == 20);
    }

    public void GivenMousePosition10x20_whenGetMousePositionYIsChanged_thenDeviceMousePositionXIsNotChanged()
    {
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy) { MousePosition = new Point(10, 20) };

        device.MousePosition.Y = 0;
        var x = device.MousePosition.X;

        Assert(x == 10);
    }

    public void GivenDeviceHelper_whenSetMousePositionAandAXIsChanged_thenDeviceMousePositionXIsNotChanged()
    {
        var point = new Point(10, 20);
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);

        device.MousePosition = point;
        point.X = 0;
        var x = device.MousePosition.X;

        Assert(x == 10);
    }

    public void GivenDeviceHelper_whenSetMousePositionAandAXIsChanged_thenDeviceMousePositionYIsNotChanged()
    {
        var point = new Point(10, 20);
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);

        device.MousePosition = point;
        point.X = 0;
        var y = device.MousePosition.Y;

        Assert(y == 20);
    }

    public void GivenDeviceHelper_whenSetMousePositionAandAYIsChanged_thenDeviceMousePositionYIsNotChanged()
    {
        var point = new Point(10, 20);
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);

        device.MousePosition = point;
        point.Y = 0;
        var y = device.MousePosition.Y;

        Assert(y == 20);
    }

    public void GivenDeviceHelper_whenSetMousePositionAandAYIsChanged_thenDeviceMousePositionXIsNotChanged()
    {
        var point = new Point(10, 20);
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);

        device.MousePosition = point;
        point.Y = 0;
        var x = device.MousePosition.X;

        Assert(x == 10);
    }


    // The loader must not be called two times to the same resource
    public void GivenNoResourceLoaded_whenValidResourceIsLoaded_thenLoaderIsCalledOnce()
    {
        var numOfCalls = 0;
        var loader = (string path) => { numOfCalls++; return new IResourceStub(); };
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);

        device.LoadResource<IResourceStub>("", loader);

        Assert(numOfCalls == 1);
    }

    public void GivenResourceLoaded_whenValidResourceIsLoaded_thenLoaderIsNotCalled()
    {
        var numOfCalls = 0;
        var loader = (string path) => { numOfCalls++; return new IResourceStub(); };
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);

        device.LoadResource<IResourceStub>("", loader);
        device.LoadResource<IResourceStub>("", loader);

        Assert(numOfCalls == 1);
    }

    public void GivenResourceXLoaded_whenValidResourceYIsLoaded_thenLoaderIsCalledWithY()
    {
        var calledPath = "";
        var loader = (string path) => { calledPath = path; return new IResourceStub(); };
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);

        device.LoadResource<IResourceStub>("X", loader);
        device.LoadResource<IResourceStub>("Y", loader);

        Assert(calledPath == "Y");
    }

    public void GivenNoResourceLoaded_whenInvalidResourceIsLoaded_thenResourceNotFoundExceptionIsThrowed()
    {
        var loader = (string path) => { if (path == "valid") return new IResourceStub(); else throw new ResourceNotFoundException(); };
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        var capturedException = false;

        try
        {
            device.LoadResource<IResourceStub>("invalid", loader);
        }
        catch (ResourceNotFoundException)
        {
            capturedException = true;
        }

        Assert(capturedException == true);
    }

    // The registers must capture only the selected element
    //keyup ctrl, alt, shift

    public void GivenRegisteredCommandXKeyUpShiftA_whenFireKeyUpShiftA_thenCommandXIsCalledOnceWithShift()
    {
        var calledWithShift = false;
        void command(KeyboardModifier mod) => calledWithShift = mod.HasFlag(KeyboardModifier.Shift);
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterKeyUp((int)'a', command);

        device.FireKeyUp((int)'a', KeyboardModifier.Shift);

        Assert(calledWithShift == true);
    }

    public void GivenRegisteredCommandXKeyUpShiftCtrlA_whenFireKeyUpShiftCtrlA_thenCommandXIsCalledOnceWithShiftCtrl()
    {
        var shiftCtrl = KeyboardModifier.Shift | KeyboardModifier.Ctrl;
        var calledWithShift = false;
        void command(KeyboardModifier mod) => calledWithShift = mod.HasFlag(shiftCtrl);
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterKeyUp((int)'a', command);

        device.FireKeyUp((int)'a', shiftCtrl);

        Assert(calledWithShift == true);
    }

    public void GivenRegisteredCommandXKeyUpShiftCtrlAltA_whenFireKeyUpShiftCtrlAltA_thenCommandXIsCalledOnceWithShiftCtrlAlt()
    {
        var shiftCtrlAlt = KeyboardModifier.Shift | KeyboardModifier.Ctrl | KeyboardModifier.Alt;
        var calledWithShift = false;
        void command(KeyboardModifier mod) => calledWithShift = mod.HasFlag(shiftCtrlAlt);
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterKeyUp((int)'a', command);

        device.FireKeyUp((int)'a', shiftCtrlAlt);

        Assert(calledWithShift == true);
    }

    public void GivenRegisteredCommandXKeyUpCtrlA_whenFireKeyUpCtrlA_thenCommandXIsCalledOnceWithCtrl()
    {
        var calledWithCtrl = false;
        void command(KeyboardModifier mod) => calledWithCtrl = mod.HasFlag(KeyboardModifier.Ctrl);
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterKeyUp((int)'a', command);

        device.FireKeyUp((int)'a', KeyboardModifier.Ctrl);

        Assert(calledWithCtrl == true);
    }

    public void GivenRegisteredCommandXKeyUpCtrlAltA_whenFireKeyUpCtrlAltA_thenCommandXIsCalledOnceWithCtrlAlt()
    {
        var shiftCtrlAlt = KeyboardModifier.Ctrl | KeyboardModifier.Alt;
        var calledWithCtrl = false;
        void command(KeyboardModifier mod) => calledWithCtrl = mod.HasFlag(shiftCtrlAlt);
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterKeyUp((int)'a', command);

        device.FireKeyUp((int)'a', shiftCtrlAlt);

        Assert(calledWithCtrl == true);
    }

    public void GivenRegisteredCommandXKeyUpAltA_whenFireKeyUpAltA_thenCommandXIsCalledOnceWithAlt()
    {
        var calledWithAlt = false;
        void command(KeyboardModifier mod) => calledWithAlt = mod.HasFlag(KeyboardModifier.Alt);
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterKeyUp((int)'a', command);

        device.FireKeyUp((int)'a', KeyboardModifier.Alt);

        Assert(calledWithAlt == true);
    }

    // key up commands

    public void GivenRegisteredCommandXKeyUpA_whenFireKeyUpA_thenCommandXIsCalledOnce()
    {
        var called = false;
        void command(KeyboardModifier mod) => called = true;
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterKeyUp((int)'a', command);

        device.FireKeyUp((int)'a', KeyboardModifier.None);

        Assert(called == true);
    }

    public void GivenRegisteredTwoCommandsToKeyUpA_whenFireKeyUpA_thenCommandXIsCalledTwice()
    {
        var numOfCalls = 0;
        void first_command(KeyboardModifier mod) => numOfCalls++;
        void second_command(KeyboardModifier mod) => numOfCalls++;
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterKeyUp('a', first_command);
        device.RegisterKeyUp('a', second_command);

        device.FireKeyUp('a', KeyboardModifier.None);

        Assert(numOfCalls == 2, numOfCalls.ToString());
    }

    public void GivenRegisteredCommandXKeyUpA_whenFireKeyUpB_thenCommandXIsNotCalled()
    {
        var called = false;
        void command(KeyboardModifier mod) => called = true;
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterKeyUp((int)'a', command);

        device.FireKeyUp((int)'b', KeyboardModifier.None);

        Assert(called == false);
    }

    public void GivenRegisteredCommandXKeyUpAandCommandYKeyUpB_whenFireKeyUpA_thenCommandXIsCalledOnce()
    {
        var called = false;
        void first_command(KeyboardModifier mod) => called = true;
        void second_command(KeyboardModifier mod) { }
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterKeyUp((int)'a', first_command);
        device.RegisterKeyUp((int)'b', second_command);

        device.FireKeyUp((int)'a', KeyboardModifier.None);

        Assert(called == true);
    }

    public void GivenRegisteredCommandXKeyUpAandCommandYKeyUpB_whenFireKeyUpA_thenCommandYIsNotCalled()
    {
        var called = false;
        void first_command(KeyboardModifier mod) { }
        void second_command(KeyboardModifier mod) => called = true;
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterKeyUp((int)'a', first_command);
        device.RegisterKeyUp((int)'b', second_command);

        device.FireKeyUp((int)'a', KeyboardModifier.None);

        Assert(called == false);
    }

    public void GivenRegisteredCommandXKeyUpAandCommandYKeyUpB_whenFireKeyUpB_thenCommandYIsCalledOnce()
    {
        var called = false;
        void first_command(KeyboardModifier mod) { }
        void second_command(KeyboardModifier mod) => called = true;
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterKeyUp((int)'a', first_command);
        device.RegisterKeyUp((int)'b', second_command);

        device.FireKeyUp((int)'b', KeyboardModifier.None);

        Assert(called == true);
    }

    public void GivenRegisteredCommandXKeyUpAandCommandYKeyUpB_whenFireKeyUpB_thenCommandXIsNotCalled()
    {
        var called = false;
        void first_command(KeyboardModifier mod) => called = true;
        void second_command(KeyboardModifier mod) { };
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterKeyUp((int)'a', first_command);
        device.RegisterKeyUp((int)'b', second_command);

        device.FireKeyUp((int)'b', KeyboardModifier.None);

        Assert(called == false);
    }


    //keydown

    
    public void GivenRegisteredCommandXKeyDownShiftA_whenFireKeyDownShiftA_thenCommandXIsCalledOnceWithShift()
    {
        var calledWithShift = false;
        void command(KeyboardModifier mod) => calledWithShift = mod.HasFlag(KeyboardModifier.Shift);
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterKeyDown((int)'a', command);

        device.FireKeyDown((int)'a', KeyboardModifier.Shift);

        Assert(calledWithShift == true);
    }

    public void GivenRegisteredCommandXKeyDownShiftCtrlA_whenFireKeyDownShiftCtrlA_thenCommandXIsCalledOnceWithShiftCtrl()
    {
        var shiftCtrl = KeyboardModifier.Shift | KeyboardModifier.Ctrl;
        var calledWithShift = false;
        void command(KeyboardModifier mod) => calledWithShift = mod.HasFlag(shiftCtrl);
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterKeyDown((int)'a', command);

        device.FireKeyDown((int)'a', shiftCtrl);

        Assert(calledWithShift == true);
    }

    public void GivenRegisteredCommandXKeyDownShiftCtrlAltA_whenFireKeyDownShiftCtrlAltA_thenCommandXIsCalledOnceWithShiftCtrlAlt()
    {
        var shiftCtrlAlt = KeyboardModifier.Shift | KeyboardModifier.Ctrl | KeyboardModifier.Alt;
        var calledWithShift = false;
        void command(KeyboardModifier mod) => calledWithShift = mod.HasFlag(shiftCtrlAlt);
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterKeyDown((int)'a', command);

        device.FireKeyDown((int)'a', shiftCtrlAlt);

        Assert(calledWithShift == true);
    }

    public void GivenRegisteredCommandXKeyDownCtrlA_whenFireKeyDownCtrlA_thenCommandXIsCalledOnceWithCtrl()
    {
        var calledWithCtrl = false;
        void command(KeyboardModifier mod) => calledWithCtrl = mod.HasFlag(KeyboardModifier.Ctrl);
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterKeyDown((int)'a', command);

        device.FireKeyDown((int)'a', KeyboardModifier.Ctrl);

        Assert(calledWithCtrl == true);
    }

    public void GivenRegisteredCommandXKeyDownCtrlAltA_whenFireKeyDownCtrlAltA_thenCommandXIsCalledOnceWithCtrlAlt()
    {
        var shiftCtrlAlt = KeyboardModifier.Ctrl | KeyboardModifier.Alt;
        var calledWithCtrl = false;
        void command(KeyboardModifier mod) => calledWithCtrl = mod.HasFlag(shiftCtrlAlt);
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterKeyDown((int)'a', command);

        device.FireKeyDown((int)'a', shiftCtrlAlt);

        Assert(calledWithCtrl == true);
    }

    public void GivenRegisteredCommandXKeyDownAltA_whenFireKeyDownAltA_thenCommandXIsCalledOnceWithAlt()
    {
        var calledWithAlt = false;
        void command(KeyboardModifier mod) => calledWithAlt = mod.HasFlag(KeyboardModifier.Alt);
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterKeyDown((int)'a', command);

        device.FireKeyDown((int)'a', KeyboardModifier.Alt);

        Assert(calledWithAlt == true);
    }

    // key down commands

    public void GivenRegisteredCommandXKeyDownA_whenFireKeyDownA_thenCommandXIsCalledOnce()
    {
        var called = false;
        void command(KeyboardModifier mod) => called = true;
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterKeyDown((int)'a', command);

        device.FireKeyDown((int)'a', KeyboardModifier.None);

        Assert(called == true);
    }

    public void GivenRegisteredTwiceCommandXKeyDownA_whenFireKeyDownA_thenCommandXIsCalledTwice()
    {
        var numOfCalls = 0;
        void command(KeyboardModifier mod) => numOfCalls++;
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterKeyDown((int)'a', command);
        device.RegisterKeyDown((int)'a', command);

        device.FireKeyDown((int)'a', KeyboardModifier.None);

        Assert(numOfCalls == 2);
    }

    public void GivenRegisteredCommandXKeyDownA_whenFireKeyDownB_thenCommandXIsNotCalled()
    {
        var called = false;
        void command(KeyboardModifier mod) => called = true;
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterKeyDown((int)'a', command);

        device.FireKeyDown((int)'b', KeyboardModifier.None);

        Assert(called == false);
    }

    public void GivenRegisteredCommandXKeyDownAandCommandYKeyDownB_whenFireKeyDownA_thenCommandXIsCalledOnce()
    {
        var called = false;
        void first_command(KeyboardModifier mod) => called = true;
        void second_command(KeyboardModifier mod) { }
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterKeyDown((int)'a', first_command);
        device.RegisterKeyDown((int)'b', second_command);

        device.FireKeyDown((int)'a', KeyboardModifier.None);

        Assert(called == true);
    }

    public void GivenRegisteredCommandXKeyDownAandCommandYKeyDownB_whenFireKeyDownA_thenCommandYIsNotCalled()
    {
        var called = false;
        void first_command(KeyboardModifier mod) { }
        void second_command(KeyboardModifier mod) => called = true;
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterKeyDown((int)'a', first_command);
        device.RegisterKeyDown((int)'b', second_command);

        device.FireKeyDown((int)'a', KeyboardModifier.None);

        Assert(called == false);
    }

    public void GivenRegisteredCommandXKeyDownAandCommandYKeyDownB_whenFireKeyDownB_thenCommandYIsCalledOnce()
    {
        var called = false;
        void first_command(KeyboardModifier mod) { }
        void second_command(KeyboardModifier mod) => called = true;
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterKeyDown((int)'a', first_command);
        device.RegisterKeyDown((int)'b', second_command);

        device.FireKeyDown((int)'b', KeyboardModifier.None);

        Assert(called == true);
    }

    public void GivenRegisteredCommandXKeyDownAandCommandYKeyDownB_whenFireKeyDownB_thenCommandXIsNotCalled()
    {
        var called = false;
        void first_command(KeyboardModifier mod) => called = true;
        void second_command(KeyboardModifier mod) { };
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterKeyDown((int)'a', first_command);
        device.RegisterKeyDown((int)'b', second_command);

        device.FireKeyDown((int)'b', KeyboardModifier.None);

        Assert(called == false);
    }


    // mouse down


    public void GivenRegisteredCommandXMouseDownRight_whenFireMouseDownRight_thenCommandXIsCalledOnce()
    {
        var called = false;
        void command(Point position) => called = true;
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterMouseDown(MouseButton.Right, command);

        device.FireMouseDown(MouseButton.Right);

        Assert(called == true);
    }

    public void GivenRegisteredCommandXMouseDownLeft_whenFireMouseDownLeft_thenCommandXIsCalledOnce()
    {
        var called = false;
        void command(Point position) => called = true;
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterMouseDown(MouseButton.Left, command);

        device.FireMouseDown(MouseButton.Left);

        Assert(called == true);
    }

    public void GivenRegisteredCommandXMouseDownMiddle_whenFireMouseDownMiddle_thenCommandXIsCalledOnce()
    {
        var called = false;
        void command(Point position) => called = true;
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterMouseDown(MouseButton.Middle, command);

        device.FireMouseDown(MouseButton.Middle);

        Assert(called == true);
    }

    public void GivenRegisteredCommandXMouseDownRight_whenFireMouseDownLeft_thenCommandXIsNotCalled()
    {
        var called = false;
        void command(Point position) => called = true;
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterMouseDown(MouseButton.Right, command);

        device.FireMouseDown(MouseButton.Left);

        Assert(called == false);
    }

    public void GivenRegisteredCommandXMouseDownLeftAndYMouseDownRight_whenFireMouseDownLeft_thenCommandXIsCalledOnce()
    {
        var called = false;
        void first_command(Point position) => called = true;
        void second_command(Point position){};
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterMouseDown(MouseButton.Left, first_command);
        device.RegisterMouseDown(MouseButton.Right, second_command);

        device.FireMouseDown(MouseButton.Left);

        Assert(called == true);
    }

    public void GivenRegisteredCommandXMouseDownLeftAndCommandYMouseDownRight_whenFireMouseDownLeft_thenCommandYIsNotCalled()
    {
        var called = false;
        void first_command(Point position) {}
        void second_command(Point position) => called = true;
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterMouseDown(MouseButton.Left, first_command);
        device.RegisterMouseDown(MouseButton.Right, second_command);

        device.FireMouseDown(MouseButton.Left);

        Assert(called == false);
    }

    public void GivenRegisteredTwiceCommandXMouseDownRight_whenFireMouseDownRight_thenCommandXIsCalledTwice()
    {
        var numOfCalls = 0;
        void command(Point position) => numOfCalls++;
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterMouseDown(MouseButton.Right, command);
        device.RegisterMouseDown(MouseButton.Right, command);

        device.FireMouseDown(MouseButton.Right);

        Assert(numOfCalls == 2);
    }

    // mouse wheel

    public void GivenRegisteredCommandXMouseWheel_whenFireMouseWheel_thenCommandXIsCalledOnce()
    {
        var called = false;
        void command(MouseWheelDirection direction) => called = true;
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterMouseWheelScroll(command);

        device.FireMouseWheel(MouseWheelDirection.Forward);

        Assert(called == true);
    }

    public void GivenRegisteredCommandXMouseWheelForward_whenFireMouseWheelForward_thenCommandXCalledWithForward()
    {
        var calledWithForward = false;
        void command(MouseWheelDirection direction) => calledWithForward = direction == MouseWheelDirection.Forward;
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterMouseWheelScroll(command);

        device.FireMouseWheel(MouseWheelDirection.Forward);

        Assert(calledWithForward == true);
    }

    public void GivenRegisteredCommandXMouseWheelBackward_whenFireMouseWheelForward_thenCommandXCalledWithBackward()
    {
        var calledWithBackward = false;
        void command(MouseWheelDirection direction) => calledWithBackward = direction == MouseWheelDirection.Backward;
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterMouseWheelScroll(command);

        device.FireMouseWheel(MouseWheelDirection.Backward);

        Assert(calledWithBackward == true);
    }


    // Changing game must reset the registers

    public void GivenRegisteredCommandXKeyUpA_whenGameIsSetAndFireKeyUpA_thenCommandXIsNotCalled()
    {
        var called = false;
        void command(KeyboardModifier mod) => called = true;
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterKeyUp('a', command);

        device.Game = gameDummy;
        device.FireKeyUp('a', KeyboardModifier.None);

        Assert(called == false);
    }

    public void GivenRegisteredCommandXKeyDownA_whenGameIsSetAndFireKeyDownA_thenCommandXIsNotCalled()
    {
        var called = false;
        void command(KeyboardModifier mod) => called = true;
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterKeyDown('a', command);

        device.Game = gameDummy;
        device.FireKeyDown('a', KeyboardModifier.None);

        Assert(called == false);
    }

    public void GivenCommandXMouseUpLeft_whenGameIsSetAndFireMouseUpLeft_thenCommandXIsNotCalled()
    {
        var called = false;
        void command(Point p) => called = true;
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterMouseUp(MouseButton.Left, command);

        device.Game = gameDummy;
        device.FireMouseUp(MouseButton.Left);

        Assert(called == false);
    }

    public void GivenCommandXMouseDownLeft_whenGameIsSetAndFireMouseDownLeft_thenCommandXIsNotCalled()
    {
        var called = false;
        void command(Point p) => called = true;
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterMouseDown(MouseButton.Left, command);

        device.Game = gameDummy;
        device.FireMouseDown(MouseButton.Left);

        Assert(called == false);
    }

    public void GivenCommandXMouseWheel_whenGameIsSetAndFireMouseWheel_thenCommandXIsNotCalled()
    {
        var called = false;
        void command(MouseWheelDirection direction) => called = true;
        var gameDummy = new Game();
        var device = new DeviceHelper(gameDummy);
        device.RegisterMouseWheelScroll(command);

        device.Game = gameDummy;
        device.FireMouseWheel(MouseWheelDirection.Forward);

        Assert(called == false);
    }
}
