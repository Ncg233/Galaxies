using System.Numerics;
using Galaxias.Core.World;
using Galaxias.Core.World.Entities;
using Galaxias.Core.World.Tiles;

namespace Galaxias.Core.World.Items;
public class TileItem : Item
{
    private Tile tile;
    public TileItem(Tile tile)
    {
        this.tile = tile;
    }
    public override bool UseOnTile(AbstractWorld world, AbstractPlayerEntity player, int x, int y)
    {
        var tileState = world.GetTileState(TileLayer.Main, x, y);
        if (tileState.GetTile() == AllTiles.Air)
        {
            world.SetTileState(TileLayer.Main, x, y, tile.GetDefaultState());
        }
        return true;
    }
}