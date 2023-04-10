
namespace SGE;

using System.Drawing;
using System.Windows.Forms;
using System.Windows.Media;

public class SoundWindows
{
    private static int _countId = 0;
    private readonly int _id;

    public readonly String Path;

    protected internal MediaPlayer _sound;
    protected internal PlatformWindows _game;

    protected internal SoundWindows(string path, MediaPlayer sound, PlatformWindows game)
    {
        _id = _countId++;
        Path = path;
        _sound = sound;
        _game = game;
    }

    public double Volume
    {
        get { return _sound.Volume; }
        set { if (value >= 0 && value <= 1.0) _sound.Volume = value; }
    }

    public void Play()
    {
        _sound.MediaEnded -= DoLoop;
        _sound.Play();
    }

    public void Loop()
    {
        _sound.MediaEnded += DoLoop;
        _sound.Play();
    }

    public void Pause()
    {
        _sound.Pause();
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

public class ImageWindows
{
    private static int _countId = 0;
    private readonly int _id;

    public readonly String Path;
    public readonly int Width;
    public readonly int Height;

    protected internal Bitmap _bitmap;
    protected internal PlatformWindows _game;

    protected internal ImageWindows(string path, Bitmap bitmap, PlatformWindows game)
    {
        _id = _countId++;
        Path = path;
        Width = bitmap.Width;
        Height = bitmap.Height;
        _bitmap = bitmap;
        _game = game;
    }

    public void Render(int x, int y)
    {
        _game.Render(this, x, y);
    }

    public override bool Equals(object? obj) { return obj is ImageWindows img ? img._id == _id : base.Equals(obj); }

    public override int GetHashCode() { return HashCode.Combine(_id); }

    public override string ToString() { return "Image(" + Path + ", " + Width + "x" + Height + ")"; }
}

internal class DrawCommand
{
    public Bitmap _bitmap;
    public Point _point;
    public DrawCommand(Bitmap bitmap, Point point)
    {
        _bitmap = bitmap;
        _point = point;
    }
}

public partial class PlatformWindows : Form, Platform
{
    private Dictionary<string, ImageWindows> _images;
    private Dictionary<string, SoundWindows> _sounds;

    private LinkedList<DrawCommand> _drawCommands;
    private Dictionary<char, Action> _keyDownCommands;
    private Dictionary<char, Action> _keyUpCommands;
    private Dictionary<char, Action> _keyPressedCommands;

    private Action<int, int>? _onMouseMove;
    private Action<int, int, int>? _onMouseClick;

    private Action _onLoop;


    private Timer timer;
    private const int FPS = 32;

    public PlatformWindows()
    {
        _images = new Dictionary<string, ImageWindows>();
        _sounds = new Dictionary<string, SoundWindows>();
        _drawCommands = new LinkedList<DrawCommand>();
        _keyDownCommands = new Dictionary<char, Action>();
        _keyUpCommands = new Dictionary<char, Action>();
        _keyPressedCommands = new Dictionary<char, Action>();

        InitializeComponent();

        // Set the form's properties
        this.Text = "Game";
        this.BackColor = System.Drawing.Color.Black;
        this.WindowState = FormWindowState.Normal;
        this.FormBorderStyle = FormBorderStyle.None;
        if (Screen.PrimaryScreen != null)
            this.Bounds = Screen.PrimaryScreen.Bounds;
        this.DoubleBuffered = true;

        // Add event handlers
        this.KeyDown += DoKeyDown;
        this.KeyUp += DoKeyUp;
        this.KeyPress += DoKeyPressed;
        this.Paint += DoPaint;
        this.MouseClick += DoMouseClick;
        this.MouseMove += DoMouseMove;
        this._onLoop += () => { };

        this.timer = new Timer();
        this.timer.Interval = (int)(1000 / FPS);
        this.timer.Tick += DoLoop;
        this.timer.Start();
    }

    public void RegisterKeyUp(char c, Action command)
    {
        _keyUpCommands.Add(c, command);
    }

    public void RegisterKeyDown(char c, Action command)
    {
        _keyDownCommands.Add(c, command);
    }

    public void RegisterPressed(char c, Action command)
    {
        _keyPressedCommands.Add(c, command);
    }

    public void RegisterMouseMove(Action<int, int> command)
    {
        _onMouseMove += command;
    }

    public void RegisterMouseClick(Action<int, int, int> command)
    {
        _onMouseClick += command;
    }

    protected internal void Render(ImageWindows img, int x, int y)
    {
        _drawCommands.AddLast(new DrawCommand(img._bitmap, new Point(x, y)));
    }

    protected void DoKeyDown(object? sender, KeyEventArgs e)
    {
        char c = (char)e.KeyCode;
        if (_keyDownCommands.ContainsKey(c))
        {
            _keyDownCommands[c].Invoke();
        }
    }

    protected void DoKeyPressed(object? sender, KeyPressEventArgs e)
    {
        char c = e.KeyChar;
        if (_keyPressedCommands.ContainsKey(c))
        {
            _keyPressedCommands[c].Invoke();
        }
    }

    protected void DoKeyUp(object? sender, KeyEventArgs e)
    {
        char c = (char)e.KeyCode;
        if (_keyUpCommands.ContainsKey(c))
        {
            _keyUpCommands[c].Invoke();
        }
    }

    protected void DoMouseMove(object? sender, MouseEventArgs e)
    {
        Point clientPosition = this.PointToClient(e.Location);
        _onMouseMove?.Invoke(clientPosition.X, clientPosition.Y);
    }

    protected void DoMouseClick(object? sender, MouseEventArgs e)
    {
        int button = -1;
        switch (e.Button)
        {
            case MouseButtons.Left: button = 0; break;
            case MouseButtons.Right: button = 1; break;
            case MouseButtons.Middle: button = 3; break;
        }
        Point clientPosition = this.PointToClient(e.Location);
        _onMouseClick?.Invoke(button, clientPosition.X, clientPosition.Y);
    }

    protected void DoPaint(object? sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        foreach (var cmd in _drawCommands)
        {
            g.DrawImage(cmd._bitmap, cmd._point);
        }
    }

    public void Start()
    {
        this.timer.Start();
    }

    public void RegisterLoop(Action loop, int fps)
    {
        _onLoop += loop;
        this.timer.Interval = (int)(1000 / fps);
    }

    protected void DoLoop(object? sender, EventArgs e)
    {
        _onLoop?.Invoke();
        Invalidate();
        _drawCommands.Clear();
    }

    public void Finish()
    {
        this.timer.Stop();
        this.Close();
    }

    public ImageWindows LoadImage(string path)
    {
        if (!_images.ContainsKey(path))
        {
            var bitmap = new Bitmap(path);
            var image = new ImageWindows(path, bitmap, this);
            _images.Add(path, image);
        }
        return _images[path];
    }

    public SoundWindows LoadSound(string path)
    {
        if (!_sounds.ContainsKey(path))
        {
            var player = new MediaPlayer();
            player.Open(new Uri(path));
            var sound = new SoundWindows(path, player, this);
            _sounds.Add(path, sound);
        }
        return _sounds[path];
    }
}
