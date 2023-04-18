
namespace SGE;

public class Component
{
    public string Name = "Component";
    protected internal Entity? _entity;

    public override string ToString()
    {
        return Name;
    }
}
