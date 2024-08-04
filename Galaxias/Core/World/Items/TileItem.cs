using Galaxias.Core.World;
using Galaxias.Core.World.Entities;
using Galaxias.Core.World.Tiles;

namespace Galasias.Core.World.Items;
public class TileItem : Item{
    private Tile tile;
    public TileItem(Tile tile){
        this.tile = tile;
    }
    public override Item use(AbstractWorld world,Player player,Item item){
        return item;
    }
}