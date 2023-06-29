
namespace SimpleGameEngine.Test;

[TestClass]
public class WindowsFontTest : IFontTest
{
    public static IFont CreateWindowsFont()
    {
        var form = new DoubleBufferedForm();
        var game = new Game();
        var helper = new DeviceHelper(game);
        var device = new WindowsDevice(form, helper);
        var font = device.MakeFont("resource.ttf");
        return font;
    }

    public WindowsFontTest() : base(CreateWindowsFont()) { }
}
