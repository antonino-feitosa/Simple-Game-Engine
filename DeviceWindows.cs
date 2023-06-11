
using Timer = System.Windows.Forms.Timer;

namespace SGE;

public class DeviceWindows : Device
{
    private WindowsAdapter _windows;
    private Position _mousePosition;
    public DeviceWindows(Game game) : base(game)
    {
        _mousePosition = new Position();
        _windows = new WindowsAdapter();
        _windows.RegisterMouseMove += (Position point) => { _mousePosition.Copy(point); };
        _windows.RegisterMouseDown += FireMouseDown;
        _windows.RegisterMouseUp += FireMouseUp;
        _windows.RegisterMouseScroll += FireMouseWheel;
        _windows.RegisterKeyDown += FireKeyDown;
        _windows.RegisterKeyUp += FireKeyUp;
        _windows.RegisterLoop += Loop;
    }

    public override Dimension Dimension => _windows.Dimension;
    public override Position MousePosition { get { return _mousePosition; } }

    public override void Start()
    {
        _windows.Start();
    }

    public override void Dispose()
    {
        _windows.Stop();
    }
    public override Color MakeColorFromName(string colorName)
    {
        return _windows.MakeColorFromName(colorName);
    }

    public override Color MakeColorFrom32Bits(int red, int green, int blue)
    {
        return _windows.MakeColorFrom32Bits(red, green, blue);
    }

    public override SpriteSheet MakeSpriteSheet(Image img, Dimension dimension)
    {
    }

    public override Text MakeText(string text, Font font)
    {
    }

    protected override Font LoadFontImpl(string path)
    {
        return _windows.LoadFont(path);
    }

    protected override Image LoadImageImpl(string path)
    {
        return _windows.LoadImage(path);
    }

    protected override Sound LoadSoundImpl(string path)
    {
        return _windows.LoadSound(path);
    }
}


public partial class WindowsAdapter : Form
{
    public Action<int, Device.KeyboardModifier>? RegisterKeyDown;
    public Action<int, Device.KeyboardModifier>? RegisterKeyUp;
    public Action<Position>? RegisterMouseMove;
    public Action<Device.MouseButton>? RegisterMouseDown;
    public Action<Device.MouseButton>? RegisterMouseUp;
    public Action<Device.MouseWheelDirection>? RegisterMouseScroll;
    public Action? RegisterLoop;

    private Timer _timer;
    private Size _dimension;

    public WindowsAdapter(string title = "Game", int framesPerSecond = 32)
    {
        _dimension = new Size(800, 600);

        InitializeComponent();

        // Set the form's properties
        Text = title;
        BackColor = global::System.Drawing.Color.Black;
        WindowState = FormWindowState.Normal;
        FormBorderStyle = FormBorderStyle.None;
        DoubleBuffered = true;
        FullScreen = false;

        // Add event handlers
        KeyDown += FireKeyDown;
        KeyUp += FireKeyUp;
        Paint += FirePaint;
        MouseDown += FireMouseDown;
        MouseUp += FireMouseUp;
        MouseWheel += FireMouseWheelScroll;
        MouseMove += FireMouseMove;
        Load += (sender, e) => Location = new Point(500, 0);

        _timer = new Timer();
        _timer.Interval = (int)(1000.0 / framesPerSecond);
        _timer.Tick += FireLoop;
    }

    public void Start()
    {
        _timer.Start();
    }

    public void Stop()
    {
        _timer.Stop();
        Close();
    }

    public bool FullScreen
    {
        set
        {
            if (value && Screen.PrimaryScreen != null)
            {
                WindowState = FormWindowState.Normal;
                FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.None;
                Bounds = Screen.PrimaryScreen.Bounds;
            }
            else
            {
                ClientSize = _dimension;
                FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Sizable;
            }
        }
    }

    public Dimension Dimension
    {
        get
        {
            return new Dimension(_dimension.Width, _dimension.Height);
        }
        set
        {
            _dimension.Width = value.Width;
            _dimension.Height = value.Height;
        }
    }

    public int FramesPerSecond
    {
        set
        {
            this._timer.Interval = (int)(1000.0 / value);
        }
    }

