
namespace SGE;
public interface Sound
{
    public String Path { get; }
    public double Volume { get; set; }

    public void Play();
    public void Loop();
    public void Pause();
    public void Stop();
}

public interface Image
{
    public String Path { get; }
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

    public ImageWindows LoadImage(string path);

    public SoundWindows LoadSound(string path);
}
