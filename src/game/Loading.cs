
namespace SGE;

public class Loading
{

    public const string TITLE_FILE = "./art/GUI/Title.png";
    public const string LOADING_FILE = "./art/GUI/Title-Loading.png";
    public const string BACKGROUND_FILE = "./art/GUI/Title-Background.png";
    public const string BORDER_PROGRESS_BAR = "./art/GUI/ProgressBar-Border.png";
    public const string FILL_PROGRESS_BAR = "./art/GUI/ProgressBar-Fill.png";

    public Image _title;
    public Image _loading;
    public Image _background;
    public Image _border;
    public Image _fill;
    private Image? _current;
    public Game _game;
    private int _loaded;

    public Loading(Game game)
    {
        _game = game;
        _loaded = 0;
        var device = _game.Device;
        _title = device.LoadImage(TITLE_FILE);
        _loading = device.LoadImage(LOADING_FILE);
        _background = device.LoadImage(BACKGROUND_FILE);
        _border = device.LoadImage(BORDER_PROGRESS_BAR);
        _fill = device.LoadImage(FILL_PROGRESS_BAR);
    }

    public int Loaded
    {
        get { return _loaded; }
        set { if (_loaded > 0 && _loaded <= 100) _loaded = value; Changed(); }
    }

    public bool Finished()
    {
        return _loaded >= 100;
    }

    protected void Changed()
    {

    }

    public void Run()
    {
        _title.Render(200, 20);
    }
}