    private (int, Device.KeyboardModifier) GetKeyboardMask(KeyEventArgs e)
    {
        int mask = 0;
        mask += e.Alt ? (int)Device.KeyboardModifier.Alt : 0;
        mask += e.Shift ? (int)Device.KeyboardModifier.Shift : 0;
        mask += e.Control ? (int)Device.KeyboardModifier.Ctrl : 0;
        return (e.KeyValue, (Device.KeyboardModifier)mask);
    }

    protected void FireKeyDown(object? sender, KeyEventArgs e)
    {
        (int key, Device.KeyboardModifier mask) = GetKeyboardMask(e);
        RegisterKeyDown?.Invoke(key, mask);
    }

    protected void FireKeyUp(object? sender, KeyEventArgs e)
    {
        (int key, Device.KeyboardModifier mask) = GetKeyboardMask(e);
        RegisterKeyUp?.Invoke(key, mask);
    }

    protected void FireMouseWheelScroll(object? sender, MouseEventArgs e)
    {
        Device.MouseWheelDirection direction = Device.MouseWheelDirection.Neutral;
        if (e.Delta > 0) direction = Device.MouseWheelDirection.Forward;
        if (e.Delta < 0) direction = Device.MouseWheelDirection.Backward;
        RegisterMouseScroll?.Invoke(direction);
    }

    protected void FireMouseMove(object? sender, MouseEventArgs e)
    {
        RegisterMouseMove?.Invoke(new Position(e.X, e.Y));
    }

    private Device.MouseButton GetMouseButton(MouseEventArgs e)
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

    protected void FireMouseDown(object? sender, MouseEventArgs e)
    {
        Device.MouseButton button = GetMouseButton(e);
        RegisterMouseDown?.Invoke(button);
    }
    protected void FireMouseUp(object? sender, MouseEventArgs e)
    {
        Device.MouseButton button = GetMouseButton(e);
        RegisterMouseUp?.Invoke(button);
    }

    protected void FirePaint(object? sender, PaintEventArgs e)
    {
        //TODO draw images and text
        
    }

    protected void FireLoop(object? sender, EventArgs e)
    {
        RegisterLoop?.Invoke();
        Invalidate();
    }

    public Font LoadFont(string path)
    {
    }

    public Image LoadImage(string path)
    {
    }

    public Sound LoadSound(string path)
    {
    }

    public Color MakeColorFrom32Bits(int red, int green, int blue)
    {
        return new ColorWindows(red, green, blue);
    }

    public Color MakeColorFromName(string name)
    {
        return new ColorWindows(name);
    }
}

internal class ColorWindows : Color
{

    global::System.Drawing.Color _color;

    public ColorWindows(string name)
    {
        _color = global::System.Drawing.Color.FromName(name);
    }

    public ColorWindows(int red, int green, int blue)
    {
        _color = global::System.Drawing.Color.FromArgb(red, green, blue);
    }
}

internal class FontWindows : Font
{
    private string _path;
    public FontWindows(string path){
        _path = path;
    }
    public int Size { get => _path }

    public string Path => throw new NotImplementedException();

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}

//--------------------------------------
/// REFACTOR


public class TextWindows : Text
{
    protected string _text;
    protected int _size;
    protected string _font;
    protected string _color;
    protected internal DeviceWindows _device;

    public string Text { get => _text; set => _text = value; }
    public int Size { get => _size; set => _size = value; }
    public string Font { get => _font; set => _font = value; }
    public string Color { get => _color; set => _color = value; }

    public TextWindows(string text, string font, DeviceWindows device)
    {
        _text = text;
        _font = font;
        _size = 12;
        _color = "white";
        _device = device;
    }

    public void Render(int x, int y)
    {
        _device.Render(this, x, y);
    }
}

public class SoundWindows : Sound
{
    private static int _countId = 0;
    private readonly int _id;

    private string _path;
    protected internal SoundPlayer _sound;
    protected internal DeviceWindows _game;

    protected internal SoundWindows(string path, SoundPlayer sound, DeviceWindows game)
    {
        _id = _countId++;
        _path = path;
        _sound = sound;
        _game = game;
    }

    public double Volume
    {
        get { return 100; }
        set { }
    }

    public string Path
    {
        get { return Path; }
    }

    public void Play()
    {
        _sound.Play();
    }

    public void Loop()
    {
        _sound.PlayLooping();
    }

