
namespace SGE;

public class Component
{
    protected internal Entity _entity;

    public Component(Entity entity){
        _entity = entity;
        _entity.AttachComponent(this);
    }
}
