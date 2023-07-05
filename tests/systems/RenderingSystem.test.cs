using static SimpleGameEngine.Test.TestRunner;

namespace SimpleGameEngine.Test;

[TestClass]
public class RenderingSystemTest
{
    public void GivenRenderable_whenRenderAtBounds_thenCallImageRender()
    {
        var cameraPosition = new Point(2, 3);
        var cameraDimension = new Dimension(2, 2);
        var system = new RenderingSystem(cameraPosition, cameraDimension);
        var called = false;
        var calledPosition = new Point(-1, -1);
        var image = new IImageStub()
        {
            OnRender = (point) => { called = true; calledPosition = point; },
            OnGetDimension = () => new Dimension(32, 32)
        };
        var position = new Point(2, 3);
        var component = new RenderableComponent(system, image, position);

        system.Process();

        AssertTrue(called, "The render must be called on the image!");
        AssertEquals(calledPosition.X, 64, "The X position must be mapped to the screen pixels!");
        AssertEquals(calledPosition.Y, 96, "The Y position must be mapped to the screen pixels!");
    }

    public void GivenRenderable_whenRenderPartiallyAtBounds_thenCallImageRender()
    {
        var cameraPosition = new Point(2, 3);
        var cameraDimension = new Dimension(2, 2);
        var system = new RenderingSystem(cameraPosition, cameraDimension);
        var called = false;
        var calledPosition = new Point(-1, -1);
        var image = new IImageStub()
        {
            OnRender = (point) => { called = true; calledPosition = point; },
            OnGetDimension = () => new Dimension(64, 64)
        };
        var position = new Point(1, 2);
        var component = new RenderableComponent(system, image, position);

        system.Process();

        AssertTrue(called, "The render must be called on the image!");
        AssertEquals(calledPosition.X, 32, "The X position must be mapped to the screen pixels!");
        AssertEquals(calledPosition.Y, 64, "The Y position must be mapped to the screen pixels!");
    }

    public void GivenRenderable_whenRenderOutOfBounds_thenDoNotCallImageRender()
    {
        var cameraPosition = new Point(2, 3);
        var cameraDimension = new Dimension(2, 2);
        var system = new RenderingSystem(cameraPosition, cameraDimension);
        var called = false;
        var image = new IImageStub()
        {
            OnRender = (point) => { called = true; },
            OnGetDimension = () => new Dimension(32, 32)
        };
        var position = new Point(1, 2);
        var component = new RenderableComponent(system, image, position);

        system.Process();

        AssertFalse(called, "The render must not be called on the image!");
    }

    public void GivenRenderable_whenRenderAtBoundsButNotVisible_thenDoNotCallImageRender()
    {
        var cameraPosition = new Point(2, 3);
        var cameraDimension = new Dimension(2, 2);
        var system = new RenderingSystem(cameraPosition, cameraDimension);
        var called = false;
        var image = new IImageStub()
        {
            OnRender = (point) => { called = true; },
            OnGetDimension = () => new Dimension(32, 32)
        };
        var position = new Point(2, 3);
        var component = new RenderableComponent(system, image, position) { Visible = false };

        system.Process();

        AssertFalse(called, "The render must not be called on the image!");
    }

    public void GivenTwoRenderable_whenAInFrontOfB_thenCallImageRenderAtBBeforeA()
    {
        var cameraPosition = new Point(2, 3);
        var cameraDimension = new Dimension(2, 2);
        var system = new RenderingSystem(cameraPosition, cameraDimension);
        var called = new List<int>();
        var firstImage = new IImageStub()
        {
            OnRender = (point) => { called.Add(1); },
            OnGetDimension = () => new Dimension(32, 32)
        };
        var secondImage = new IImageStub()
        {
            OnRender = (point) => { called.Add(0); },
            OnGetDimension = () => new Dimension(32, 32)
        };
        var position = new Point(2, 3);
        var firstComponent = new RenderableComponent(system, firstImage, position) { ZIndex = 1 };
        var secondComponent = new RenderableComponent(system, secondImage, position) { ZIndex = 0 };

        system.Process();

        AssertEquals(called.Count, 2, "The two components must be rendered!");
        AssertEquals(called[0], 0, "The minor z index must be drawn first!");
        AssertEquals(called[1], 1, "The second minor z index must be drawn second!");
    }

