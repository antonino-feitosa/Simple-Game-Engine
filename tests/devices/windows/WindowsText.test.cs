
namespace SimpleGameEngine.Test;

[TestClass]
public class WindowsTextTest : ITextTest
{
    private static WindowsDevice? _device;
    public static WindowsDevice CreateDevice()
    {
        var form = new DoubleBufferedForm();
        var game = new Game();
        var helper = new DeviceHelper(game);
        _device = new WindowsDevice(form, helper);
        return _device;
    }

    public static IText CreateWindowsText()
    {
        if (_device is null) throw new ArgumentException();
        var font = _device.MakeFont("resource.ttf");
        return _device.MakeText("text", font);
    }

    public WindowsTextTest() : base(CreateDevice(), CreateWindowsText()) { }
}
