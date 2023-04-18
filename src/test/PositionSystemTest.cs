
namespace SGE;

public class PositionSystemTest
{

    public static void Test()
    {
        var ground = new HashSet<PositionSystem.Position>();

        var dim = 10;
        for (int i = 0; i < dim; i++)
        {
            for (int j = 0; j < dim; j++)
            {
                var pos = new PositionSystem.Position(i * 10, j);
                ground.Add(pos);
            }
        }

        var sys = new PositionSystem(ground);
        var a = sys.CreateComponent(1, 1);
        a.OnCollision += (other) => Console.WriteLine("Collision with " + other);
        a.OnMove += (source, dest) => Console.WriteLine("Moved from " + source + " to " + dest);
        a.OnOutOfBounds += (pos) => Console.WriteLine("Out of Bounds" + pos);

        sys.Start();
        for (int i = 0; i < 2; i++)
        {
            a.Move(sys.RIGHT);
            Console.WriteLine("\tPosition " + a);
        }
    }
}