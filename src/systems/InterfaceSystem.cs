
namespace SimpleGameEngine;

public class InterfaceSystem : SystemBase<InterfaciableComponent>
{
    private InterfaciableComponent? _selectedMouseDown;
    private InterfaciableComponent? _selectedMouseUp;

    private bool _fireReset;
    private bool _fireMouseUp;
    private bool _fireMouseDown;

    public override void Start(IDevice device)
    {
        _fireReset = false;
        _fireMouseUp = false;
        _fireMouseDown = false;
        device.RegisterMouseDown(MouseButton.Left, OnMouseDown);
        device.RegisterMouseUp(MouseButton.Left, OnMouseUp);
    }
    public override void Process()
    {
        if(_fireMouseDown){
            _selectedMouseDown?.OnMouseDown?.Invoke();
            _fireMouseDown = false;
        }
        if(_fireMouseUp){
            _selectedMouseUp?.OnMouseUp?.Invoke();
            _fireMouseDown = false;
            _selectedMouseUp = null;
        }
        if(_fireReset){
            _selectedMouseDown?.OnReset?.Invoke();
            _selectedMouseDown = null;
            _selectedMouseUp = null;
        }
    }

    protected void OnMouseDown(Point point)
    {
        _selectedMouseDown = Components.FirstOrDefault(c => c.Bounds.Contains(point));
        _fireMouseDown = _selectedMouseDown is not null;
    }

    protected void OnMouseUp(Point point)
    {
        _selectedMouseUp = Components.FirstOrDefault(c => c.Bounds.Contains(point));
        _fireMouseUp = _selectedMouseUp is not null && _selectedMouseDown == _selectedMouseUp;
        _fireReset = _selectedMouseDown is not null && _selectedMouseDown != _selectedMouseUp;
    }
}
