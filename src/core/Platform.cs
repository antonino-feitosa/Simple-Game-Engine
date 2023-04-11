
namespace SGE;

public interface Text
{
    public string Text { get; set; }
    public int Size { get; set; }
    public string Font { get; set; }
    public string Color {get; set;}
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

    public void RegisterKeyUp(char c, Action command);

    public void RegisterKeyDown(char c, Action command);

    public void RegisterPressed(char c, Action command);

    public void RegisterMouseMove(Action<int, int> command);

    public void RegisterMouseClick(Action<int, int, int> command);

    public void Start();
    public void Finish();
    public void RegisterLoop(Action loop, int fps);

    public Image LoadImage(string path);
    public Sound LoadSound(string path);
    public Text LoadText(string text, string font = "Arial", int size = 12);
}
