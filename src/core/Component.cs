
namespace SGE;

public class Component
{
    public string Name = "Component";
    protected internal Entity _entity;

    public Component(Entity entity){
        _entity = entity;
        _entity.AttachComponent(this);
    }

    public override string ToString()
    {
        return Name;
    }
}
