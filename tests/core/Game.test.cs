using static SimpleGameEngine.Test.TestRunner;

namespace SimpleGameEngine.Test;

[TestClass]
public class GameTest {

    public void GivenAttachedEntity_whenEntityDestroyIsProcessed_thenDoesNotThrowsException (){
        var game = new Game();
        var entity = new Entity(game);
        var capturedException = false;
        
        entity.Destroy();
        try {
            game.Loop();
        } catch {
            capturedException = true;
        }

        Assert(capturedException == false);
    }

    public void GivenAttachedEntity_whenEntityDestroyIsProcessedTwice_thenDoesNotThrowsException (){
        var game = new Game();
        var entity = new Entity(game);
        var capturedException = false;
        
        try {
            entity.Destroy();
            game.Loop();
            entity.Destroy();
            game.Loop();
        } catch {
            capturedException = true;
        }
        
        Assert(capturedException == false);
    }
}
