
using NAudio.Wave;

namespace SimpleGameEngine;

internal class WindowsSound : ISound
{
    private bool _isLoop;
    private readonly string _path;
    private readonly AudioFileReader _audioFile;
    private readonly WaveOutEvent _waveOut;
    private readonly EventHandler<StoppedEventArgs> _loopCallback;

    public WindowsSound(string path)
    {
        _path = path;
        _isLoop = false;
        _audioFile = new AudioFileReader(path);
        _waveOut = new WaveOutEvent();
        _waveOut.Init(_audioFile);
        _loopCallback = (sender, args) => _waveOut.Play();
    }
    public float Volume { get => _waveOut.Volume; set => _waveOut.Volume = value > 1 ? 1 : (value < 0 ? 0 : value); }
    public string Path { get => _path; }
    public bool IsPlaying { get => _waveOut.PlaybackState == PlaybackState.Playing; }

    public bool IsLoop
    {
        get => _isLoop;
        set
        {
            if (value) _waveOut.PlaybackStopped += _loopCallback;
            else _waveOut.PlaybackStopped -= _loopCallback;
            _isLoop = value;
        }
    }

    public void Play()
    {
        _waveOut.Play();
    }

    public void Pause()
    {
        _waveOut.Pause();
    }

    public void Stop()
    {
        _waveOut.Stop();
    }

    public void Dispose()
    {
        _waveOut.Stop();
        _waveOut.Dispose();
        _audioFile.Dispose();
    }
}
