
namespace SimpleGameEngine;

internal class WindowsSpriteSheet : ISpriteSheet
{
    private readonly WindowsDevice _windows;
    private readonly Dimension _dimension;
    private readonly List<IImage> _sprites;
    public WindowsSpriteSheet(WindowsDevice windows, Bitmap sheet, Dimension dimension)
    {
        _windows = windows;
        _dimension = dimension;
        _sprites = Split(sheet, _dimension);
    }
    public int Length { get => _sprites.Count; }

    public Dimension SpriteDimension { get => _dimension; }

    public IImage GetSprite(int index)
    {
        if (index < 0 || index >= _sprites.Count)
            throw new ArgumentException(String.Format("The index {0} does not exist!", index));
        return _sprites[index];
    }

    private List<IImage> Split(Bitmap bitmap, Dimension dimension)
    {
        var frames = new List<IImage>();
        int x_count = bitmap.Width / dimension.Width;
        int y_count = bitmap.Height / dimension.Height;
        var rect = new Rectangle(0, 0, dimension.Width, dimension.Height);
        for (int y = 0; y < y_count; y++)
        {
            for (int x = 0; x < x_count; x++)
            {
                var splitted = new Bitmap(dimension.Width, dimension.Height);
                var graphics = Graphics.FromImage(splitted);
                var dest = new Rectangle(x * dimension.Width, y * dimension.Height, dimension.Width, dimension.Height);
                graphics.DrawImage(bitmap, rect, dest, GraphicsUnit.Pixel);
                graphics.Dispose();
                var sprite = new WindowsImage(_windows, splitted);
                frames.Add(sprite);
            }
        }
        return frames;
    }
}
