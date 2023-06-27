
using static SimpleGameEngine.Test.TestRunner;

namespace SimpleGameEngine.Test;

public class IDeviceTest {

    public IDevice device;

    public IDeviceTest(){
        device = null;
    }

    public void GivenValidPath_whenMakeImage_thenDoesNotThrowsResourceNotFoundException(){
        var capturedException = false;

        try {
            device.MakeImage("resource.png");
        } catch(ResourceNotFoundException){
            capturedException = true;
        }

        Assert(capturedException == false);
    }

    public void GivenInvalidPath_whenMakeImage_thenThrowsResourceNotFoundException(){
        var capturedException = false;

        try {
            device.MakeImage("invalid path");
        } catch(ResourceNotFoundException){
            capturedException = true;
        }

        Assert(capturedException == true);
    }

    public void GivenValidPath_whenMakeSound_thenDoesNotThrowsResourceNotFoundException(){
        var capturedException = false;

        try {
            device.MakeSound("resource.mp3");
        } catch(ResourceNotFoundException){
            capturedException = true;
        }

        Assert(capturedException == false);
    }

    public void GivenInvalidPath_whenMakeSound_thenThrowsResourceNotFoundException(){
        var capturedException = false;

        try {
            device.MakeSound("invalid path");
        } catch(ResourceNotFoundException){
            capturedException = true;
        }

        Assert(capturedException == true);
    }

    public void GivenValidPath_whenMakeFont_thenDoesNotThrowsResourceNotFoundException(){
        var capturedException = false;

        try {
            device.MakeFont("resource.ttf");
        } catch(ResourceNotFoundException){
            capturedException = true;
        }

        Assert(capturedException == false);
    }

    public void GivenInvalidPath_whenMakeFont_thenThrowsResourceNotFoundException(){
        var capturedException = false;

        try {
            device.MakeFont("invalid path");
        } catch(ResourceNotFoundException){
            capturedException = true;
        }

        Assert(capturedException == true);
    }


    public void GivenValidName_whenMakeColor_thenDoesNotThrowsResourceNotFoundException(){
        var capturedException = false;

        try {
            device.MakeColorFromName("black");
        } catch(ResourceNotFoundException){
            capturedException = true;
        }

        Assert(capturedException == false);
    }

    public void GivenInvalidName_whenMakeColor_thenThrowsResourceNotFoundException(){
        var capturedException = false;

        try {
            device.MakeColorFromName("invalid name");
        } catch(ResourceNotFoundException){
            capturedException = true;
        }

        Assert(capturedException == true);
    }

    public void GivenValues_whenMakeColor_thenDoesNotThrowsResourceNotFoundException(){
        
    }

    public void GivenFont_whenMakeText_thenDoesNotThrowsResourceNotFoundException(){
        
    }

    public void GivenImage_whenMakeSpriteSheet_thenDoesNotThrowsResourceNotFoundException(){
        
    }
}
