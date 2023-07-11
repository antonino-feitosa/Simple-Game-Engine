
using System.IO;

namespace SimpleGameEngine;

public class DeviceHelper
{
    private Game? _game;
    private bool _isFullScreen;
    private int _framesPerSecond;
    private readonly Dimension _dimension;
    private readonly Point _mousePosition;

    private readonly Dictionary<string, IResource> _resources;
    private readonly Dictionary<int, List<Action<KeyboardModifier>>> _onKeyDown;
    private readonly Dictionary<int, List<Action<KeyboardModifier>>> _onKeyUp;
    private readonly Dictionary<MouseButton, List<Action<Point>>> _onMouseDown;
    private readonly List<Action<MouseWheelDirection>> _onMouseWheel;
    private readonly Dictionary<MouseButton, List<Action<Point>>> _onMouseUp;

    public bool IsFullScreen { get => _isFullScreen; set => _isFullScreen = value; }
    public int FramesPerSecond
    {
        get => _framesPerSecond;
        set
        {
            if (value > 0)
                _framesPerSecond = value;
            else
                throw new ArgumentOutOfRangeException(nameof(value), "The value must be positive");
        }
    }
    public Dimension Dimension { get => new(_dimension.Width, _dimension.Height); set => _dimension.Copy(value); }
    public Point MousePosition { get => new(_mousePosition.X, _mousePosition.Y); set => _mousePosition.Copy(value); }

    public Game Game
    {
        get { return _game ?? throw new ArgumentException("The game was not set!"); }
        set
        {
            _onMouseWheel.Clear();
            _onMouseDown.Clear();
            _onMouseUp.Clear();
            _onKeyDown.Clear();
            _onKeyUp.Clear();
            _game = value;
        }
    }

    public DeviceHelper()
    {
        _isFullScreen = false;
        _framesPerSecond = 32;
        _dimension = new Dimension(800, 600);
        _mousePosition = new Point();
        _resources = new Dictionary<string, IResource>();
        _onMouseWheel = new List<Action<MouseWheelDirection>>();
        _onKeyDown = new Dictionary<int, List<Action<KeyboardModifier>>>();
        _onKeyUp = new Dictionary<int, List<Action<KeyboardModifier>>>();
        _onMouseDown = new Dictionary<MouseButton, List<Action<Point>>>();
        _onMouseUp = new Dictionary<MouseButton, List<Action<Point>>>();
    }

    public T LoadResource<T>(string path, Func<string, IResource> loader) where T : IResource
    {
        if (!_resources.ContainsKey(path))
        {
            IResource loaded = loader(path);
            _resources.Add(path, loaded);
        }
        if (_resources[path] is T resource)
            return resource;
        else
            throw new FileNotFoundException(path);
    }

    private static void Fire<TEvent, TArg>(TEvent eventType, TArg argument, Dictionary<TEvent, List<Action<TArg>>> callback) where TEvent : notnull
    {
        if (callback.TryGetValue(eventType, out List<Action<TArg>>? callbacks)){
            foreach(var call in callbacks){
                call(argument);
            }
        }   
    }
    private static void Register<TEvent, TArg>(TEvent eventType, Action<TArg> command, Dictionary<TEvent, List<Action<TArg>>> callback) where TEvent : notnull
    {
        if (callback.TryGetValue(eventType, out List<Action<TArg>>? actions))
        {
            actions.Add(command);
        }
        else
        {
            actions = new List<Action<TArg>> { command };
            callback.Add(eventType, actions);
        }
    }

    
    public void FireKeyUp(int charInUTF16, KeyboardModifier modifiers) { Fire(charInUTF16, modifiers, _onKeyUp); }
    public void FireKeyDown(int charInUTF16, KeyboardModifier modifiers) { Fire(charInUTF16, modifiers, _onKeyDown); }
    public void FireMouseUp(MouseButton button) { Fire(button, MousePosition, _onMouseUp); }
    public void FireMouseDown(MouseButton button) { Fire(button, MousePosition, _onMouseDown); }
    public void FireMouseWheel(MouseWheelDirection direction) {
        foreach(var callback in _onMouseWheel){
            callback(direction);
        }
    }

    public void RegisterKeyUp(int charInUTF16, Action<KeyboardModifier> command) { Register(charInUTF16, command, _onKeyUp); }
    public void RegisterKeyDown(int charInUTF16, Action<KeyboardModifier> command) { Register(charInUTF16, command, _onKeyDown); }
    public void RegisterMouseDown(MouseButton button, Action<Point> command) { Register(button, command, _onMouseDown); }
    public void RegisterMouseUp(MouseButton button, Action<Point> command) { Register(button, command, _onMouseUp); }
    public void RegisterMouseWheelScroll(Action<MouseWheelDirection> command) { _onMouseWheel.Add(command); }
}
