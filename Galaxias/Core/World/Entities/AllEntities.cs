using System.Collections.Generic;

namespace Galaxias.Core.World.Entities;
public class AllEntities
{
    public static readonly Dictionary<string, Entity> entityRegister = new Dictionary<string, Entity>();
    public static readonly Entity PlayerEntity = Register("player", new Player(null));
    private static Entity Register(string name, Entity entity)
    {
        entityRegister.Add(name, entity);
        return entity;
    }
}
