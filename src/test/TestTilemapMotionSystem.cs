
public class EchoComp : TilemapMotionComponent
{

    public string name;
    public Direction dir;
    public EchoComp(string name, Direction dir, TilePosition position, int updateTime = 0) : base(position, updateTime)
    {
        this.name = name;
        this.dir = dir;
        OnUpdate = delegate
        {
            Console.WriteLine(name + ": Position " + Pos);
            Move(dir);
        };
        OnCollision += delegate (TilemapMotionComponent other)
        {
            if (other is EchoComp c)
                Console.WriteLine(name + ": Collision with " + c.name);
        };
    }

    public override string ToString()
    {
        return name;
    }
}

public class TestTilemapMotionSystem
{

    public static void Main()
    {
        var sys = new TilemapMotionSystem();
        var a = new EchoComp("A", Direction.RIGHT, new TilePosition(0, 0));
        var b = new EchoComp("B", Direction.LEFT, new TilePosition(10, 0));
        var c = new EchoComp("C", Direction.RIGHT, new TilePosition(0, 2));
        var d = new EchoComp("D", Direction.RIGHT, new TilePosition(2, 2));
        var e = new EchoComp("E", Direction.RIGHT, new TilePosition(1, 2));
        sys.Register(a);
        sys.Register(b);
        sys.Register(c);
        sys.Register(d);
        sys.Register(e);

        var render = new SubSystem();
        var game = new Game(render);
        var a_ent = game.CreateEntity(a);
        var b_ent = game.CreateEntity(b);
        var c_ent = game.CreateEntity(c);
        var d_ent = game.CreateEntity(d);
        var e_ent = game.CreateEntity(e);
        game.AttachSystem(sys);
        game.Start();
        game.Run();
    }
}