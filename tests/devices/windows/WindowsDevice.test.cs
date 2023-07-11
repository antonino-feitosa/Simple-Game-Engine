
namespace SimpleGameEngine.Test;

[TestClass]
public class WindowsDeviceTest : IDeviceTest
{
    public static WindowsDevice CreateDevice()
    {
        var form = new DoubleBufferedForm();
        var helper = new DeviceHelper();
        return new WindowsDevice(form, helper);
    }

    public WindowsDeviceTest() : base(CreateDevice()) { }
}
