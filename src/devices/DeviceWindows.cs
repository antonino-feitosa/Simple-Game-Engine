
namespace SGE;

public class DeviceWindows : Device
{
    private readonly Form _form;
    private readonly global::System.Windows.Forms.Timer _timer;
    private readonly List<Action<Graphics>> _drawQueue;
    private readonly DeviceHelper _device;
    private bool _disposed;

    public DeviceWindows(Form form, DeviceHelper device)
    {
        _device = device;
        _disposed = false;
        _drawQueue = new List<Action<Graphics>>();

        _form = form;
        _form.Text = "Simple Game";
        _form.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
        _form.BackColor = global::System.Drawing.Color.Black;
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
        _form.Load += (sender, e) => _form.Location = new Point(500, 0);

        _timer = new global::System.Windows.Forms.Timer();
        _timer.Tick += OnLoop;

        IsFullScreen = false;
        FramesPerSecond = 32;
    }
    ~DeviceWindows() { Dispose(false); }

    public Game Game { get => _device.Game; set => _device.Game = value; }
    public Position MousePosition { get => _device.MousePosition; }
    public Dimension Dimension { get => _device.Dimesion; set => _device.Dimesion = value; }
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
                _form.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.None;
                _form.Bounds = Screen.PrimaryScreen.Bounds;
            }
            else
            {
                _form.ClientSize = new Size(_device.Dimesion.Width, _device.Dimesion.Height);
                _form.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Sizable;
            }
            _device.IsFullScreen = value;
        }
    }

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

    private static (int, Device.KeyboardModifier) GetKeyboardMask(KeyEventArgs e)
    {
        int mask = 0;
        mask += e.Alt ? (int)Device.KeyboardModifier.Alt : 0;
        mask += e.Shift ? (int)Device.KeyboardModifier.Shift : 0;
        mask += e.Control ? (int)Device.KeyboardModifier.Ctrl : 0;
        return (e.KeyValue, (Device.KeyboardModifier)mask);
    }

    protected void OnKeyDown(object? sender, KeyEventArgs e)
    {
        (int key, Device.KeyboardModifier mask) = GetKeyboardMask(e);
        _device.FireKeyDown(key, mask);
    }

    protected void OnKeyUp(object? sender, KeyEventArgs e)
    {
        (int key, Device.KeyboardModifier mask) = GetKeyboardMask(e);
        _device.FireKeyUp(key, mask);
    }

    protected void OnMouseWheelScroll(object? sender, MouseEventArgs e)
    {
        Device.MouseWheelDirection direction = Device.MouseWheelDirection.Neutral;
        if (e.Delta > 0) direction = Device.MouseWheelDirection.Forward;
        if (e.Delta < 0) direction = Device.MouseWheelDirection.Backward;
        _device.FireMouseWheel(direction);
    }

    protected void OnMouseMove(object? sender, MouseEventArgs e)
    {
        _device.MousePosition = new Position(e.X, e.Y);
    }

    private static Device.MouseButton GetMouseButton(MouseEventArgs e)
    {
        Device.MouseButton button = Device.MouseButton.None;
        switch (e.Button)
        {
            case MouseButtons.Left: button = Device.MouseButton.Left; break;
            case MouseButtons.Right: button = Device.MouseButton.Right; break;
            case MouseButtons.Middle: button = Device.MouseButton.Middle; break;
        }
        return button;
    }

    protected void OnMouseDown(object? sender, MouseEventArgs e)
    {
        Device.MouseButton button = GetMouseButton(e);
        _device.FireMouseDown(button);
    }
    protected void OnMouseUp(object? sender, MouseEventArgs e)
    {
        Device.MouseButton button = GetMouseButton(e);
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

    public Font MakeFont(string path)
    {
        //FontFamily fontFamily = new FontFamily(@"C:\Projects\MyProj\#free3of9");
        //The font name without the file extension, and keep the '#' symbol.
        try
        {
            return new FontWindows(path);
        }
        catch (ArgumentException e)
        {
            throw new ResourceNotFoundException("Can not load the resource " + path + ".", e);
        }
    }

    public Image MakeImage(string path)
    {
        try
        {
            return new ImageWindows(this, path);
        }
        catch (ArgumentException e)
        {
            throw new ResourceNotFoundException("Can not load the resource " + path + ".", e);
        }
    }

    public Sound MakeSound(string path)
    {
        try
        {
            return new SoundWindows(path);
        }
        catch (ArgumentException e)
        {
            throw new ResourceNotFoundException("Can not load the resource " + path + ".", e);
        }
    }

    public Color MakeColorFrom32Bits(int red, int green, int blue)
    {
        return ColorWindows.FromRGB(red, green, blue);
    }

    public Color MakeColorFromName(string name)
    {
        return ColorWindows.FromName(name);
    }

    public SpriteSheet MakeSpriteSheet(Image image, Dimension dimension)
    {
        if (image is ImageWindows imageWindows)
            return new SpriteSheetWindows(this, imageWindows._bitmap, dimension);
        else throw new ResourceNotFoundException("Can not load the image in the windows plataform!");
    }

    public Text MakeText(string text, Font font)
    {
        if (font is FontWindows fontWindows)
        {
            var textWindows = new TextWindows(this, text) { Font = fontWindows };
            return textWindows;
        }
        else throw new ResourceNotFoundException("Can not load the font in the windows plataform!");
    }

    public void Render(Action<Graphics> render)
    {
        _drawQueue.Add(render);
    }

    public void RegisterKeyUp(int charInUTF16, Action<Device.KeyboardModifier> command) { _device.RegisterKeyUp(charInUTF16, command); }
    public void RegisterKeyDown(int charInUTF16, Action<Device.KeyboardModifier> command) { _device.RegisterKeyDown(charInUTF16, command); }
    public void RegisterMouseWheelScroll(Action<Device.MouseWheelDirection> command) { _device.RegisterMouseWheelScroll(command); }
    public void RegisterMouseDown(Device.MouseButton button, Action<Position> command) { _device.RegisterMouseDown(button, command); }
    public void RegisterMouseUp(Device.MouseButton button, Action<Position> command) { _device.RegisterMouseUp(button, command); }

}

