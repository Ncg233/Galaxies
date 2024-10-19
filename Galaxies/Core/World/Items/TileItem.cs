using System.Numerics;
using Galaxies.Core.World.Entities;
using Galaxies.Core.World.Tiles;

namespace Galaxies.Core.World.Items;
public class TileItem : Item
{
    private Tile tile;
    private TileLayer layer;
    public TileItem(Tile tile, TileLayer layer)
    {
        this.tile = tile;
        this.layer = layer;
    }
    public override bool Use(AbstractWorld world, AbstractPlayerEntity player, int x, int y)
    {
        var tileState = world.GetTileState(layer, x, y);
        if (!tileState.GetTile().IsAir())
        {
            return false;

        }
        var state = tile.GetPlaceState(world, player, x, y);
        world.SetTileState(layer, x, y, state);
        return true;

    }
}