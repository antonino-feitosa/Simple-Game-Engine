
using System.Threading;

public class InputComponent : Component {

}

public class InputSystem : ISystem
{

    protected HashSet <char> _pressed;

    public InputSystem(){
        _pressed = new HashSet<char>();
    }

    public void Start()
    {

    }

    public void ListenInput(){

    }

    public void Process()
    {

    }

    public void Finish()
    {

    }
}
