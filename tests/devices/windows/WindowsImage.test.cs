
namespace SimpleGameEngine.Test;

[TestClass]
public class WindowsImageTest : IImageTest
{
    public static IImage CreateWindowsImage()
    {
        var form = new DoubleBufferedForm();
        var helper = new DeviceHelper();
        var device = new WindowsDevice(form, helper);
        var image = device.MakeImage("resource.png");
        return image;
    }

    public WindowsImageTest() : base(CreateWindowsImage()) { }
}
