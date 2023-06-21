
namespace SimpleGameEngine;

internal class WindowsImage : IImage
{
    protected internal readonly Bitmap _bitmap;
    private readonly WindowsDevice _windows;
    private readonly Dimension _dimension;
    private readonly string _path;

    public WindowsImage(WindowsDevice windows, string path)
    {
        _windows = windows;
        _path = path;
        _bitmap = new Bitmap(path);
        _dimension = new Dimension(_bitmap.Width, _bitmap.Height);
    }
    public WindowsImage(WindowsDevice windows, Bitmap bitmap)
    {
        _windows = windows;
        _path = "custom";
        _bitmap = bitmap;
        _dimension = new Dimension(bitmap.Width, bitmap.Height);
    }
    public string Path { get => _path; }
    public Dimension Dimension { get => _dimension; }

    public void Render(Point position)
    {
        int x = position.X;
        int y = position.Y;
        _windows.Render((Graphics g) => g.DrawImage(_bitmap, x, y));
    }

    public IImage Crop(Point position, Dimension dimension)
    {
        var cropped = new Bitmap(dimension.Width, dimension.Height);
        var graphics = Graphics.FromImage(cropped);
        graphics.DrawImageUnscaledAndClipped(_bitmap, new Rectangle(position.X, position.Y, cropped.Width, cropped.Height));
        return new WindowsImage(_windows, cropped);
    }

    public IImage Resize(Dimension dimension)
    {
        var resized = new Bitmap(dimension.Width, dimension.Height);
        var graphics = Graphics.FromImage(resized);
        graphics.DrawImage(_bitmap, new Rectangle(0, 0, resized.Width, resized.Height));
        return new WindowsImage(_windows, resized);
    }

    public void Dispose()
    {
        _bitmap.Dispose();
    }
}
