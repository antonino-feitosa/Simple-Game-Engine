
namespace SGE;

public class TestConsoleRenderSystem {

    public static void Main1(){
        var game = new Game(new PlatformAdapter());
        var sys = new ConsoleRenderSystem(game, new Dimension(20, 10));
        var comp = new ConsoleRenderComponent(new Position(2, 3), '@', Color.Yellow);
        sys.Register(comp);

        sys.Start();

        comp.DoUpdate();

        sys.Process();
        sys.Finish();
    }
}