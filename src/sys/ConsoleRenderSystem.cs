
namespace SGE;

public class Color
{
    public static readonly Color Black = new Color("000000");
    public static readonly Color White = new Color("FFFFFF");
    public static readonly Color Blue = new Color("0000FF");
    public static readonly Color Red = new Color("FF0000");
    public static readonly Color Green = new Color("00FF00");
    public static readonly Color Yellow = new Color("FFFF00");
    public static readonly Color Grey = new Color("808080");

    protected internal string _value;

    public Color(string value)
    {
        _value = value;
    }

    protected internal string ToASCII()
    {
        return string.Format("{0};{1};{2}", GetChannel(0), GetChannel(1), GetChannel(2));
    }

    private string GetChannel(int index)
    {
        string color = _value.Substring(index, 2);
        int hex = Convert.ToInt32(color, 16);
        color = hex.ToString();
        /*if (hex < 10)
            color = '0' + color;
        if (hex < 100)
            color = '0' + color;*/
        return color;
    }
}

public class Position
{
    public int X;
    public int Y;

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class Dimension
{

    public int Width;
    public int Height;

    public Dimension(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public Dimension(Dimension dim) : this(dim.Width, dim.Height)
    {
    }
}

public class ConsoleRenderComponent : Component
{

    public Position Pos;
    public char Glyph;
    public bool IsVisible;

    public Color Foreground;
    public Color Background;

    public ConsoleRenderComponent(Position pos, char glyph, Color foreground)
    {
        Pos = pos;
        Glyph = glyph;
        IsVisible = true;
        Foreground = foreground;
        Background = Color.Black;
    }

    protected internal override void DoUpdate()
    {
        base.DoUpdate();
        GetSystem<ConsoleRenderSystem>()?.Render(this);
    }
}

public class CameraConsoleRenderComponent
{
    public Position Pos;
    public Dimension Dim;

    public CameraConsoleRenderComponent(Position pos, Dimension dim)
    {
        Pos = pos;
        Dim = dim;
    }

    public bool IsAtView(ConsoleRenderComponent render)
    {
        Position r = render.Pos;
        return r.X >= Pos.X && r.Y >= Pos.Y && r.X < (Pos.X + Dim.Width) && r.Y < (Pos.Y + Dim.Height);
    }
}

public class ConsoleRenderSystem : SubSystem
{
    public bool IsHideCursor;
    public bool IsClearConsole;

    public readonly CameraConsoleRenderComponent Camera;
    public readonly Dimension Dim;
    public Color Foreground;
    public Color Background;

    private string[,] _buffer;

    public ConsoleRenderSystem(Game game, Dimension dimension) : base(game)
    {
        IsHideCursor = true;
        IsClearConsole = true;
        Foreground = Color.White;
        Background = Color.Black;
        Dim = new Dimension(dimension);
        _buffer = new string[Dim.Height, Dim.Width];
        Camera = new CameraConsoleRenderComponent(new Position(0, 0), new Dimension(dimension));
        ClearBuffer();
    }

    public void Render(ConsoleRenderComponent render)
    {
        if (Camera.IsAtView(render))
        {
            string fg = render.Foreground.ToASCII();
            string bg = render.Background.ToASCII();
            string color = string.Format("\u001b[38;2;{0}m\u001b[48;2;{1}m", fg, bg);
            _buffer[render.Pos.X, render.Pos.Y] = color + render.Glyph.ToString();
        }
    }

    public override void Start()
    {
        Console.Write("\u001b[?1049h"); // enable double buffer
        if (IsHideCursor)
            Console.Write("\u001b[?25l"); // hide cursor
    }

    public override void Process()
    {
        string text = "";
        if (IsClearConsole)
        {
            // move to first line and column
            text += string.Format("\x1b[{0}A", Dim.Height + 1);
        }
        for (int y = 0; y < Dim.Height; y++)
        {
            for (int x = 0; x < Dim.Width; x++)
            {
                text += _buffer[y, x];
            }
            text += '\n';
        }
        Console.Write(text);
        ClearBuffer();
    }

    public override void Finish()
    {
        Console.Write("\u001b[0m"); // reset colors and mode
        Console.Write("\u001b[?25h"); // restore cursor
    }

    public void ClearBuffer()
    {
        for (int y = 0; y < Dim.Height; y++)
        {
            for (int x = 0; x < Dim.Width; x++)
            {
                _buffer[y, x] = ApplyColor(" ", Foreground, Background);
            }
        }
    }

    protected static string ApplyColor(ConsoleRenderComponent comp)
    {
        return ApplyColor(comp.Glyph.ToString(), comp.Foreground, comp.Background);
    }

    protected static string ApplyColor(string text)
    {
        return ApplyColor(text, Color.White, Color.Black);
    }

    protected static string ApplyColor(string text, Color fg)
    {
        return ApplyColor(text, fg);
    }

    protected static string ApplyColor(string text, Color fg, Color bg)
    {
        string fore = fg.ToASCII();
        string back = bg.ToASCII();
        string color = string.Format("\u001b[38;2;{0}m\u001b[48;2;{1}m", fore, back);
        return color + text;
    }
}
