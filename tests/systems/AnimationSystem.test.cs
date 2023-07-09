
using static SimpleGameEngine.Test.TestRunner;

namespace SimpleGameEngine.Test;

[TestClass]
public class AnimationSystemTest
{
    public void GivenAnimatableComponentWithZeroUpdatebyFrame_whenRunning_thenRenderDifferentSprites()
    {
        var dimension = new Dimension(32, 32);
        var images = new Dictionary<int, IImage>{
            {0, new IImageStub() { OnGetDimension = () => dimension, OnRender = (point) => { } }},
            {1, new IImageStub() { OnGetDimension = () => dimension, OnRender = (point) => { } }},
            {2, new IImageStub() { OnGetDimension = () => dimension, OnRender = (point) => { } }}
        };
        var imagesToIndex = new Dictionary<IImage, int> { { images[0], 0 }, { images[1], 1 }, { images[2], 2 } };
        var renderingSystem = new RenderingSystem(new Point(), new Dimension(1, 1));
        var renderComponent = new RenderableComponent(renderingSystem, images[0], new Point());
        var system = new AnimationSystem();
        var sprite = new ISpriteSheetStub() { OnGetLength = () => 3, OnGetDimension = () => dimension, OnGetSprite = (i) => images[i] };
        var component = new AnimatableComponent(system, renderComponent, sprite) { UpdatesBetweenFrames = 0 };

        var activeImages = new List<int> { imagesToIndex[renderComponent.Image] };
        system.Process();
        activeImages.Add(imagesToIndex[renderComponent.Image]);
        system.Process();
        activeImages.Add(imagesToIndex[renderComponent.Image]);

        AssertEquals(activeImages[0], 0, "The first image is incorret!");
        AssertEquals(activeImages[1], 1, "The second image is incorret!");
        AssertEquals(activeImages[2], 2, "The third image is incorret!");
    }

    public void GivenAnimatableComponent_whenNotRunning_thenDoNotChangeSprites()
    {
        var dimension = new Dimension(32, 32);
        var images = new Dictionary<int, IImage>{
            {0, new IImageStub() { OnGetDimension = () => dimension, OnRender = (point) => { } }},
            {1, new IImageStub() { OnGetDimension = () => dimension, OnRender = (point) => { } }},
            {2, new IImageStub() { OnGetDimension = () => dimension, OnRender = (point) => { } }}
        };
        var imagesToIndex = new Dictionary<IImage, int> { { images[0], 0 }, { images[1], 1 }, { images[2], 2 } };
        var renderingSystem = new RenderingSystem(new Point(), new Dimension(1, 1));
        var renderComponent = new RenderableComponent(renderingSystem, images[0], new Point());
        var system = new AnimationSystem();
        var sprite = new ISpriteSheetStub() { OnGetLength = () => 3, OnGetDimension = () => dimension, OnGetSprite = (i) => images[i] };
        var component = new AnimatableComponent(system, renderComponent, sprite)
        {
            Running = false,
            UpdatesBetweenFrames = 0
        };

        var activeImages = new List<int> { imagesToIndex[renderComponent.Image] };
        system.Process();
        activeImages.Add(imagesToIndex[renderComponent.Image]);
        system.Process();
        activeImages.Add(imagesToIndex[renderComponent.Image]);

        AssertEquals(activeImages[0], 0, "The image must not change!");
        AssertEquals(activeImages[1], 0, "The image must not change!");
        AssertEquals(activeImages[2], 0, "The image must not change!");
    }

    public void GivenAnimatableComponent_whenRunning_thenLoopSprites()
    {
        var dimension = new Dimension(32, 32);
        var images = new Dictionary<int, IImage>{
            {0, new IImageStub() { OnGetDimension = () => dimension, OnRender = (point) => { } }},
            {1, new IImageStub() { OnGetDimension = () => dimension, OnRender = (point) => { } }},
            {2, new IImageStub() { OnGetDimension = () => dimension, OnRender = (point) => { } }}
        };
        var imagesToIndex = new Dictionary<IImage, int> { { images[0], 0 }, { images[1], 1 }, { images[2], 2 } };
        var renderingSystem = new RenderingSystem(new Point(), new Dimension(1, 1));
        var renderComponent = new RenderableComponent(renderingSystem, images[0], new Point());
        var system = new AnimationSystem();
        var sprite = new ISpriteSheetStub() { OnGetLength = () => 3, OnGetDimension = () => dimension, OnGetSprite = (i) => images[i] };
        var component = new AnimatableComponent(system, renderComponent, sprite) { UpdatesBetweenFrames = 0 };

        var activeImages = new List<int> { imagesToIndex[renderComponent.Image] };
        system.Process();
        activeImages.Add(imagesToIndex[renderComponent.Image]);
        system.Process();
        activeImages.Add(imagesToIndex[renderComponent.Image]);
        system.Process();
        activeImages.Add(imagesToIndex[renderComponent.Image]);
        system.Process();
        activeImages.Add(imagesToIndex[renderComponent.Image]);
        system.Process();

        AssertEquals(activeImages[0], 0, "The first image is incorret!");
        AssertEquals(activeImages[1], 1, "The second image is incorret!");
        AssertEquals(activeImages[2], 2, "The third image is incorret!");
        AssertEquals(activeImages[3], 0, "The fourth image is incorret!");
        AssertEquals(activeImages[4], 1, "The fifth image is incorret!");
    }

