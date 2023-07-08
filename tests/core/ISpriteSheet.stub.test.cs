
namespace SimpleGameEngine.Test;

public class ISpriteSheetStub : ISpriteSheet
{
    public Func<int>? OnGetLength;
    public Func<Dimension>? OnGetDimension;
    public Func<int, IImage>? OnGetSprite;
    public int Length { get => OnGetLength?.Invoke() ?? throw new ArgumentException(nameof(OnGetLength)); }

    public Dimension SpriteDimension { get => OnGetDimension?.Invoke() ?? throw new ArgumentException(nameof(OnGetDimension)); }

    public IImage GetSprite(int index)
    {
        return OnGetSprite?.Invoke(index) ?? throw new ArgumentException(nameof(OnGetSprite));
    }
}
