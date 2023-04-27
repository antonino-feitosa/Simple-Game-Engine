using System.IO;

namespace SGE;

public class PlatformTest {

    public static void Test(Platform device){

        device.RegisterKeyDown('a',  (mask) => Console.WriteLine("Key Down! " + mask));
        device.RegisterKeyUp('a',  (mask) => Console.WriteLine("Key Up! " + mask));
        device.RegisterMouseDown('L', (x,y) => Console.WriteLine("Left Button Down " + x + "," + y));
        device.RegisterMouseDown('R', (x,y) => Console.WriteLine("Right Button Down"));
        device.RegisterMouseDown('M', (x,y) => Console.WriteLine("Middle Button Down"));
        device.RegisterMouseUp('L', (x,y) => Console.WriteLine("Left Button Up"));
        device.RegisterMouseUp('R', (x,y) => Console.WriteLine("Right Button Up"));
        device.RegisterMouseUp('M', (x,y) => Console.WriteLine("Middle Button Up"));
        device.RegisterMouseWheel( (delta) => Console.WriteLine("Wheel " + delta));

        var sound = device.LoadSound("./art/leap-motiv-113893.wav");
        var image = device.LoadImage("./art/Doors.png");
        var sheet = device.LoadSpriteSheet("./art/BallRed.png", 32, 32);
        var text = device.LoadText("Text");

        device.Start();
        sound.Play();

        device.RegisterLoop(()=>{
            text.Render(0, 0);
            image.Render(100, 100);
            sheet.GetImage(0).Render(300, 300);
            Console.WriteLine("Loop");
        }, 1);
    }
}
