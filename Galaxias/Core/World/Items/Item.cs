using System.Numerics;
using Galaxias.Core.World;
using Galaxias.Core.World.Entities;
using Galaxias.Core.World.Tiles;

namespace Galaxias.Core.World.Items;
public class Item
{
    public virtual bool Use(World world, Player player, ItemPile item)
    {
        return true;
    }
    public virtual bool UseOnTile(World world, Player player, TileState tile, int x, int y)
    {
        return true;
    }
    public virtual bool HurtEntity(World world, LivingEntity player, LivingEntity entity)
    {
        return true;
    }
    public virtual bool OnUsingTime(World world, Player player, ItemPile itemPile, float second)
    {
        return true;
    }
    public virtual bool ReleaseUse(World world, Player player, ItemPile itemPile, float second)
    {
        return true;
    }
    public virtual bool OnHandTime(World world, Player player, ItemPile itemPile)
    {
        return true;
    }

}