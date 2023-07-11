
namespace SimpleGameEngine.Test;

[TestClass]
public class WindowsColorTest : IColorTest
{
    public static IColor CreateWindowsColor()
    {
        var form = new DoubleBufferedForm();
        var helper = new DeviceHelper();
        var device = new WindowsDevice(form, helper);
        var color = device.MakeColor(0, 0, 0);
        return color;
    }

    public WindowsColorTest() : base(CreateWindowsColor()) { }
}