internal class ColorWindows : Color
{
    public global::System.Drawing.SolidBrush _drawBrush;

    public ColorWindows()
    {
        var color = global::System.Drawing.Color.FromName("black");
        _drawBrush = new global::System.Drawing.SolidBrush(color);
    }

    private ColorWindows(global::System.Drawing.Color color)
    {
        _drawBrush = new global::System.Drawing.SolidBrush(color);
    }

    public static Color FromName(string name)
    {
        var color = global::System.Drawing.Color.FromName(name);
        return new ColorWindows(color);
    }

    public static Color FromRGB(int red, int green, int blue)
    {
        var color = global::System.Drawing.Color.FromArgb(red, green, blue);
        return new ColorWindows(color);
    }

    public void Dispose()
    {
        _drawBrush.Dispose();
    }
}

internal class FontWindows : Font
{
    private readonly string _path;
    internal readonly FontFamily _fontFamily;

    public FontWindows()
    {
        //FontFamily fontFamily = new FontFamily(@"C:\Projects\MyProj\#free3of9");
        //The font name without the file extension, and keep the '#' symbol.
        _path = "Arial";
        _fontFamily = new FontFamily("Arial");
    }

    public FontWindows(string path)
    {
        _path = path;
        _fontFamily = new FontFamily(path);
    }

    public string Path { get => _path; }

    public void Dispose()
    {
        _fontFamily.Dispose();
    }
}

public class TextWindows : Text
{
    private readonly DeviceWindows _windows;
    private string _text;
    private int _size;
    private ColorWindows _color;
    private FontWindows _font;

    internal global::System.Drawing.Font _drawFont;
    private bool _disposed;

    public TextWindows(DeviceWindows windows, string text)
    {
        _disposed = false;
        _windows = windows;
        _text = text;
        _size = 12;
        _color = new ColorWindows();
        _font = new FontWindows();
        _drawFont = new global::System.Drawing.Font(_font._fontFamily, _size);
    }
    ~TextWindows()
    {
        Dispose(false);
    }

    public string Text { get => _text; set => _text = value; }
    public int Size { get => _size; set { _size = value; UpdateDraw(); } }
    public Font Font { get => _font; set => _font = (FontWindows)value; }
    public Color Color { get => _color; set => _color = (ColorWindows)value; }

    private void UpdateDraw()
    {
        _drawFont.Dispose();
        _drawFont = new global::System.Drawing.Font(_font._fontFamily, _size);
    }

    public void Render(Position position)
    {
        int x = position.X;
        int y = position.Y;
        var text = _text;
        var drawFont = _drawFont;
        var drawBrush = _color._drawBrush;
        var drawFormat = new global::System.Drawing.StringFormat();
        void render(Graphics g) => g.DrawString(text, drawFont, drawBrush, x, y, drawFormat);
        _windows.Render(render);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _drawFont.Dispose();
            }
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}

internal class ImageWindows : Image
{
    protected internal readonly Bitmap _bitmap;
    private readonly DeviceWindows _windows;
    private readonly Dimension _dimension;
    private readonly string _path;

