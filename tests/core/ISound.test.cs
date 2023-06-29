
using static SimpleGameEngine.Test.TestRunner;

namespace SimpleGameEngine.Test;

public class ISoundTest : IResourceTest
{
    public ISound sound;
    public ISoundTest(ISound sound) : base(sound)
    {
        this.sound = sound;
    }

    public void GivenNewSound_whenCreate_thenIsNotPlaying()
    {
        var isPlaying = sound.IsPlaying;

        Assert(isPlaying == false);
    }

    public void CheckVolume()
    {
        sound.Volume = 0.1f;
        var volume = sound.Volume;

        AssertEquals(volume, 0.1f);
    }

    public void CheckIsPlaying()
    {
        sound.Play();

        var isPlaying = sound.IsPlaying;

        Assert(isPlaying == true);
    }

    public void CheckIsLoop()
    {
        sound.IsLoop = true;

        var isLoop = sound.IsLoop;

        Assert(isLoop == true);
    }

    public void CheckPlay()
    {
        var capturedException = false;
        try
        {
            sound.Play();
        }
        catch
        {
            capturedException = true;
        }
        Assert(capturedException == false);
    }

    public void CheckPause()
    {
        var capturedException = false;
        sound.Play();
        try
        {
            sound.Pause();
        }
        catch
        {
            capturedException = true;
        }
        Assert(capturedException == false);
    }

    public void CheckStop()
    {
        var capturedException = false;
        sound.Play();
        try
        {
            sound.Stop();
        }
        catch
        {
            capturedException = true;
        }
        Assert(capturedException == false);
    }
}
