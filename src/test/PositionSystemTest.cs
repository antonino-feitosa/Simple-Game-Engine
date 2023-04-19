
namespace SGE;

public class PositionSystemTest
{

    public static void Test(Game game)
    {
        var ground = new HashSet<PositionSystem.Position>();

        var dim = 10;
        for (int i = 0; i < dim; i++)
        {
            for (int j = 0; j < dim; j++)
            {
                var pos = new PositionSystem.Position(i, j);
                ground.Add(pos);
            }
        }

        var entity = game.CreateEntity();

        var sys = new PositionSystem(ground);
        var a = sys.CreateComponent(entity, 1, 1);
        a.Name = "A";
        a.OnCollision += (other) => Console.WriteLine("A:Collision with " + other);
        a.OnMove += (source, dest) => Console.WriteLine("A:Moved from " + source + " to " + dest);
        a.OnOutOfBounds += (pos) => Console.WriteLine("A:Out of Bounds" + pos);

        var b = sys.CreateComponent(entity, 7, 1);
        b.Name = "B";
        b.OnCollision += (other) => Console.WriteLine("B:Collision with " + other);
        b.OnMove += (source, dest) => Console.WriteLine("B:Moved from " + source + " to " + dest);
        b.OnOutOfBounds += (pos) => Console.WriteLine("B:Out of Bounds" + pos);

        sys.Start();
        for (int i = 0; i < 10; i++)
        {
            a.Move(sys.RIGHT);
            b.Move(sys.LEFT);
            sys.Process();
            Console.WriteLine("\tPosition " + a);
            Console.WriteLine("\tPosition " + b);
        }
    }
}