
namespace SimpleGameEngine.Test;

[TestClass]
public class WindowsDeviceTest : IDeviceTest
{
    public static WindowsDevice CreateDevice()
    {
        var form = new DoubleBufferedForm();
        var game = new Game();
        var helper = new DeviceHelper(game);
        return new WindowsDevice(form, helper);
    }

    public WindowsDeviceTest() : base(CreateDevice()) { }
}
