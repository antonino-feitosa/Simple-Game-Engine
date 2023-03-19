
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

public class ConsoleInputSystem : SubSystem
{

    protected HashSet<char> _pressed;
    protected HashSet<char> _update;
    protected Thread _thread;
    protected bool _running;

    private object Lock = new object();

    public ConsoleInputSystem()
    {
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
                _game?.Stop();
            }
            else
            {
                lock (Lock)
                {
                    _update.Add(keyInfo.KeyChar);
                }
            }
            Thread.Yield();
        }
    }

    private void FireEvents(HashSet<char> keys, Func<ConsoleInputComponent, char, bool> method)
    {
        foreach (var k in keys)
        {
            foreach (var comp in GetComponents<ConsoleInputComponent>())
            {
                if (method(comp, k))
                    break;
            }
        }
    }

    public override void Process()
    {
        HashSet<char> upkeys, downkeys;
        lock (Lock)
        {
            upkeys = _pressed.Except(_update).ToHashSet();
            downkeys = _update.Except(_pressed).ToHashSet();
            _pressed.Clear();
            _pressed.UnionWith(_update);
            _update.Clear();
        }

        Func<ConsoleInputComponent, char, bool> keyUpFunction = (obj, key) => obj.OnKeyUp(key);
        Func<ConsoleInputComponent, char, bool> keyDownFunction = (obj, key) => obj.OnKeyDown(key);
        Func<ConsoleInputComponent, char, bool> keyPressFunction = (obj, key) => obj.OnKeyPressed(key);

        FireEvents(upkeys, keyUpFunction);
        FireEvents(downkeys, keyDownFunction);
        FireEvents(_pressed, keyPressFunction);
    }

    public override void Finish()
    {
        _running = false;
    }
}
