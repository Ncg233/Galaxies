using System.Numerics;
using Galasias.Core.World.Entities;
using Galaxias.Core.World;
using Galaxias.Core.World.Entities;
using Galaxias.Core.World.Tiles;

namespace Galasias.Core.World.Items;
public class Item{
    public virtual bool Use(AbstractWorld world, Player player, ItemPile item){
        return true;
    }
    public virtual bool UseOnTile(AbstractWorld world, Player player, TileState tile, int x,int y){
        return true;
    }
    public virtual bool HurtEntity(AbstractWorld world, LivingEntity player, LivingEntity entity){
        return true;
    }
    public virtual bool OnUsingTime(AbstractWorld world, Player player, ItemPile itemPile, float second){
        return true;
    }
    public virtual bool ReleaseUse(AbstractWorld world, Player player, ItemPile itemPile, float second){
        return true;
    }
    public virtual bool OnHandTime(AbstractWorld world, Player player, ItemPile itemPile){
        return true;
    }
    
}