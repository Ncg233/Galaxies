using System.Collections.Generic;

namespace Galaxias.Core.World.Entities;
public class AllEntityType
{
    public static readonly Dictionary<string, EntityType> entityRegister = [];
    public static readonly EntityType PlayerEntity = Register("player", new EntityType());
    private static EntityType Register(string name, EntityType entity)
    {
        entityRegister.Add(name, entity);
        return entity;
    }
}
