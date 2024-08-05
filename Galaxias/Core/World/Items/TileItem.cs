using System.Numerics;
using Galaxias.Core.World;
using Galaxias.Core.World.Entities;
using Galaxias.Core.World.Tiles;

namespace Galasias.Core.World.Items;
public class TileItem : Item{
    private Tile tile;
    public TileItem(Tile tile){
        this.tile = tile;
    }
    public override bool UseOn(AbstractWorld world,Player player,Tile tile,int x,int y){
        var tileState = world.GetTileState(TileLayer.Main, x, y);
                if (tileState.GetTile() == AllTiles.Air && tile.OnPlace(world, x, y, tileState))
                {
                    world.SetTileState(TileLayer.Main, x, y, this.tile.GetDefaultState());
                }
        return true;
    }
}