using Galaxies.Core.World.Tiles.Entity;
using Galaxies.Core.World.Tiles.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.World.Tiles;
public class ChestTile : MultiTile, ITileEntityProvider
{
    public ChestTile(TileSettings settings) : base(2, 2, settings)
    {

    }

    public void CreateScreenHandler()
    {
        
    }

    public TileEntity CreateTileEntity(AbstractWorld world, TileState tileState, int x, int y)
    {
        return new ChestTileEntity(world, x, y);
    }
}