    public void GivenAnimatableComponentWithSpecificSequence_whenRunning_thenFollowTheSequence()
    {
        var dimension = new Dimension(32, 32);
        var images = new Dictionary<int, IImage>{
            {0, new IImageStub() { OnGetDimension = () => dimension, OnRender = (point) => { } }},
            {1, new IImageStub() { OnGetDimension = () => dimension, OnRender = (point) => { } }},
            {2, new IImageStub() { OnGetDimension = () => dimension, OnRender = (point) => { } }}
        };
        var imagesToIndex = new Dictionary<IImage, int> { { images[0], 0 }, { images[1], 1 }, { images[2], 2 } };
        var renderingSystem = new RenderingSystem(new Point(), new Dimension(1, 1));
        var renderComponent = new RenderableComponent(renderingSystem, images[0], new Point());
        var system = new AnimationSystem();
        var sprite = new ISpriteSheetStub() { OnGetLength = () => 3, OnGetDimension = () => dimension, OnGetSprite = (i) => images[i] };
        var sequence = new List<int> { 2, 1, 2, 0, 2 };
        var component = new AnimatableComponent(system, renderComponent, sprite)
        {
            UpdatesBetweenFrames = 0,
            Sequence = sequence
        };

        var activeImages = new List<int> { imagesToIndex[renderComponent.Image] };
        system.Process();
        activeImages.Add(imagesToIndex[renderComponent.Image]);
        system.Process();
        activeImages.Add(imagesToIndex[renderComponent.Image]);
        system.Process();
        activeImages.Add(imagesToIndex[renderComponent.Image]);
        system.Process();
        activeImages.Add(imagesToIndex[renderComponent.Image]);
        system.Process();

        AssertEquals(activeImages[0], sequence[0], "The first image is incorret!");
        AssertEquals(activeImages[1], sequence[1], "The second image is incorret!");
        AssertEquals(activeImages[2], sequence[2], "The third image is incorret!");
        AssertEquals(activeImages[3], sequence[3], "The fourth image is incorret!");
        AssertEquals(activeImages[4], sequence[4], "The fifth image is incorret!");
    }

    public void GivenAnimatableComponentPaused_whenResume_thenContinueFromLastFrame()
    {
        var dimension = new Dimension(32, 32);
        var images = new Dictionary<int, IImage>{
            {0, new IImageStub() { OnGetDimension = () => dimension, OnRender = (point) => { } }},
            {1, new IImageStub() { OnGetDimension = () => dimension, OnRender = (point) => { } }},
            {2, new IImageStub() { OnGetDimension = () => dimension, OnRender = (point) => { } }}
        };
        var imagesToIndex = new Dictionary<IImage, int> { { images[0], 0 }, { images[1], 1 }, { images[2], 2 } };
        var renderingSystem = new RenderingSystem(new Point(), new Dimension(1, 1));
        var renderComponent = new RenderableComponent(renderingSystem, images[0], new Point());
        var system = new AnimationSystem();
        var sprite = new ISpriteSheetStub() { OnGetLength = () => 3, OnGetDimension = () => dimension, OnGetSprite = (i) => images[i] };
        var component = new AnimatableComponent(system, renderComponent, sprite) { UpdatesBetweenFrames = 0 };

        var activeImages = new List<int> { imagesToIndex[renderComponent.Image] };
        system.Process();
        component.Running = false;
        activeImages.Add(imagesToIndex[renderComponent.Image]);
        system.Process();
        component.Running = true;
        activeImages.Add(imagesToIndex[renderComponent.Image]);
        system.Process();
        activeImages.Add(imagesToIndex[renderComponent.Image]);


        AssertEquals(activeImages[0], 0, "The first image is incorret!");
        AssertEquals(activeImages[1], 1, "The second image is incorret!");
        AssertEquals(activeImages[2], 1, "The third image is incorret!");
        AssertEquals(activeImages[3], 2, "The fourth image is incorret!");
    }

