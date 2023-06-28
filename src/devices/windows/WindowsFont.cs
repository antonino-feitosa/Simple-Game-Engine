
using System.Drawing.Text;

namespace SimpleGameEngine;

internal class WindowsFont : IFont
{
    private readonly string _path;
    internal readonly FontFamily _fontFamily;

    public WindowsFont()
    {
        //FontFamily fontFamily = new FontFamily(@"C:\Projects\MyProj\#free3of9");
        //The font name without the file extension, and keep the '#' symbol.
        _path = "Arial";
        _fontFamily = new FontFamily("Arial");
    }

    public WindowsFont(string path)
    {
        _path = path;
        var privateFontCollection = new PrivateFontCollection();
        privateFontCollection.AddFontFile(path);
        _fontFamily = privateFontCollection.Families[0];
    }

    public string Path { get => _path; }

    public void Dispose()
    {
        _fontFamily.Dispose();
    }
}
