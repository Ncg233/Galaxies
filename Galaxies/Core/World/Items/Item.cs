using System.Numerics;
using Galaxies.Core.World;
using Galaxies.Core.World.Entities;

namespace Galaxies.Core.World.Items;
public class Item
{
    public virtual bool Use(AbstractWorld world, AbstractPlayerEntity player, int x, int y)
    {
        return false;
    }
    public virtual bool HurtEntity(AbstractWorld world, LivingEntity player, LivingEntity entity)
    {
        return true;
    }
    public virtual bool OnUsingTime(AbstractWorld world, AbstractPlayerEntity player, ItemPile itemPile, float second)
    {
        return true;
    }
    public virtual bool ReleaseUse(AbstractWorld world, AbstractPlayerEntity player, ItemPile itemPile, float second)
    {
        return true;
    }
    public virtual bool OnHandTime(AbstractWorld world, AbstractPlayerEntity player, ItemPile itemPile)
    {
        return true;
    }

}