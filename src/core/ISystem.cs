
namespace SimpleGameEngine;

public interface ISystem
{
    public void Start(IDevice device);
    public void Process();
}
