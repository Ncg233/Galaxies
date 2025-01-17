using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.World.Tiles.Entity;
public abstract class TileEntity
{
    protected readonly AbstractWorld world;
    protected readonly int X, Y;
    public TileEntity(AbstractWorld world, int x, int y)
    {
        this.world = world;
        X = x;
        Y = y;
    }
    
}
