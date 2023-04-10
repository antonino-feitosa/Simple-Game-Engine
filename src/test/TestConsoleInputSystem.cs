
namespace SGE;

public class TestConsoleInputSystem {

    public static void Main1(){
        
        var game = new Game(new PlatformAdapter());
        var console = new ConsoleInputSystem(game);

        var entity = game.CreateEntity();
        var echo = new ConsoleInputComponent();
        echo.OnKeyUp += (char c) => {Console.WriteLine("\t\t\t\t\t\tKey Up " + c); return true; };
        echo.OnKeyDown += (char c) => {Console.WriteLine("\t\t\t\t\t\tKey Down " + c); return true; };
        echo.OnKeyPressed += (char c) => {Console.WriteLine("\t\t\t\t\t\tKey Pressed " + c); return true; };
        entity.AttachComponent(echo);
        console.Register(echo);

        game.Start();
        game.Loop();
    }
}
