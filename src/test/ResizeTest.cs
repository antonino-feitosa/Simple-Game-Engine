namespace SGE;

public class ResizeTest
{

    public static void Test(Device device, Game game)
    {

        var entity = game.CreateEntity();
        var sys = new CameraSystem(new Vector2(), new Dimension(device.Width, device.Height));
        game.AttachSystem(sys);

        var img = device.LoadImage("./art/BallRed.png");
        var cropped = img.Crop(0, 0, 32, 32);
        var resized = cropped.Resize(64, 64);

        var a_comp = sys.CreateComponent(entity, cropped, new Vector2(1, 1));
        var b_comp = sys.CreateComponent(entity, resized, new Vector2(3, 3));
    }
}