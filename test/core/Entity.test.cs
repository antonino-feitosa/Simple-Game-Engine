using static SimpleGameEngine.Test.TestRunner;

namespace SimpleGameEngine.Test;

[TestClass]
public class EntityTest {

    public void GivenAttachedComponentX_whenGetComponentX_thenNotThrowsArgumentException (){
        var gameDummy = new Game();
        var entity = new Entity(gameDummy);
        var component = new Component();
        entity.AttachComponent(component);

        var capturedException = false;
        try {
            entity.GetComponent<Component>();
        } catch (ArgumentException){
            capturedException = true;
        }

        Assert(capturedException == false);
    }

    public void GivenNotAttachedComponent_whenGetComponent_thenThrowsArgumentException (){
        var gameDummy = new Game();
        var entity = new Entity(gameDummy);

        var capturedException = false;
        try {
            entity.GetComponent<Component>();
        } catch (ArgumentException){
            capturedException = true;
        }

        Assert(capturedException == true);
    }
}
