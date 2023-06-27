
namespace SimpleGameEngine;

public enum MouseButton { Left = 1, Middle = 2, Right = 3 }

public enum MouseWheelDirection { Backward = -1, Neutral = 0, Forward = 1 }

[Flags]
public enum KeyboardModifier { None = 0, Shift = 1, Alt = 2, Ctrl = 4 }

public class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException() { }
    public ResourceNotFoundException(string message) : base(message) { }
    public ResourceNotFoundException(string message, Exception inner) : base(message, inner) { }
}

public interface IResource : IDisposable
{
    public string Path { get; }
}

public interface IFont : IResource { }

public interface IColor : IDisposable { }

public interface IText : IDisposable
{
    public int Size { get; set; }
    public IFont Font { get; set; }
    public IColor Color { get; set; }
    public string Text { get; set; }
    public void Render(Point position);
}

public interface ISound : IResource
{
    public float Volume { get; set; }
    public bool IsPlaying { get; }
    public bool IsLoop { get; set; }
    public void Play();
    public void Pause();
    public void Stop();
}

public interface ISpriteSheet
{
    public int Length { get; }
    public IImage GetSprite(int index);
    public Dimension SpriteDimension { get; }
}

public interface IImage : IResource
{
    public Dimension Dimension { get; }
    public void Render(Point position);
    public IImage Resize(Dimension dimension);
    public IImage Crop(Point position, Dimension dimension);
}

public interface IDevice : IDisposable
{
    public Game Game { get; set; }
    public Dimension Dimension { get; }
    public Point MousePosition { get; }
    public bool IsFullScreen { get; set; }
    public int FramesPerSecond { get; set; }

    public void Start();

    public IImage MakeImage(string path);
    public ISound MakeSound(string path);
    public IFont MakeFont(string path);
    public IColor MakeColor(int red8bits, int green8bits, int blue8bits, int alpha8bits = 255);
    public IText MakeText(string text, IFont font);
    public ISpriteSheet MakeSpriteSheet(IImage img, Dimension dimension);

    public void RegisterKeyUp(int charInUTF16, Action<KeyboardModifier> command);
    public void RegisterKeyDown(int charInUTF16, Action<KeyboardModifier> command);
    public void RegisterMouseWheelScroll(Action<MouseWheelDirection> command);
    public void RegisterMouseDown(MouseButton button, Action<Point> command);
    public void RegisterMouseUp(MouseButton button, Action<Point> command);
}
