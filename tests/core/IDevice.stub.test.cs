
namespace SimpleGameEngine.Test;

public class IDeviceStub : IDevice
{

    public Action<MouseButton, Action<Point>>? OnRegisterMouseUp;
    public Action<MouseButton, Action<Point>>? OnRegisterMouseDown;

    public Game Game { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Dimension Dimension => throw new NotImplementedException();
    public Point MousePosition => throw new NotImplementedException();
    public bool IsFullScreen { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public int FramesPerSecond { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public IColor MakeColor(int red8bits, int green8bits, int blue8bits, int alpha8bits = 255)
    {
        throw new NotImplementedException();
    }

    public IFont MakeFont(string path)
    {
        throw new NotImplementedException();
    }

    public IImage MakeImage(string path)
    {
        throw new NotImplementedException();
    }

    public ISound MakeSound(string path)
    {
        throw new NotImplementedException();
    }

    public ISpriteSheet MakeSpriteSheet(IImage img, Dimension dimension)
    {
        throw new NotImplementedException();
    }

    public IText MakeText(string text, IFont font)
    {
        throw new NotImplementedException();
    }

    public void RegisterKeyDown(int charInUTF16, Action<KeyboardModifier> command)
    {
        throw new NotImplementedException();
    }

    public void RegisterKeyUp(int charInUTF16, Action<KeyboardModifier> command)
    {
        throw new NotImplementedException();
    }

    public void RegisterMouseDown(MouseButton button, Action<Point> command)
    {
        OnRegisterMouseDown?.Invoke(button, command);
    }

    public void RegisterMouseUp(MouseButton button, Action<Point> command)
    {
        OnRegisterMouseUp?.Invoke(button, command);
    }

    public void RegisterMouseWheelScroll(Action<MouseWheelDirection> command)
    {
        throw new NotImplementedException();
    }

    public void Start()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
