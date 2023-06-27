
namespace SimpleGameEngine.Test;

public class IResourceStub : IResource
{
    private string _path = "";
    public string Path {
        get => _path;
        set => _path = value;
    }

    public void Dispose(){}
}
