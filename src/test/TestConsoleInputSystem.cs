
public class TestConsoleInputSystem {

    public static void Main(){
        var console = new ConsoleInputSystem();
        var render = new SubSystem();

        var game = new Game(render);
        game.AttachSystem(console);

        var entity = game.CreateEntity();
        var echo = new ConsoleInputComponent();
        echo.OnKeyUp += (char c) => {Console.WriteLine("\t\t\t\t\t\tKey Up " + c); return true; };
        echo.OnKeyDown += (char c) => {Console.WriteLine("\t\t\t\t\t\tKey Down " + c); return true; };
        echo.OnKeyPressed += (char c) => {Console.WriteLine("\t\t\t\t\t\tKey Pressed " + c); return true; };
        entity.AttachComponent(echo);
        console.Register(echo);

        game.Start();
        game.Run();
    }
}
