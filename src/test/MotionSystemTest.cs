
namespace SGE;

public class MotionSystemTest
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

        var pos_sys = new PositionSystem(ground);
        var mon_sys = new MotionSystem();
        var pos_comp = pos_sys.CreateComponent(entity, 1, 4);
        var mon_comp = mon_sys.CreateComponent(pos_comp, 4);

        mon_comp.OnIdle += (pos) => {pos_comp.Move(pos_sys.UP_RIGHT); Console.WriteLine("Moving Right");};
        mon_comp.OnEndMove += (pos) => {Console.WriteLine("End Move at " + pos);};
        mon_comp.OnMoving += (pos) => {Console.WriteLine("Moving at " + pos);};
        mon_comp.OnStartMove += (pos) => {Console.WriteLine("Start Move at " + pos);};

        
        pos_sys.Start();
        mon_sys.Start();
        for (int i = 0; i < 40; i++)
        {
            pos_sys.Process();
            mon_sys.Process();
            Console.WriteLine("\tPosition " + mon_comp);
        }
    }
}