    public void Pause()
    {
    }

    public void Stop()
    {
        _sound.Stop();
    }

    public override bool Equals(object? obj) { return obj is SoundWindows sound ? sound._id == _id : base.Equals(obj); }

    public override int GetHashCode() { return HashCode.Combine(_id); }

    public override string ToString() { return "Sound(" + Path + ")"; }

    private static void DoLoop(object? sender, EventArgs e)
    {
        if (sender != null)
        {
            MediaPlayer mediaPlayer = (MediaPlayer)sender;
            mediaPlayer.Position = TimeSpan.Zero;
            mediaPlayer.Play();
        }
    }
}

public class SpriteSheetWindows : SpriteSheet
{
    private static int _countId = 0;
    private readonly int _id;
    private string _path;
    private int _width;
    private int _height;

    protected internal List<Image> _sheet;
    protected internal Bitmap _bitmap;
    protected internal DeviceWindows _device;

    protected internal SpriteSheetWindows(string path, Bitmap bitmap, DeviceWindows device)
    {
        _id = _countId++;
        _path = path;
        _bitmap = bitmap;
        _width = 0;
        _height = 0;
        _device = device;
        _sheet = new List<Image>();
    }

    public string Path { get { return _path; } }
    public int Width { get { return _width; } }
    public int Height { get { return _height; } }
    public int Length { get { return _sheet.Count; } }

    protected internal void Split(int width, int height)
    {
        _width = width;
        _height = height;
        int x_count = (int)(_bitmap.Width / width);
        int y_count = (int)(_bitmap.Height / height);
        var rect = new Rectangle(0, 0, width, height);
        for (int y = 0; y < y_count; y++)
        {
            for (int x = 0; x < x_count; x++)
            {
                var index = y * x_count + x;
                var splitted = new Bitmap(width, height);
                var graphics = Graphics.FromImage(splitted);
                var dest = new Rectangle(x * width, y * height, width, height);
                var img = new ImageWindows("", splitted, _device);
                _sheet.Add(img);
                graphics.DrawImage(_bitmap, rect, dest, GraphicsUnit.Pixel);
                graphics.Dispose();
            }
        }
    }

    public Image GetImage(int index)
    {
        if (index < 0 || index >= _sheet.Count)
            throw new ArgumentException(String.Format("The index {0} does not exist!", index));
        return _sheet[index];
    }

    public override bool Equals(object? obj) { return obj is SpriteSheetWindows ss ? ss._id == _id : base.Equals(obj); }

    public override int GetHashCode() { return HashCode.Combine(_id); }

    public override string ToString() { return "Image(" + Path + ", " + Width + "x" + Height + ")"; }
}

public class ImageWindows : Image
{
    private static int _countId = 0;
    private readonly int _id;
    private string _path;

    protected internal Bitmap _bitmap;
    protected internal DeviceWindows _device;

    protected internal ImageWindows(string path, Bitmap bitmap, DeviceWindows device)
    {
        _id = _countId++;
        _path = path;
        _bitmap = bitmap;
        _device = device;
    }

    public string Path { get { return _path; } }
    public int Width { get { return _bitmap.Width; } }
    public int Height { get { return _bitmap.Height; } }

    public void Render(int x, int y)
    {
        _device.Render(_bitmap, x, y);
    }

    public Image Resize(int width, int height)
    {
        var bmp = new Bitmap(width, height);
        var graphics = Graphics.FromImage(bmp);
        graphics.DrawImage(_bitmap, new Rectangle(0, 0, bmp.Width, bmp.Height));
        return new ImageWindows(Path + "_resized(" + width + "," + height + ")", bmp, _device);
    }

    public Image Crop(int x, int y, int width, int height)
    {
        var bmp = new Bitmap(width, height);
        var graphics = Graphics.FromImage(bmp);
        graphics.DrawImageUnscaledAndClipped(_bitmap, new Rectangle(x, y, bmp.Width, bmp.Height));
        return new ImageWindows(Path + "_cropped(" + width + "," + height + ")", bmp, _device);
    }

    public override bool Equals(object? obj) { return obj is ImageWindows img ? img._id == _id : base.Equals(obj); }

    public override int GetHashCode() { return HashCode.Combine(_id); }

    public override string ToString() { return "Image(" + Path + ", " + Width + "x" + Height + ")"; }
}

