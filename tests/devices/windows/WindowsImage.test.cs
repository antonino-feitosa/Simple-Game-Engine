
namespace SimpleGameEngine.Test;

[TestClass]
public class WindowsImageTest : IImageTest
{
    public static IImage CreateWindowsImage()
    {
        var form = new DoubleBufferedForm();
        var game = new Game();
        var helper = new DeviceHelper(game);
        var device = new WindowsDevice(form, helper);
        var image = device.MakeImage("resource.png");
        return image;
    }

    public WindowsImageTest() : base(CreateWindowsImage()) { }
}
