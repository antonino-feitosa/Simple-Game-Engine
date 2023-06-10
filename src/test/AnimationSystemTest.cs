
namespace SGE;

public class AnimationSystemTest
{

    public static void Test(Device device, Game game)
    {

        var entity = game.CreateEntity();
        var cam_sys = new CameraSystem(new Vector2(), new Dimension(device.Width, device.Height));
        var ani_sys = new AnimationSystem();
        game.AttachSystem(cam_sys);
        game.AttachSystem(ani_sys);

        var img = device.LoadImage("./art/BallRed.png");
        var sheet = device.LoadSpriteSheet(img, 32, 32);
        var render = cam_sys.CreateComponent(entity, sheet.GetImage(0), new Vector2(0, 0));
        var anim = ani_sys.CreateComponent(render, sheet, new int[] { 0, 1, 2, 3 });
        anim.UpdatesByFrames = 8;
    }
}