using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
