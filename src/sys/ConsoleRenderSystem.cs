
public class Color {

    public static readonly Color Black = new Color("000000");
    public static readonly Color White = new Color("FFFFFF");
    public static readonly Color Blue = new Color("0000FF");
    public static readonly Color Red = new Color("FF0000");
    public static readonly Color Green = new Color("00FF00");
    public static readonly Color Yellow = new Color("FFFF00");
    public static readonly Color Grey = new Color("808080");
 
    protected internal string _value;

    public Color(string value){
        _value = value;
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
}

public class ConsoleRenderComponent
{

    public Position Pos;
    public char Glyph;
    public bool IsVisible;
    public bool HideCursor;

    public Color Foreground;
    public Color Background;

    public ConsoleRenderComponent(Position pos, char glyph, Color foreground){
        Pos = pos;
        Glyph = glyph;
        IsVisible = true;
        Foreground = foreground;
        Background = Color.Black;
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
}

public class ConsoleRenderSystem : SubSystem
{
    public bool ClearConsole;

    public override void Start()
    {

    }

    public override void Process()
    {

    }

    public override void Finish()
    {

    }
}
