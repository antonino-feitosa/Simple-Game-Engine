
namespace SimpleGameEngine;

public class WindowsText : IText
{
    private readonly WindowsDevice _windows;
    private string _text;
    private int _size;
    private WindowsColor _color;
    private WindowsFont _font;

    internal Font _drawFont;
    private bool _disposed;

    public WindowsText(WindowsDevice windows, string text)
    {
        _disposed = false;
        _windows = windows;
        _text = text;
        _size = 12;
        _color = new WindowsColor(0, 0, 0, 255);
        _font = new WindowsFont();
        _drawFont = new Font(_font._fontFamily, _size);
    }
    ~WindowsText()
    {
        Dispose(false);
    }

    public string Text { get => _text; set => _text = value; }
    public int Size { get => _size; set { _size = value; UpdateDraw(); } }
    public IFont Font { get => _font; set => _font = (WindowsFont)value; }
    public IColor Color { get => _color; set => _color = (WindowsColor)value; }

    private void UpdateDraw()
    {
        _drawFont.Dispose();
        _drawFont = new Font(_font._fontFamily, _size);
    }

    public void Render(Point position)
    {
        int x = position.X;
        int y = position.Y;
        var text = _text;
        var drawFont = _drawFont;
        var drawBrush = _color._drawBrush;
        var drawFormat = new StringFormat();
        void render(Graphics g) => g.DrawString(text, drawFont, drawBrush, x, y, drawFormat);
        _windows.Render(render);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _drawFont.Dispose();
            }
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