    public void GivenRenderable_whenRenderAtUpLeftBounds_thenCallImageRender()
    {
        var cameraPosition = new Point(2, 3);
        var cameraDimension = new Dimension(3, 6);
        var system = new RenderingSystem(cameraPosition, cameraDimension);
        var called = false;
        var calledPosition = new Point(-1, -1);
        var image = new IImageStub()
        {
            OnRender = (point) => { called = true; calledPosition = point; },
            OnGetDimension = () => new Dimension(32, 32)
        };
        var position = new Point(cameraPosition.X, cameraPosition.Y);
        var component = new RenderableComponent(system, image, position);

        system.Process();

        AssertTrue(called, "The render must be called on the image!");
        AssertEquals(calledPosition.X, position.X * 32, "The X position must be mapped to the screen pixels!");
        AssertEquals(calledPosition.Y, position.Y * 32, "The Y position must be mapped to the screen pixels!");
    }

    public void GivenRenderable_whenRenderAtUpRightBounds_thenCallImageRender()
    {
        var cameraPosition = new Point(2, 3);
        var cameraDimension = new Dimension(3, 6);
        var system = new RenderingSystem(cameraPosition, cameraDimension);
        var called = false;
        var calledPosition = new Point(-1, -1);
        var image = new IImageStub()
        {
            OnRender = (point) => { called = true; calledPosition = point; },
            OnGetDimension = () => new Dimension(32, 32)
        };
        var position = new Point(cameraPosition.X + cameraDimension.Width - 1, cameraPosition.Y);
        var component = new RenderableComponent(system, image, position);

        system.Process();

        AssertTrue(called, "The render must be called on the image!");
        AssertEquals(calledPosition.X, position.X * 32, "The X position must be mapped to the screen pixels!");
        AssertEquals(calledPosition.Y, position.Y * 32, "The Y position must be mapped to the screen pixels!");
    }

    public void GivenRenderable_whenRenderAtDownLeftBounds_thenCallImageRender()
    {
        var cameraPosition = new Point(2, 3);
        var cameraDimension = new Dimension(3, 6);
        var system = new RenderingSystem(cameraPosition, cameraDimension);
        var called = false;
        var calledPosition = new Point(-1, -1);
        var image = new IImageStub()
        {
            OnRender = (point) => { called = true; calledPosition = point; },
            OnGetDimension = () => new Dimension(32, 32)
        };
        var position = new Point(cameraPosition.X, cameraPosition.Y + cameraDimension.Height - 1);
        var component = new RenderableComponent(system, image, position);

        system.Process();

        AssertTrue(called, "The render must be called on the image!");
        AssertEquals(calledPosition.X, position.X * 32, "The X position must be mapped to the screen pixels!");
        AssertEquals(calledPosition.Y, position.Y * 32, "The Y position must be mapped to the screen pixels!");
    }

    public void GivenRenderable_whenRenderAtDownRightBounds_thenCallImageRender()
    {
        var cameraPosition = new Point(2, 3);
        var cameraDimension = new Dimension(3, 6);
        var system = new RenderingSystem(cameraPosition, cameraDimension);
        var called = false;
        var calledPosition = new Point(-1, -1);
        var image = new IImageStub()
        {
            OnRender = (point) => { called = true; calledPosition = point; },
            OnGetDimension = () => new Dimension(32, 32)
        };
        var position = new Point(cameraPosition.X + cameraDimension.Width - 1, cameraPosition.Y + cameraDimension.Height - 1);
        var component = new RenderableComponent(system, image, position);

        system.Process();

        AssertTrue(called, "The render must be called on the image!");
        AssertEquals(calledPosition.X, position.X * 32, "The X position must be mapped to the screen pixels!");
        AssertEquals(calledPosition.Y, position.Y * 32, "The Y position must be mapped to the screen pixels!");
    }
}
