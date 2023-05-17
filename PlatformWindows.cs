
namespace SGE;

using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;
using System.Windows.Media;

public class TextWindows : Text
{
    protected string _text;
    protected int _size;
    protected string _font;
    protected string _color;
    protected internal PlatformWindows _device;

    public string Text { get => _text; set => _text = value; }
    public int Size { get => _size; set => _size = value; }
    public string Font { get => _font; set => _font = value; }
    public string Color { get => _color; set => _color = value; }

    public TextWindows(string text, string font, PlatformWindows device)
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
    protected internal PlatformWindows _game;

    protected internal SoundWindows(string path, SoundPlayer sound, PlatformWindows game)
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
    protected internal PlatformWindows _device;

    protected internal SpriteSheetWindows(string path, Bitmap bitmap, PlatformWindows device)
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
    protected internal PlatformWindows _device;

    protected internal ImageWindows(string path, Bitmap bitmap, PlatformWindows device)
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
        System.Drawing.Font drawFont = new System.Drawing.Font(_text.Font, _text.Size);
        System.Drawing.Color color = System.Drawing.Color.FromName(_text.Color);
        System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(color);
        System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
        g.DrawString(_text.Text, drawFont, drawBrush, _point.X, _point.Y, drawFormat);
        drawFont.Dispose();
        drawBrush.Dispose();
    }
}

public partial class PlatformWindows : Form, Platform
{
    private Dictionary<string, SpriteSheetWindows> _spriteSheets;
    private Dictionary<string, ImageWindows> _images;
    private Dictionary<string, SoundWindows> _sounds;

    private LinkedList<Render> _renderCommands;
    private LinkedList<Render> _bufferCommands;

    private Dictionary<char, Action<int>> _keyDownCommands;
    private Dictionary<char, Action<int>> _keyUpCommands;

    private Action<int>? _onMouseWheel;
    private Dictionary<char, Action<int, int>> _onMouseDown;
    private Dictionary<char, Action<int, int>> _onMouseUp;

    private Action _onLoop;
    private bool _fullScreen;


    private Timer timer;
    private const int FPS = 32;

    public PlatformWindows()
    {
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
        this.BackColor = System.Drawing.Color.Black;
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
        this.timer.Interval = (int)(1000 / FPS);
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
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.Bounds = Screen.PrimaryScreen.Bounds;
            }
            else
            {
                this.ClientSize = new Size(800, 600);
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
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

    public void Finish()
    {
        this.timer.Stop();
        this.Close();
    }

    public Image LoadImage(string path)
    {
        if (!_images.ContainsKey(path))
        {
            try {
                var bitmap = new Bitmap(path);
                var image = new ImageWindows(path, bitmap, this);
                _images.Add(path, image);
            } catch(ArgumentException e){
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
