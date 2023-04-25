
namespace SGE;

public interface Text
{
    public string Text { get; set; }
    public int Size { get; set; }
    public string Font { get; set; }
    public string Color { get; set; }
    public void Render(int x, int y);
}
public interface Sound
{
    public string Path { get; }
    public double Volume { get; set; }

    public void Play();
    public void Loop();
    public void Pause();
    public void Stop();
}

public interface SpriteSheet
{
    public string Path { get; }
    public int Width { get; }
    public int Height { get; }
    public void Render(int index, int x, int y);
    public Image GetImage(int index);
}

public interface Image
{
    public string Path { get; }
    public int Width { get; }
    public int Height { get; }
    public void Render(int x, int y);
}

public interface Platform
{
    public int Width { get; }
    public int Height { get; }
    public (int, int) MousePosition { get; }
    public bool FullScreen { get; set; }

    // bits: alt, shift, ctrl
    public void RegisterKeyUp(char c, Action<int> command);
    public void RegisterKeyDown(char c, Action<int> command);

    // -1, 0, +1
    public void RegisterMouseWheel(Action<int> command);

    // L, R, M
    public void RegisterMouseDown(char button, Action<int, int> command);
    public void RegisterMouseUp(char button, Action<int, int> command);

    public void Start();
    public void Finish();
    public void RegisterLoop(Action loop, int fps = 32);

    public Image LoadImage(string path);
    public Sound LoadSound(string path);
    public Text LoadText(string text, string font = "Arial");
    public SpriteSheet LoadSpriteSheet(string path, int width, int height);
}
