
public class TestCollision2System {

    public static void Main(){
        var collision = new Collision2System();
        var render = new SubSystem();

        var game = new Game(render);
        game.AttachSystem(collision);

        var a_comp = new Collision2Component(new Vector2(-10, 0), 0.5, 1.0);
        a_comp.OnStart += () => a_comp.MoveTo(new Vector2(0,0));
        a_comp.OnUpdate += () => Console.WriteLine("Position a: " + a_comp._position);
        a_comp.OnCollision += (Collision2Component other) => Console.WriteLine("Collision: a");
        a_comp.OnEndMoving += () => Console.WriteLine("End Move a");
        collision.Register(a_comp);
        var a_entity = game.CreateEntity(a_comp);
        
        var b_comp = new Collision2Component(new Vector2(10, 0), 0.5, 1.0);
        b_comp.OnStart += () => b_comp.MoveTo(new Vector2(0,0));
        b_comp.OnUpdate += () => Console.WriteLine("Position b: " + b_comp._position);
        b_comp.OnCollision += (Collision2Component other) => Console.WriteLine("Collision: b");
        b_comp.OnEndMoving += () => Console.WriteLine("End Move b");
        collision.Register(b_comp);
        var b_entity = game.CreateEntity(b_comp);

        game.Start();
        game.Run();
    }
}
