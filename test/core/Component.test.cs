
using static SimpleGameEngine.Test.TestRunner;

namespace SimpleGameEngine.Test;

[TestClass]
public class ComponentTest {

    public void GivenNewGameAndEntityAttached_whenGameStarts_thenOnStartIsCalledWithEntity (){
        Entity? calledEntity = null;
        var called = false;
        var game = new Game();
        var entity = new Entity(game);
        var component = new Component();
        component.OnStart += (Entity e) => {called = true; calledEntity = e;};
        entity.AttachComponent(component);
        
        game.Start();

        Assert(called == true);
        Assert(calledEntity == entity);
    }

    public void GivenNewGameAndEntityAttached_whenEntityDisabled_thenOnDisableIsCalledWithEntity (){
        Entity? calledEntity = null;
        var called = false;
        var game = new Game();
        var entity = new Entity(game);
        var component = new Component();
        component.OnDisable += (Entity e) => {called = true; calledEntity = e;};
        entity.AttachComponent(component);
        
        entity.Enabled = false;

        Assert(called == true);
        Assert(calledEntity == entity);
    }

    public void GivenNewGameAndEntityAttached_whenEntityDisabledTwiceBeforeEnable_thenOnDisableIsCalledWithEntityOnce (){
        Entity? calledEntity = null;
        var numCalled = 0;
        var game = new Game();
        var entity = new Entity(game);
        var component = new Component();
        component.OnDisable += (Entity e) => {numCalled++; calledEntity = e;};
        entity.AttachComponent(component);
        
        entity.Enabled = false;
        entity.Enabled = false;

        Assert(numCalled == 1);
        Assert(calledEntity == entity);
    }

    public void GivenNewGameAndEntityAttached_whenEntityEnabled_thenOnEnableIsCalledWithEntity (){
        Entity? calledEntity = null;
        var called = false;
        var game = new Game();
        var entity = new Entity(game);
        var component = new Component();
        component.OnEnable += (Entity e) => {called = true; calledEntity = e;};
        entity.AttachComponent(component);
        
        entity.Enabled = false;
        entity.Enabled = true;

        Assert(called == true);
        Assert(calledEntity == entity);
    }

    public void GivenNewGameAndEntityAttached_whenEntityEnabledTwiceBeforeDisable_thenOnEnableIsCalledWithEntityOnce (){
        Entity? calledEntity = null;
        var numCalled = 0;
        var game = new Game();
        var entity = new Entity(game);
        var component = new Component();
        component.OnEnable += (Entity e) => {numCalled++; calledEntity = e;};
        entity.AttachComponent(component);
        
        entity.Enabled = false;
        entity.Enabled = true;
        entity.Enabled = true;

        Assert(numCalled == 1);
        Assert(calledEntity == entity);
    }

    public void GivenNewGameAndEntityAttached_whenEntityDestroyIsProcessed_thenOnDestroyIsCalledWithEntity (){
        Entity? calledEntity = null;
        var called = false;
        var game = new Game();
        var entity = new Entity(game);
        var component = new Component();
        component.OnDestroy += (Entity e) => {called = true; calledEntity = e;};
        entity.AttachComponent(component);
        
        entity.Destroy();
        game.Loop();

        Assert(called == true);
        Assert(calledEntity == entity);
    }

    public void GivenNewGameAndEntityAttached_whenEntityDestroyIsProcessedTwice_thenOnDestroyIsCalledOnce (){
        Entity? calledEntity = null;
        var numCalled = 0;
        var game = new Game();
        var entity = new Entity(game);
        var component = new Component();
        component.OnDestroy += (Entity e) => {numCalled++; calledEntity = e;};
        entity.AttachComponent(component);
        
        entity.Destroy();
        game.Loop();
        entity.Destroy();
        game.Loop();

        Assert(numCalled == 1);
        Assert(calledEntity == entity);
    }
}
