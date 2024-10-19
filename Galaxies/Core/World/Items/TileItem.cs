using System.Numerics;
using Galaxies.Core.World.Entities;
using Galaxies.Core.World.Tiles;

namespace Galaxies.Core.World.Items;
public class TileItem : Item
{
    private Tile tile;
    public TileItem(Tile tile)
    {
        this.tile = tile;
    }
    public override bool Use(AbstractWorld world, AbstractPlayerEntity player, int x, int y)
    {
        var tileState = world.GetTileState(TileLayer.Main, x, y);
        if (!tileState.GetTile().IsAir())
        {
            return false;
        }
        var state = tile.GetPlaceState(world, player, x, y);
        world.SetTileState(TileLayer.Main, x, y, state);
        return true;
    }
}