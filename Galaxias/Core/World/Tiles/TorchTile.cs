using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Core.World.Tiles;
public class TorchTile : Tile
{
    public override int GetLight(TileState tileState)
    {
        return 127;
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
