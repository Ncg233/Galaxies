using Galaxies.Core.World.Tiles.State;
using Galaxies.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.World.Tiles;
public class GrassTile : Tile
{
    public GrassTile(TileSettings settings) : base(settings)
    {

    }
    public override void OnNeighborChanged(TileState tileState, AbstractWorld world, TileLayer layer, int x, int y, TileState changedTile)
    {
        if(world.GetTileState(TileLayer.Main, x, y + 1).IsFullTile())
        {
            world.SetTileState(TileLayer.Main, x, y, AllTiles.Dirt.GetDefaultState());
        }
    }
    public override bool CanPlaceThere(TileState state, AbstractWorld world, TileLayer placeLayer, int x, int y)
    {
        return !world.GetTileState(TileLayer.Main, x, y + 1).IsFullTile() && base.CanPlaceThere(state, world, placeLayer, x, y);
    }
    public override void RandomTick(TileState state, AbstractWorld world, int x, int y, Random random)
    {
        if (!world.IsClient)
        {
            int cx = random.Next(-2, 2) + x;
            int cy = random.Next(-2, 2) + y;
            if (world.GetTileState(TileLayer.Main, cx, cy) == AllTiles.Dirt.GetDefaultState())
            {
                if (AllTiles.GrassTile.GetDefaultState().CanPlaceThere(world, TileLayer.Main, cx, cy))
                {
                    world.SetTileState(TileLayer.Main, cx, cy, AllTiles.GrassTile.GetDefaultState());
                }
            }
        }
    }
}
