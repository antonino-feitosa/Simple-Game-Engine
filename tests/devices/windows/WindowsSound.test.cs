
namespace SimpleGameEngine.Test;

[TestClass]
public class WindowsSoundTest : ISoundTest
{
    public static ISound CreateWindowsSound()
    {
        var form = new DoubleBufferedForm();
        var game = new Game();
        var helper = new DeviceHelper(game);
        var device = new WindowsDevice(form, helper);
        var sound = device.MakeSound("resource.mp3");
        return sound;
    }

    public WindowsSoundTest() : base(CreateWindowsSound()) { }
}
