
namespace SGE;

public class PlatformAdapter : Platform
{
    public int Width => throw new NotImplementedException();

    public int Height => throw new NotImplementedException();

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

    public Text LoadText(string text, string font = "Arial", int size = 12)
    {
        throw new NotImplementedException();
    }

    public void RegisterKeyDown(char c, Action command)
    {
        throw new NotImplementedException();
    }

    public void RegisterKeyUp(char c, Action command)
    {
        throw new NotImplementedException();
    }

    public void RegisterLoop(Action loop, int fps)
    {
        throw new NotImplementedException();
    }

    public void RegisterMouseClick(Action<int, int, int> command)
    {
        throw new NotImplementedException();
    }

    public void RegisterMouseMove(Action<int, int> command)
    {
        throw new NotImplementedException();
    }

    public void RegisterPressed(char c, Action command)
    {
        throw new NotImplementedException();
    }

    public void Start()
    {
    }
}
