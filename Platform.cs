
namespace SGE;

using System.Drawing;
using System.Windows.Forms;
using System.Windows.Media;

public class Sound
{
    private static int _countId = 0;
    private readonly int _id;

    public readonly String Path;

    protected internal MediaPlayer _sound;
    protected internal Platform _game;

    protected internal Sound(string path, MediaPlayer sound, Platform game)
    {
        _id = _countId++;
        Path = path;
        _sound = sound;
        _game = game;
    }

    public double Volume {
        get {return _sound.Volume; }
        set {if(value >= 0 && value <= 1.0) _sound.Volume = value;}
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

    public void Pause(){
        _sound.Pause();
    }

    public void Stop()
    {
        _sound.Stop();
    }

    public override bool Equals(object? obj) { return obj is Sound sound ? sound._id == _id : base.Equals(obj); }

    public override int GetHashCode() { return HashCode.Combine(_id); }

    public override string ToString() { return "Sound(" + Path + ")"; }

    private static void DoLoop(object? sender, EventArgs e)
    {
        if(sender != null){
            MediaPlayer mediaPlayer = (MediaPlayer)sender;
            mediaPlayer.Position = TimeSpan.Zero;
            mediaPlayer.Play();
        }
    }
}

public class Image
{
    private static int _countId = 0;
    private readonly int _id;

    public readonly String Path;
    public readonly int Width;
    public readonly int Height;

    protected internal Bitmap _bitmap;
    protected internal Platform _game;

    protected internal Image(string path, Bitmap bitmap, Platform game)
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

    public override bool Equals(object? obj) { return obj is Image img ? img._id == _id : base.Equals(obj); }

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

public partial class Platform : Form
{
    private Dictionary<string, Image> _images;
    private Dictionary<string, Sound> _sounds;

    private LinkedList<DrawCommand> _drawCommands;
    private Dictionary<char, Action> _keyDownCommands;
    private Dictionary<char, Action> _keyUpCommands;
    private Dictionary<char, Action> _keyPressedCommands;

    private Action<int, int>? _onMouseMove;
    private Action<int, int, int>? _onMouseClick;


    private Timer timer;
    private const int FPS = 32;

    public Platform()
    {
        _images = new Dictionary<string, Image>();
        _sounds = new Dictionary<string, Sound>();
        _drawCommands = new LinkedList<DrawCommand>();
        _keyDownCommands = new Dictionary<char, Action>();
        _keyUpCommands = new Dictionary<char, Action>();
        _keyPressedCommands = new Dictionary<char, Action>();

        InitializeComponent();

        // Set the form's properties
        this.Text = "Game";
        this.BackColor = System.Drawing.Color.Black;
        this.FormBorderStyle = FormBorderStyle.None;
        this.DoubleBuffered = true;
        this.ClientSize = new Size(640, 480);

        // Add event handlers
        this.KeyDown += DoKeyDown;
        this.KeyUp += DoKeyUp;
        this.KeyPress += DoKeyPressed;
        this.Paint += DoPaint;
        this.MouseClick += DoMouseClick;
        this.MouseMove += DoMouseMove;

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

    protected internal void Render(Image img, int x, int y)
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

    protected void DoLoop(object? sender, EventArgs e)
    {
        Loop();
        Invalidate();
        _drawCommands.Clear();
    }

    public virtual void Loop(){}

    public Image LoadImage(string path)
    {
        if (!_images.ContainsKey(path))
        {
            var bitmap = new Bitmap(path);
            var image = new Image(path, bitmap, this);
            _images.Add(path, image);
        }
        return _images[path];
    }

    public Sound LoadSound(string path)
    {
        if (!_sounds.ContainsKey(path))
        {
            var player = new MediaPlayer();
            player.Open(new Uri(path));
            var sound = new Sound(path, player, this);
            _sounds.Add(path, sound);
        }
        return _sounds[path];
    }
}