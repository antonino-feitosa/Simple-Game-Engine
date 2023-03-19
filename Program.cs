
var console = new ConsoleInputSystem();
var render = new SubSystem();

var game = new Game(render);
game.AttachSystem(console);

var entity = game.CreateEntity();
var echo = new EchoComponent();
entity.AttachComponent(echo);
console.Register(echo);

game.Start();
game.Run();


class EchoComponent : ConsoleInputComponent {


    public override bool OnKeyDown(char c)
    {
        Console.WriteLine("\t\t\t\t\t\tKey Down " + c);
        return true;
    }

    public override bool OnKeyPressed(char c)
    {
        Console.WriteLine("\t\t\t\t\t\tKey Pressed " + c);
        return true;
    }

    public override bool OnKeyUp(char c)
    {
        Console.WriteLine("\t\t\t\t\t\tKey Up " + c);
        return true;
    }
}