internal interface Render
{
    public void Render(Graphics g);
}

internal class DrawCommand : Render
{
    public Bitmap _bitmap;
    public Point _point;
    public DrawCommand(Bitmap bitmap, Point point)
    {
        _bitmap = bitmap;
        _point = point;
    }

    public void Render(Graphics g)
    {
        g.DrawImage(_bitmap, _point);
    }
}

internal class TextCommand : Render
{

    public Text _text;
    public Point _point;

    public TextCommand(Text text, Point point)
    {
        _text = text;
        _point = point;
    }

    public void Render(Graphics g)
    { // TODO optimization point: shared references
        global::System.Drawing.Font drawFont = new global::System.Drawing.Font(_text.Font, _text.Size);
        global::System.Drawing.Color color = global::System.Drawing.Color.FromName(_text.Color);
        global::System.Drawing.SolidBrush drawBrush = new global::System.Drawing.SolidBrush(color);
        global::System.Drawing.StringFormat drawFormat = new global::System.Drawing.StringFormat();
        g.DrawString(_text.Text, drawFont, drawBrush, _point.X, _point.Y, drawFormat);
        drawFont.Dispose();
        drawBrush.Dispose();
    }
}



public partial class DeviceWindows1 : Form, Device
{

    private LinkedList<Render> _renderCommands;
    private LinkedList<Render> _bufferCommands;

    public override int FramesPerSecond
    {
        set
        {
            _framesPerSeconds = value;
            this.timer.Interval = (int)(1000 / _framesPerSeconds);
        }
    }

    private Timer timer;

    public DeviceWindows(Game game)
    {
        _current = game;
        _images = new Dictionary<string, ImageWindows>();
        _sounds = new Dictionary<string, SoundWindows>();
        _spriteSheets = new Dictionary<string, SpriteSheetWindows>();
        _renderCommands = new LinkedList<Render>();
        _bufferCommands = new LinkedList<Render>();
        _keyDownCommands = new Dictionary<char, Action<int>>();
        _keyUpCommands = new Dictionary<char, Action<int>>();
        _onMouseDown = new Dictionary<char, Action<int, int>>();
        _onMouseUp = new Dictionary<char, Action<int, int>>();

        InitializeComponent();

        // Set the form's properties
        this.Text = "Game";
        this.BackColor = global::System.Drawing.Color.Black;
        this.WindowState = FormWindowState.Normal;
        this.FormBorderStyle = FormBorderStyle.None;
        this.DoubleBuffered = true;
        FullScreen = false;

        // Add event handlers
        this.KeyDown += DoKeyDown;
        this.KeyUp += DoKeyUp;
        this.Paint += DoPaint;
        this.MouseDown += DoMouseDown;
        this.MouseUp += DoMouseUp;
        this.MouseWheel += DoMouseWheel;
        this.Load += (sender, e) => this.Location = new Point(500, 0);
        this._onLoop += () => { };

        this.timer = new Timer();
        this.timer.Interval = (int)(1000 / _framesPerSeconds);
        this.timer.Tick += DoLoop;
        this.timer.Start();
    }

