
using static SimpleGameEngine.Test.TestRunner;

namespace SimpleGameEngine.Test;

public class IResourceTest
{

    public IResource resource;

    public IResourceTest(IResource resource)
    {
        this.resource = resource;
    }

    public void CheckPath()
    {
        var capturedException = false;

        try
        {
            var path = resource.Path;
        }
        catch
        {
            capturedException = true;
        }

        Assert(capturedException == false);
    }

    public void CheckDispose()
    {
        var capturedException = false;

        try
        {
            resource.Dispose();
        }
        catch
        {
            capturedException = true;
        }

        Assert(capturedException == false);
    }
}