    public void GivenAnimatableComponent_whenReset_thenStartFromTheFirstFrame()
    {
        var dimension = new Dimension(32, 32);
        var images = new Dictionary<int, IImage>{
            {0, new IImageStub() { OnGetDimension = () => dimension, OnRender = (point) => { } }},
            {1, new IImageStub() { OnGetDimension = () => dimension, OnRender = (point) => { } }},
            {2, new IImageStub() { OnGetDimension = () => dimension, OnRender = (point) => { } }}
        };
        var imagesToIndex = new Dictionary<IImage, int> { { images[0], 0 }, { images[1], 1 }, { images[2], 2 } };
        var renderingSystem = new RenderingSystem(new Point(), new Dimension(1, 1));
        var renderComponent = new RenderableComponent(renderingSystem, images[0], new Point());
        var system = new AnimationSystem();
        var sprite = new ISpriteSheetStub() { OnGetLength = () => 3, OnGetDimension = () => dimension, OnGetSprite = (i) => images[i] };
        var component = new AnimatableComponent(system, renderComponent, sprite) { UpdatesBetweenFrames = 0 };

        var activeImages = new List<int> { imagesToIndex[renderComponent.Image] };
        system.Process();
        component.Reset();
        activeImages.Add(imagesToIndex[renderComponent.Image]);

        AssertEquals(activeImages[0], 0, "The first image is incorret!");
        AssertEquals(activeImages[1], 0, "The second image is incorret!");
    }

    public void GivenAnimatableComponent_whenSetEmptySequence_thenThrowsArgumentException()
    {
        var dimension = new Dimension(32, 32);
        var image = new IImageStub() { OnGetDimension = () => dimension, OnRender = (point) => { } };
        var renderingSystem = new RenderingSystem(new Point(), new Dimension(1, 1));
        var renderComponent = new RenderableComponent(renderingSystem, image, new Point());
        var system = new AnimationSystem();
        var sprite = new ISpriteSheetStub() { OnGetLength = () => 1, OnGetDimension = () => dimension, OnGetSprite = (i) => image };
        var sequence = new List<int> { };
        var component = new AnimatableComponent(system, renderComponent, sprite) { UpdatesBetweenFrames = 0 };
        var capturedException = false;

        try
        {
            component.Sequence = sequence;
        }
        catch (ArgumentException)
        {
            capturedException = true;
        }

        AssertTrue(capturedException, "The sequence can not be empty!");
    }

    public void GivenAnimatableComponent_whenSetSequenceWithNegativeIndex_thenThrowsArgumentException()
    {
        var dimension = new Dimension(32, 32);
        var image = new IImageStub() { OnGetDimension = () => dimension, OnRender = (point) => { } };
        var renderingSystem = new RenderingSystem(new Point(), new Dimension(1, 1));
        var renderComponent = new RenderableComponent(renderingSystem, image, new Point());
        var system = new AnimationSystem();
        var sprite = new ISpriteSheetStub() { OnGetLength = () => 1, OnGetDimension = () => dimension, OnGetSprite = (i) => image };
        var sequence = new List<int> { };
        var component = new AnimatableComponent(system, renderComponent, sprite) { UpdatesBetweenFrames = 0 };
        var capturedException = false;

        try
        {
            component.Sequence = sequence;
        }
        catch (ArgumentException)
        {
            capturedException = true;
        }

        AssertTrue(capturedException, "The sequence can not have negative indexes!");
    }

    public void GivenAnimatableComponent_whenSetSequenceWithIndexWithtoutSpriteIndex_thenThrowsArgumentException()
    {
        var dimension = new Dimension(32, 32);
        var image = new IImageStub() { OnGetDimension = () => dimension, OnRender = (point) => { } };
        var renderingSystem = new RenderingSystem(new Point(), new Dimension(1, 1));
        var renderComponent = new RenderableComponent(renderingSystem, image, new Point());
        var system = new AnimationSystem();
        var sprite = new ISpriteSheetStub() { OnGetLength = () => 1, OnGetDimension = () => dimension, OnGetSprite = (i) => image };
        var sequence = new List<int> { };
        var component = new AnimatableComponent(system, renderComponent, sprite) { UpdatesBetweenFrames = 0 };
        var capturedException = false;

        try
        {
            component.Sequence = sequence;
        }
        catch (ArgumentException)
        {
            capturedException = true;
        }

        AssertTrue(capturedException, "The sequence can not have index whitout sprites!");
    }
}
