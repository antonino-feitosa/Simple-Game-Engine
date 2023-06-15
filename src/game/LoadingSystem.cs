
namespace SGE;

public class LoadingSystem : System
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

    public LoadingSystem(Device device, Game game)
    {
        _game = game;
        _loaded = 0;
        _title = device.MakeImage(TITLE_FILE);
        _loading = device.MakeImage(LOADING_FILE);
        _background = device.MakeImage(BACKGROUND_FILE);
        _border = device.MakeImage(BORDER_PROGRESS_BAR);
        _fill = device.MakeImage(FILL_PROGRESS_BAR);
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
        //TODO
    }
    public void Load(){
        //TODO
    }

    public void Process()
    {
        _background.Render(new Position(0,0));
        _title.Render(new Position(200, 20));

        _border.Render(new Position(215, 180));
        _fill.Render(new Position(215, 180));
        _loading.Render(new Position(265, 175));
    }
}
