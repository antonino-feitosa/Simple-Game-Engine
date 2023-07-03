
using static SimpleGameEngine.Test.TestRunner;

namespace SimpleGameEngine.Test;

[TestClass]
public class PositionSystemTest
{
    // move Up
    public void GivenComponent_whenMoveUp_thenChangePositionYUp(){

    }

    public void GivenComponent_whenMoveUp_thenDoNotChangePositionX(){

    }

    public void GivenComponent_whenMoveUp_thenFireOnMove(){

    }

    public void GivenComponent_whenMoveUp_thenFireOnMoveWithSameX(){

    }

    public void GivenComponent_whenMoveUp_thenFireOnMoveWithYUp(){

    }

    // out of Bounds

    public void GivenComponentAtUpBorder_whenMoveUp_thenFireOutOfBounds (){

    }

    public void GivenComponentAtUpBorder_whenMoveUp_thenFireOutOfBoundsWithSameX (){

    }

    public void GivenComponentAtUpBorder_whenMoveUp_thenFireOutOfBoundsWithUpY (){

    }

    public void GivenComponentAtUpBorder_whenMoveUp_thenDoNotChangePosition (){

    }

    // move with dependency 2
    public void GivenComponentAWithComponentBUp_whenAMoveUpBMoveUp_thenTheyFireOnMove(){

    }

    public void GivenComponentAWithComponentBUp_whenBMoveUpAMoveUp_thenTheyFireOnMove(){

    }

    // move with dependency 3

    public void GivenComponentAWithComponentBUpWithComponentCUp_whenAMoveUpBMoveUpCMoveUp_thenTheyFireOnMove(){

    }

    public void GivenComponentAWithComponentBUpWithComponentCUp_whenAMoveUpCMoveUpBMoveUp_thenTheyFireOnMove(){

    }

    public void GivenComponentAWithComponentBUpWithComponentCUp_whenBMoveUpAMoveUpCMoveUp_thenTheyFireOnMove(){

    }

    public void GivenComponentAWithComponentBUpWithComponentCUp_whenBMoveUpCMoveUpAMoveUp_thenTheyFireOnMove(){

    }

    public void GivenComponentAWithComponentBUpWithComponentCUp_whenCMoveUpAMoveUpBMoveUp_thenTheyFireOnMove(){

    }

    public void GivenComponentAWithComponentBUpWithComponentCUp_whenCMoveUpBMoveUpCMoveUp_thenTheyFireOnMove(){

    }


    // collisions stand
    public void GivenStandComponentA_whenComponentBMoveUpToA_thenTheyFireCollision(){

    }

    public void GivenStandComponentA_whenComponentBMoveUpToA_thenFireCollisionToAWithB(){

    }
    
    public void GivenStandComponentA_whenComponentBMoveUpToA_thenFireCollisionToBWithA(){
        
    }

    public void GivenStandComponentA_whenComponentBMoveUpToA_thenDoNotChangePositions(){

    }

    // move collision
    public void GivenComponentAWithComponentBUp_whenAMoveUpAndBMoveDown_thenDoNotChangePositions(){

    }

    public void GivenComponentAWithComponentBTwoUp_whenAMoveUpAndBMoveDown_thenDoNotChangePositions(){

    }
}
