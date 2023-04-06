
public class TestConsoleRenderSystem {

    public static void Main1(){
        var sys = new ConsoleRenderSystem(new Dimension(20, 10));
        var comp = new ConsoleRenderComponent(new Position(2, 3), '@', Color.Yellow);
        sys.Register(comp);

        sys.Start();

        comp.DoUpdate();

        sys.Process();
        sys.Finish();
    }
}