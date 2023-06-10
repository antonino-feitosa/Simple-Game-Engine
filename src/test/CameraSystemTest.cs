
namespace SGE;

public class CameraSystemTest
{

    public static void Test(Device device, Game game)
    {

        var entity = game.CreateEntity();
        var sys = new CameraSystem(new Vector2(), new Dimension(device.Width, device.Height));
        game.AttachSystem(sys);

        var img = device.LoadImage("./art/BallRed.png");
        var sheet = device.LoadSpriteSheet(img, 32, 32);
        var a_comp = sys.CreateComponent(entity, sheet.GetImage(0), new Vector2(-0.5, -0.5));
        var b_comp = sys.CreateComponent(entity, sheet.GetImage(1), new Vector2(2,1));
    }
}