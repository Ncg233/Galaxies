using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaxies.Core.World.Tiles.State;
using Galaxies.Util;

namespace Galaxies.Core.World.Tiles;
public class TorchTile : Tile
{
    public TorchTile(TileSettings settings) : base(settings)
    {
    }

    public override int GetLight(TileState tileState)
    {
        return GameConstants.MaxLight;
    }
    public override bool IsFullTile()
    {
        return false;
    }
    public override bool CanCollide()
    {
        return false;
    }
    public override bool CanStay(TileState state, AbstractWorld world, TileLayer layer, int x, int y)
    {
        return CanPlaceThere(state, world, layer, x, y);
    }
    public override bool CanPlaceThere(TileState state, AbstractWorld world, TileLayer layer, int x, int y)
    {
        if (world.GetTileState(TileLayer.Main, x, y - 1).IsFullTile())
        {
            return true;
        }
        else if (world.GetTileState(TileLayer.Background, x, y).IsFullTile())
        {
            return true;
        }
        return false;
    }
}
