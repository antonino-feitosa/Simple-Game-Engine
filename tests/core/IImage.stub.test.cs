
namespace SimpleGameEngine.Test;

public class IImageStub : IImage
{
    public Action<Point>? OnRender;
    public Func<Point, Dimension, IImage>? OnCrop;
    public Func<Dimension, IImage>? OnResize;
    public Action? OnDispose;
    public Func<Dimension>? OnGetDimension;
    public Func<string>? OnGetPath;

    public Dimension Dimension { get => OnGetDimension?.Invoke() ?? throw new ArgumentException(nameof(OnGetDimension)); }

    public string Path { get => OnGetPath?.Invoke() ?? throw new ArgumentException(nameof(OnGetPath)); }

    public IImage Crop(Point position, Dimension dimension)
    {
        return OnCrop?.Invoke(position, dimension) ?? throw new ArgumentException(nameof(OnCrop));
    }

    public void Render(Point position) { OnRender?.Invoke(position); }

    public IImage Resize(Dimension dimension)
    {
        return OnResize?.Invoke(dimension) ?? throw new ArgumentException(nameof(OnResize));
    }

    public void Dispose()
    {
        OnDispose?.Invoke();
    }
}