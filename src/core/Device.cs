
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

public interface Device : IDisposable
{
    public enum MouseButton { None = 100, Left = -1, Middle = 0, Right = 1 };
    public enum MouseWheelDirection { Backward = -1, Neutral = 0, Forward = 1 };
    public enum KeyboardModifier { None = 0, Shift = 0b001, Alt = 0b010, Ctrl = 0b100 };

    public Game Game { get; set; }
    public Dimension Dimension { get; }
    public Position MousePosition { get; }
    public bool IsFullScreen { get; set; }
    public int FramesPerSecond { get; set; }

    public void Start();

    public Image MakeImage(string path);
    public Sound MakeSound(string path);
    public Font MakeFont(string path);
    public Color MakeColorFromName(string colorName);
    public Color MakeColorFrom32Bits(int red, int green, int blue);
    public Text MakeText(string text, Font font);
    public SpriteSheet MakeSpriteSheet(Image img, Dimension dimension);

    public void RegisterKeyUp(int charInUTF16, Action<KeyboardModifier> command);
    public void RegisterKeyDown(int charInUTF16, Action<KeyboardModifier> command);
    public void RegisterMouseWheelScroll(Action<MouseWheelDirection> command);
    public void RegisterMouseDown(MouseButton button, Action<Position> command);
    public void RegisterMouseUp(MouseButton button, Action<Position> command);
}


public class DeviceHelper
{
    private Game _game;
    private bool _isFullScreen;
    private int _framesPerSecond;
    private readonly Dimension _dimension;
    private readonly Position _mousePosition;

    private readonly Dictionary<string, Resource> _resources;
    private readonly Dictionary<int, Action<Device.KeyboardModifier>> _onKeyDown;
    private readonly Dictionary<int, Action<Device.KeyboardModifier>> _onKeyUp;
    private readonly Dictionary<Device.MouseButton, Action<Position>> _onMouseDown;
    private Action<Device.MouseWheelDirection>? _onMouseWheel;
    private readonly Dictionary<Device.MouseButton, Action<Position>> _onMouseUp;

    public DeviceHelper(Game game)
    {
        _game = game;
        _isFullScreen = false;
        _framesPerSecond = 32;
        _dimension = new Dimension(800, 600);
        _mousePosition = new Position();
        _resources = new Dictionary<string, Resource>();
        _onKeyDown = new Dictionary<int, Action<Device.KeyboardModifier>>();
        _onKeyUp = new Dictionary<int, Action<Device.KeyboardModifier>>();
        _onMouseDown = new Dictionary<Device.MouseButton, Action<Position>>();
        _onMouseUp = new Dictionary<Device.MouseButton, Action<Position>>();
    }

    public bool IsFullScreen { get => _isFullScreen; set => _isFullScreen = value; }
    public int FramesPerSecond { get => _framesPerSecond; set => _framesPerSecond = value; }
    public Dimension Dimesion { get => new(_dimension.Width, _dimension.Height); set => _dimension.Copy(value); }
    public Position MousePosition { get => new(_mousePosition.X, _mousePosition.Y); set => _mousePosition.Copy(value); }

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

    public T LoadResource<T>(string path, Func<string, Resource> loader)
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

    private static void FireKey(int charInUTF16, Device.KeyboardModifier modifiers, Dictionary<int, Action<Device.KeyboardModifier>> events)
    {
        if (events.TryGetValue(charInUTF16, out Action<Device.KeyboardModifier>? value))
            value.Invoke(modifiers);
    }
    public void FireKeyUp(int charInUTF16, Device.KeyboardModifier modifiers) { FireKey(charInUTF16, modifiers, _onKeyUp); }
    public void FireKeyDown(int charInUTF16, Device.KeyboardModifier modifiers) { FireKey(charInUTF16, modifiers, _onKeyDown); }
    private void FireMouse(Device.MouseButton button, Dictionary<Device.MouseButton, Action<Position>> events)
    {
        if (events.TryGetValue(button, out Action<Position>? value))
            value.Invoke(MousePosition);
    }
    public void FireMouseUp(Device.MouseButton button) { FireMouse(button, _onMouseUp); }
    public void FireMouseDown(Device.MouseButton button) { FireMouse(button, _onMouseDown); }
    public void FireMouseWheel(Device.MouseWheelDirection direction) { _onMouseWheel?.Invoke(direction); }

    public void RegisterKeyUp(int charInUTF16, Action<Device.KeyboardModifier> command) { _onKeyUp.Add(charInUTF16, command); }
    public void RegisterKeyDown(int charInUTF16, Action<Device.KeyboardModifier> command) { _onKeyDown.Add(charInUTF16, command); }
    public void RegisterMouseWheelScroll(Action<Device.MouseWheelDirection> command) { _onMouseWheel = command; }
    public void RegisterMouseDown(Device.MouseButton button, Action<Position> command) { _onMouseDown.Add(button, command); }
    public void RegisterMouseUp(Device.MouseButton button, Action<Position> command) { _onMouseUp.Add(button, command); }

}