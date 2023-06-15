
namespace SGE;

public interface Resource : IDisposable
{
    public string Path { get; }
}

public class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException() { }
    public ResourceNotFoundException(string message) : base(message) { }
    public ResourceNotFoundException(string message, Exception inner) : base(message, inner) { }
}

public interface Font : Resource { }

public interface Color : IDisposable { }

public interface Text : IDisposable
{
    public int Size { get; set; }
    public Font Font { get; set; }
    public Color Color { get; set; }
    public string Text { get; set; }
    public void Render(Position position);
}

public interface Sound : Resource
{
    public float Volume { get; set; }
    public bool IsPlaying { get; }
    public bool IsLoop { get; set; }
    public void Play();
    public void Pause();
    public void Stop();
}

public interface SpriteSheet
{
    public int Length { get; }
    public Image GetSprite(int index);
    public Dimension SpriteDimension { get; }
}

public interface Image : Resource
{
    public Dimension Dimension { get; }
    public void Render(Position position);
    public Image Resize(Dimension dimension);
    public Image Crop(Position position, Dimension dimension);
}

public abstract class Device
{
    public enum MouseButton { None = 100, Left = -1, Middle = 0, Right = 1 };
    public enum MouseWheelDirection { Backward = -1, Neutral = 0, Forward = 1 };
    public enum KeyboardModifier { None = 0, Shift = 0b001, Alt = 0b010, Ctrl = 0b100 };

    private Game _game;
    private int _framesPerSecond;
    private bool _fullScreen;

    private Dictionary<string, Resource> _resources;

    private Dictionary<int, Action<KeyboardModifier>> _onKeyDown;
    private Dictionary<int, Action<KeyboardModifier>> _onKeyUp;
    private Dictionary<MouseButton, Action<Position>> _onMouseDown;
    private Action<MouseWheelDirection>? _onMouseWheel;
    private Dictionary<MouseButton, Action<Position>> _onMouseUp;

    public Device(Game game)
    {
        _game = game;
        _framesPerSecond = 32;
        _fullScreen = false;
        _resources = new Dictionary<string, Resource>();
        _onKeyDown = new Dictionary<int, Action<KeyboardModifier>>();
        _onKeyUp = new Dictionary<int, Action<KeyboardModifier>>();
        _onMouseDown = new Dictionary<MouseButton, Action<Position>>();
        _onMouseUp = new Dictionary<MouseButton, Action<Position>>();
    }

    public abstract Dimension Dimension { get; }
    public abstract Position MousePosition { get; }

    public Game Game
    {
        get { return _game; }
        set
        {
            _onMouseWheel = null;
            _onMouseDown.Clear();
            _onMouseUp.Clear();
            _onKeyDown.Clear();
            _onKeyUp.Clear();
            _game = value;
        }
    }
    public bool FullScreen { get { return _fullScreen; } set { _fullScreen = value; } }
    public int FramesPerSecond { get { return _framesPerSecond; } set { _framesPerSecond = value; } }

    public abstract void Start();
    public abstract void Dispose();
    public void Loop() { _game.Loop(); }

    protected abstract Image LoadImageImpl(string path);
    protected abstract Sound LoadSoundImpl(string path);
    protected abstract Font LoadFontImpl(string path);

    protected T LoadResource<T>(string path, Func<string, Resource> loader)
    {
        if (!_resources.ContainsKey(path))
        {
            Resource loaded = loader(path);
            _resources.Add(path, loaded);
        }
        if (_resources[path] is T resource)
            return resource;
        else
            throw new ResourceNotFoundException(path);
    }

    public Image LoadImage(string path) { return LoadResource<Image>(path, LoadImageImpl); }
    public Sound LoadSound(string path) { return LoadResource<Sound>(path, LoadSoundImpl); }
    public Font LoadFont(string path) { return LoadResource<Font>(path, LoadFontImpl); }

    public abstract Color MakeColorFromName(string colorName);
    public abstract Color MakeColorFrom32Bits(int red, int green, int blue);
    public abstract Text MakeText(string text, Font font);
    public abstract SpriteSheet MakeSpriteSheet(Image img, Dimension dimension);

    private static void FireKey(int charInUTF16, KeyboardModifier modifiers, Dictionary<int, Action<KeyboardModifier>> events)
    {
        if (events.TryGetValue(charInUTF16, out Action<KeyboardModifier>? value))
            value.Invoke(modifiers);
    }
    protected void FireKeyUp(int charInUTF16, KeyboardModifier modifiers) { FireKey(charInUTF16, modifiers, _onKeyUp); }
    protected void FireKeyDown(int charInUTF16, KeyboardModifier modifiers) { FireKey(charInUTF16, modifiers, _onKeyDown); }
    private void FireMouse(MouseButton button, Dictionary<MouseButton, Action<Position>> events)
    {
        if (events.TryGetValue(button, out Action<Position>? value))
            value.Invoke(MousePosition);
    }
    protected void FireMouseUp(MouseButton button) { FireMouse(button, _onMouseUp); }
    protected void FireMouseDown(MouseButton button) { FireMouse(button, _onMouseDown); }
    protected void FireMouseWheel(MouseWheelDirection direction) { _onMouseWheel?.Invoke(direction); }

    public void RegisterKeyUp(int charInUTF16, Action<KeyboardModifier> command) { _onKeyUp.Add(charInUTF16, command); }
    public void RegisterKeyDown(int charInUTF16, Action<KeyboardModifier> command) { _onKeyDown.Add(charInUTF16, command); }
    public void RegisterMouseWheelScroll(Action<MouseWheelDirection> command) { _onMouseWheel = command; }
    public void RegisterMouseDown(MouseButton button, Action<Position> command) { _onMouseDown.Add(button, command); }
    public void RegisterMouseUp(MouseButton button, Action<Position> command) { _onMouseUp.Add(button, command); }
}