    public bool FullScreen
    {
        get { return _fullScreen; }
        set
        {
            _fullScreen = value;
            if (_fullScreen && Screen.PrimaryScreen != null)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.None;
                this.Bounds = Screen.PrimaryScreen.Bounds;
            }
            else
            {
                this.ClientSize = new Size(800, 600);
                this.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Sizable;
            }
        }
    }

    public new (int, int) MousePosition
    {
        get { Point p = PointToClient(Control.MousePosition); return (p.X, p.Y); }
    }

    public void RegisterKeyUp(char c, Action<int> command)
    {
        c = Char.ToUpper(c);
        _keyUpCommands.Add(c, command);
    }

    public void RegisterKeyDown(char c, Action<int> command)
    {
        c = Char.ToUpper(c);
        _keyDownCommands.Add(c, command);
    }

    public void RegisterMouseWheel(Action<int> command)
    {
        _onMouseWheel += command;
    }

    // L, R, M
    public void RegisterMouseDown(char button, Action<int, int> command)
    {
        if (button == 'L' || button == 'R' || button == 'M')
            _onMouseDown.Add(button, command);
    }
    public void RegisterMouseUp(char button, Action<int, int> command)
    {
        if (button == 'L' || button == 'R' || button == 'M')
            _onMouseUp.Add(button, command);
    }

    private void DoKey(KeyEventArgs e, Dictionary<char, Action<int>> events)
    {
        char c = (char)e.KeyCode;
        int mask = 0;
        mask += e.Alt ? 1 : 0;
        mask += e.Shift ? 10 : 0;
        mask += e.Control ? 100 : 0;
        if (events.ContainsKey(c))
            events[c].Invoke(mask);
    }

    protected void DoKeyDown(object? sender, KeyEventArgs e)
    {
        Console.WriteLine((char)e.KeyCode);
        DoKey(e, _keyDownCommands);
    }

    protected void DoKeyUp(object? sender, KeyEventArgs e)
    {
        DoKey(e, _keyUpCommands);
    }

    protected void DoMouseWheel(object? sender, MouseEventArgs e)
    {
        _onMouseWheel?.Invoke(Math.Sign(e.Delta));
    }

    private void DoMouse(MouseEventArgs e, Dictionary<char, Action<int, int>> events)
    {
        char button = ' ';
        switch (e.Button)
        {
            case MouseButtons.Left: button = 'L'; break;
            case MouseButtons.Right: button = 'R'; break;
            case MouseButtons.Middle: button = 'M'; break;
        }
        Point clientPosition = e.Location;
        if (events.ContainsKey(button))
            events[button]?.Invoke(clientPosition.X, clientPosition.Y);
    }

    protected void DoMouseDown(object? sender, MouseEventArgs e)
    {
        DoMouse(e, _onMouseDown);
    }
    protected void DoMouseUp(object? sender, MouseEventArgs e)
    {
        DoMouse(e, _onMouseUp);
    }

    protected void DoPaint(object? sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        foreach (var cmd in _renderCommands) { cmd.Render(g); }
    }

    public void Start()
    {
        this.timer.Start();
    }

    public void RegisterLoop(Action loop, int fps = 32)
    {
        _onLoop += loop;
        this.timer.Interval = (int)(1000 / fps);
    }

    protected void DoLoop(object? sender, EventArgs e)
    {
        var aux = _bufferCommands;
        _bufferCommands = _renderCommands;
        _renderCommands = aux;
        _bufferCommands.Clear();
        _onLoop?.Invoke();
        Invalidate();
    }

    public void Dispose()
    {
        this.timer.Stop();
        this.Close();
    }

    public Image LoadImage(string path)
    {
        if (!_images.ContainsKey(path))
        {
            try
            {
                var bitmap = new Bitmap(path);
                var image = new ImageWindows(path, bitmap, this);
                _images.Add(path, image);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(String.Format("Image {0} not found!", path));
                throw e;
            }
        }
        if (_images[path] is Image img)
        {
            return img;
        }
        throw new ArgumentException(String.Format("Image {0} not found!", path));
    }

    public SpriteSheet LoadSpriteSheet(Image img, int width, int height)
    {
        if (img is ImageWindows win)
        {
            var bitmap = win._bitmap;
            var sheet = new SpriteSheetWindows(img.Path, bitmap, this);
            sheet.Split(width, height);
            _spriteSheets.Add(img.Path, sheet);
            if (_spriteSheets[img.Path] is SpriteSheetWindows ss)
            {
                return ss;
            }
        }
        throw new ArgumentException(String.Format("Image {0} not found!", img));
    }

    public Sound LoadSound(string path)
    {
        if (!_sounds.ContainsKey(path))
        {
            var player = new SoundPlayer(path);
            var sound = new SoundWindows(path, player, this);
            _sounds.Add(path, sound);
        }
        if (_sounds[path] is Sound snd)
        {
            return snd;
        }
        throw new ArgumentException(String.Format("Sound {0} not found!", path));
    }

    public Text LoadText(string text, string font = "Arial")
    {
        return new TextWindows(text, font, this);
    }

    protected internal void Render(Bitmap img, int x, int y)
    {
        _bufferCommands.AddLast(new DrawCommand(img, new Point(x, y)));
    }

    protected internal void Render(TextWindows text, int x, int y)
    {
        _bufferCommands.AddLast(new TextCommand(text, new Point(x, y)));
    }
}
