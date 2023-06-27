
namespace SimpleGameEngine;

public class WindowsDevice : IDevice
{
    private readonly Form _form;
    private readonly System.Windows.Forms.Timer _timer;
    private readonly List<Action<Graphics>> _drawQueue;
    private readonly DeviceHelper _device;
    private bool _disposed;


    public Game Game { get => _device.Game; set => _device.Game = value; }
    public Point MousePosition { get => _device.MousePosition; }
    public Dimension Dimension { get => _device.Dimension; set => _device.Dimension = value; }
    public int FramesPerSecond
    {
        get => _device.FramesPerSecond;
        set
        {
            _timer.Interval = (int)(1000.0 / value);
            _device.FramesPerSecond = value;
        }
    }

    public bool IsFullScreen
    {
        get => _device.IsFullScreen;
        set
        {
            if (value && Screen.PrimaryScreen != null)
            {
                _form.WindowState = FormWindowState.Normal;
                _form.FormBorderStyle = FormBorderStyle.None;
                _form.Bounds = Screen.PrimaryScreen.Bounds;
            }
            else
            {
                _form.ClientSize = new Size(_device.Dimension.Width, _device.Dimension.Height);
                _form.FormBorderStyle = FormBorderStyle.Sizable;
            }
            _device.IsFullScreen = value;
        }
    }

    public WindowsDevice(Form form, DeviceHelper device)
    {
        _device = device;
        _disposed = false;
        _drawQueue = new List<Action<Graphics>>();

        _form = form;
        _form.Text = "Simple Game";
        _form.AutoScaleMode = AutoScaleMode.Font;
        _form.BackColor = Color.Black;
        //_form.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        //_form.DoubleBuffered = true;

        // Add event handlers
        _form.KeyDown += OnKeyDown;
        _form.KeyUp += OnKeyUp;
        _form.Paint += OnPaint;
        _form.MouseDown += OnMouseDown;
        _form.MouseUp += OnMouseUp;
        _form.MouseWheel += OnMouseWheelScroll;
        _form.MouseMove += OnMouseMove;
        _form.Load += (sender, e) => _form.Location = new System.Drawing.Point(500, 0);

        _timer = new System.Windows.Forms.Timer();
        _timer.Tick += OnLoop;

        IsFullScreen = false;
        FramesPerSecond = 32;
    }
    ~WindowsDevice() { Dispose(false); }

    public void Start()
    {
        _timer.Start();
        Game.Start();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _timer.Stop();
                _form.Close();
            }
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private static (int, KeyboardModifier) GetKeyboardMask(KeyEventArgs e)
    {
        int mask = 0;
        mask += e.Alt ? (int)KeyboardModifier.Alt : 0;
        mask += e.Shift ? (int)KeyboardModifier.Shift : 0;
        mask += e.Control ? (int)KeyboardModifier.Ctrl : 0;
        return (e.KeyValue, (KeyboardModifier)mask);
    }

    protected void OnKeyDown(object? sender, KeyEventArgs e)
    {
        (int key, KeyboardModifier mask) = GetKeyboardMask(e);
        _device.FireKeyDown(key, mask);
    }

    protected void OnKeyUp(object? sender, KeyEventArgs e)
    {
        (int key, KeyboardModifier mask) = GetKeyboardMask(e);
        _device.FireKeyUp(key, mask);
    }

    protected void OnMouseWheelScroll(object? sender, MouseEventArgs e)
    {
        MouseWheelDirection direction = MouseWheelDirection.Neutral;
        if (e.Delta > 0) direction = MouseWheelDirection.Forward;
        if (e.Delta < 0) direction = MouseWheelDirection.Backward;
        _device.FireMouseWheel(direction);
    }

    protected void OnMouseMove(object? sender, MouseEventArgs e)
    {
        _device.MousePosition = new Point(e.X, e.Y);
    }

    private static MouseButton GetMouseButton(MouseEventArgs e)
    {
        MouseButton button = 0;
        switch (e.Button)
        {
            case MouseButtons.Left: button = MouseButton.Left; break;
            case MouseButtons.Right: button = MouseButton.Right; break;
            case MouseButtons.Middle: button = MouseButton.Middle; break;
        }
        return button;
    }

    protected void OnMouseDown(object? sender, MouseEventArgs e)
    {
        MouseButton button = GetMouseButton(e);
        _device.FireMouseDown(button);
    }
    protected void OnMouseUp(object? sender, MouseEventArgs e)
    {
        MouseButton button = GetMouseButton(e);
        _device.FireMouseUp(button);
    }

    protected void OnPaint(object? sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        foreach (var paintCommand in _drawQueue)
            paintCommand.Invoke(g);
    }

    protected void OnLoop(object? sender, EventArgs e)
    {
        _drawQueue.Clear();
        _device.Game.Loop();
        _form.Invalidate();
    }

    public IFont MakeFont(string path)
    {
        //FontFamily fontFamily = new FontFamily(@"C:\Projects\MyProj\#free3of9");
        //The font name without the file extension, and keep the '#' symbol.
        try
        {
            return new WindowsFont(path);
        }
        catch (ArgumentException e)
        {
            throw new ResourceNotFoundException("Can not load the resource " + path + ".", e);
        }
    }

    public IImage MakeImage(string path)
    {
        try
        {
            return new WindowsImage(this, path);
        }
        catch (ArgumentException e)
        {
            throw new ResourceNotFoundException("Can not load the resource " + path + ".", e);
        }
    }

    public ISound MakeSound(string path)
    {
        try
        {
            return new WindowsSound(path);
        }
        catch (ArgumentException e)
        {
            throw new ResourceNotFoundException("Can not load the resource " + path + ".", e);
        }
    }

    public IColor MakeColor(int red, int green, int blue)
    {
        return new WindowsColor(red, green, blue);
    }

    public ISpriteSheet MakeSpriteSheet(IImage image, Dimension dimension)
    {
        if (image is WindowsImage imageWindows)
            return new WindowsSpriteSheet(this, imageWindows._bitmap, dimension);
        else throw new ResourceNotFoundException("Can not load the image in the windows plataform!");
    }

    public IText MakeText(string text, IFont font)
    {
        if (font is WindowsFont fontWindows)
        {
            var textWindows = new WindowsText(this, text) { Font = fontWindows };
            return textWindows;
        }
        else throw new ResourceNotFoundException("Can not load the font in the windows plataform!");
    }

    public void Render(Action<Graphics> render)
    {
        _drawQueue.Add(render);
    }

    public void RegisterKeyUp(int charInUTF16, Action<KeyboardModifier> command) { _device.RegisterKeyUp(charInUTF16, command); }
    public void RegisterKeyDown(int charInUTF16, Action<KeyboardModifier> command) { _device.RegisterKeyDown(charInUTF16, command); }
    public void RegisterMouseWheelScroll(Action<MouseWheelDirection> command) { _device.RegisterMouseWheelScroll(command); }
    public void RegisterMouseDown(MouseButton button, Action<Point> command) { _device.RegisterMouseDown(button, command); }
    public void RegisterMouseUp(MouseButton button, Action<Point> command) { _device.RegisterMouseUp(button, command); }

}
