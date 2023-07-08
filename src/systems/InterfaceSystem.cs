
namespace SimpleGameEngine;

public class InterfaceSystem : SystemBase<InterfaciableComponent>
{
    private InterfaciableComponent? _selectedMouseDown;
    private InterfaciableComponent? _selectedMouseUp;

    private enum State { WaitingMouseDown, FireMouseDown, WaitingMouseUp, FireMouseUp, FireReset };
    private State _state;

    public InterfaceSystem()
    {
        _state = State.WaitingMouseDown;
    }
    public override void Start(IDevice device)
    {
        _state = State.WaitingMouseDown;
        device.RegisterMouseDown(MouseButton.Left, OnMouseDown);
        device.RegisterMouseUp(MouseButton.Left, OnMouseUp);
    }
    public override void Process()
    {
        switch (_state)
        {
            case State.FireMouseDown:
                _selectedMouseDown?.OnMouseDown?.Invoke();
                _state = State.WaitingMouseUp;
                break;
            case State.FireMouseUp:
                if (_selectedMouseDown == _selectedMouseUp)
                    _selectedMouseUp?.OnMouseUp?.Invoke();
                else
                    _selectedMouseDown?.OnReset?.Invoke();
                _selectedMouseDown = null;
                _selectedMouseUp = null;
                _state = State.WaitingMouseDown;
                break;
            case State.FireReset:
                _selectedMouseDown?.OnReset?.Invoke();
                _selectedMouseDown = null;
                _selectedMouseUp = null;
                break;
        }
    }

    protected void OnMouseDown(Point point)
    {
        _selectedMouseDown = Components.Where(c => c.Bounds.Contains(point)).First();
        if (_state == State.WaitingMouseDown && _selectedMouseDown is not null)
            _state = State.FireMouseDown;
    }

    protected void OnMouseUp(Point point)
    {
        _selectedMouseUp = Components.Where(c => c.Bounds.Contains(point)).First();
        if (_state == State.WaitingMouseUp)
        {
            if (_selectedMouseUp is not null)
                _state = State.FireMouseUp;
            else
                _state = State.FireReset;
        }
    }
}
