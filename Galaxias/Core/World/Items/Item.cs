using System.Numerics;
using Galaxias.Core.World;
using Galaxias.Core.World.Entities;
using Galaxias.Core.World.Tiles;

namespace Galaxias.Core.World.Items;
public class Item
{
    public virtual bool Use(AbstractWorld world, AbstractPlayerEntity player, ItemPile item)
    {
        return true;
    }
    public virtual bool UseOnTile(AbstractWorld world, AbstractPlayerEntity player, int x, int y)
    {
        return true;
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