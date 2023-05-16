
namespace SGE;

public class PlatformAdapter : Platform
{
    public int Width => throw new NotImplementedException();
    public int Height => throw new NotImplementedException();
    public (int, int) MousePosition => throw new NotImplementedException();

    public bool FullScreen { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void Finish()
    {
    }

    public Image LoadImage(string path)
    {
        throw new NotImplementedException();
    }

    public Sound LoadSound(string path)
    {
        throw new NotImplementedException();
    }

    public SpriteSheet LoadSpriteSheet(Image img, int width, int height)
    {
        throw new NotImplementedException();
    }

    public Text LoadText(string text, string font = "Arial")
    {
        throw new NotImplementedException();
    }

    public void RegisterKeyDown(char c, Action<int> command)
    {
        throw new NotImplementedException();
    }

    public void RegisterKeyUp(char c, Action<int> command)
    {
        throw new NotImplementedException();
    }

    public void RegisterLoop(Action loop, int fps)
    {
        throw new NotImplementedException();
    }

    public void RegisterMouseClick(char button, Action<int, int> command)
    {
        throw new NotImplementedException();
    }

    public void RegisterMouseDown(char button, Action<int, int> command)
    {
        throw new NotImplementedException();
    }

    public void RegisterMouseUp(char button, Action<int, int> command)
    {
        throw new NotImplementedException();
    }

    public void RegisterMouseMove(Action<int, int> command)
    {
        throw new NotImplementedException();
    }

    public void RegisterMouseWheel(Action<int> command)
    {
        throw new NotImplementedException();
    }

    public void RegisterKeyPressed(char c, Action command)
    {
        throw new NotImplementedException();
    }

    public void Start()
    {
    }
}
