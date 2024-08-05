using Galaxias.Core.World;
using Galaxias.Core.World.Entities;
using Galaxias.Core.World.Tiles;

namespace Galasias.Core.World.Items;
public class Item{
    public virtual Item Use(AbstractWorld world, Player player, Item item){
        return item;
    }
    public virtual Item UseOnTile(AbstractWorld world, Player player, TileState state,Item item)
    {
        return item;
    }
    public Item GetItem(){
        return this;
    }
}