
namespace SimpleGameEngine;
public class MainScreen : Game {

    public MainScreen (){

        
        var exitGameButton = new Entity(this);
    }

    public Entity CreateNewGameButton(){
        var newGameButton = new Entity(this);

        //var foreground = new RenderableComponent();

        return newGameButton;
    }
}