    public ImageWindows(DeviceWindows windows, string path)
    {
        _windows = windows;
        _path = path;
        _bitmap = new Bitmap(path);
        _dimension = new Dimension(_bitmap.Width, _bitmap.Height);
    }
    public ImageWindows(DeviceWindows windows, Bitmap bitmap)
    {
        _windows = windows;
        _path = "custom";
        _bitmap = bitmap;
        _dimension = new Dimension(bitmap.Width, bitmap.Height);
    }
    public string Path { get => _path; }
    public Dimension Dimension { get => _dimension; }

    public void Render(Position position)
    {
        int x = position.X;
        int y = position.Y;
        _windows.Render((Graphics g) => g.DrawImage(_bitmap, x, y));
    }

    public Image Crop(Position position, Dimension dimension)
    {
        var cropped = new Bitmap(dimension.Width, dimension.Height);
        var graphics = Graphics.FromImage(cropped);
        graphics.DrawImageUnscaledAndClipped(_bitmap, new Rectangle(position.X, position.Y, cropped.Width, cropped.Height));
        return new ImageWindows(_windows, cropped);
    }

    public Image Resize(Dimension dimension)
    {
        var resized = new Bitmap(dimension.Width, dimension.Height);
        var graphics = Graphics.FromImage(resized);
        graphics.DrawImage(_bitmap, new Rectangle(0, 0, resized.Width, resized.Height));
        return new ImageWindows(_windows, resized);
    }

    public void Dispose()
    {
        _bitmap.Dispose();
    }
}

internal class SpriteSheetWindows : SpriteSheet
{
    private readonly DeviceWindows _windows;
    private readonly Dimension _dimension;
    private readonly List<Image> _sprites;
    public SpriteSheetWindows(DeviceWindows windows, Bitmap sheet, Dimension dimension)
    {
        _windows = windows;
        _dimension = dimension;
        _sprites = Split(sheet, _dimension);
    }
    public int Length { get => _sprites.Count; }

    public Dimension SpriteDimension { get => _dimension; }

    public Image GetSprite(int index)
    {
        if (index < 0 || index >= _sprites.Count)
            throw new ArgumentException(String.Format("The index {0} does not exist!", index));
        return _sprites[index];
    }

    private List<Image> Split(Bitmap bitmap, Dimension dimension)
    {
        var frames = new List<Image>();
        int x_count = bitmap.Width / dimension.Width;
        int y_count = bitmap.Height / dimension.Height;
        var rect = new Rectangle(0, 0, dimension.Width, dimension.Height);
        for (int y = 0; y < y_count; y++)
        {
            for (int x = 0; x < x_count; x++)
            {
                var splitted = new Bitmap(dimension.Width, dimension.Height);
                var graphics = Graphics.FromImage(splitted);
                var dest = new Rectangle(x * dimension.Width, y * dimension.Height, dimension.Width, dimension.Height);
                graphics.DrawImage(bitmap, rect, dest, GraphicsUnit.Pixel);
                graphics.Dispose();
                var sprite = new ImageWindows(_windows, splitted);
                frames.Add(sprite);
            }
        }
        return frames;
    }
}

internal class SoundWindows : Sound
{
    private bool _isLoop;
    private readonly string _path;
    private readonly NAudio.Wave.AudioFileReader _audioFile;
    private readonly NAudio.Wave.WaveOutEvent _waveOut;
    private readonly EventHandler<NAudio.Wave.StoppedEventArgs> _loopCallback;

    public SoundWindows(string path)
    {
        _path = path;
        _isLoop = false;
        _audioFile = new NAudio.Wave.AudioFileReader(path);
        _waveOut = new NAudio.Wave.WaveOutEvent();
        _loopCallback = (sender, args) => _waveOut.Play();
    }
    public float Volume { get => _waveOut.Volume; set => _waveOut.Volume = value > 1 ? 1 : (value < 0 ? 0 : value); }
    public string Path { get => _path; }
    public bool IsPlaying { get => _waveOut.PlaybackState == NAudio.Wave.PlaybackState.Playing; }

    public bool IsLoop
    {
        get => _isLoop;
        set
        {
            if (value) _waveOut.PlaybackStopped += _loopCallback;
            else _waveOut.PlaybackStopped -= _loopCallback;
            _isLoop = value;
        }
    }

    public void Play()
    {
        _waveOut.Play();
    }

    public void Pause()
    {
        _waveOut.Pause();
    }

    public void Stop()
    {
        _waveOut.Stop();
    }

    public void Dispose()
    {
        _waveOut.Stop();
        _waveOut.Dispose();
        _audioFile.Dispose();
    }
}
