
namespace SimpleGameEngine.Test;

[TestClass]
public class WindowsSpriteSheetTest : ISpriteSheetTest
{
    public static ISpriteSheet CreateWindowsSpriteSheet()
    {
        var form = new DoubleBufferedForm();
        var game = new Game();
        var helper = new DeviceHelper(game);
        var device = new WindowsDevice(form, helper);
        var image = device.MakeImage("resource.png");
        var sheet = device.MakeSpriteSheet(image, new Dimension(32,32));
        return sheet;
    }

    public WindowsSpriteSheetTest() : base(CreateWindowsSpriteSheet()) { }
}
