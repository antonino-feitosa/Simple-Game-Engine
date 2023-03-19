
public class ConsoleInputComponent : Component
{

    public virtual bool OnKeyDown(char c)
    {
        return false;
    }

    public virtual bool OnKeyUp(char c)
    {
        return false;
    }

    public virtual bool OnKeyPressed(char c)
    {
        return false;
    }
}

public class ConsoleInputSystem : SubSystem<ConsoleInputComponent>
{

    protected HashSet<char> _pressed;
    protected HashSet<char> _update;
    protected Thread _thread;
    protected bool _running;
    protected Game _game;

    public ConsoleInputSystem(Game game)
    {
        _game = game;
        _update = new HashSet<char>();
        _pressed = new HashSet<char>();
        _thread = new Thread(new ThreadStart(this.ListenInput));
    }

    public override void Start()
    {
        _running = true;
        _thread.Start();
    }

    public void ListenInput()
    {
        Console.TreatControlCAsInput = true;
        while (_running)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.C && keyInfo.Modifiers == ConsoleModifiers.Control)
            {
                _game.Stop();
            }
            else
            {
                _update.Add(keyInfo.KeyChar);
            }
            Thread.Yield();
        }
    }

    public override void Process()
    {
        var upkeys = _pressed.Except(_update);
        var downkeys = _update.Except(_pressed);
        _pressed.IntersectWith(_update);
        _update.Clear();

        foreach (var comp in _components)
        {
            foreach (var up in upkeys)
            {
                comp.OnKeyDown(up);
            }
            foreach (var down in downkeys)
            {
                comp.OnKeyDown(down);
            }
            foreach (var press in _pressed)
            {
                comp.OnKeyDown(press);
            }
        }
    }

    public override void Finish()
    {
        _running = false;
    }
}
