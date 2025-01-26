using Galaxies.Core.World.Tiles.Entity;
using Galaxies.Core.World.Tiles.State;
using Galaxies.Utill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.World.Tiles;
internal interface ITileEntityProvider
{
    public TileEntity CreateTileEntity(AbstractWorld world, TileState tileState, TilePos pos);
}
