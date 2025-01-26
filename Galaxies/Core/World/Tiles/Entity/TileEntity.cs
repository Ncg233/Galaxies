using Galaxies.Core.Data;
using Galaxies.Utill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.World.Tiles.Entity;
public abstract class TileEntity
{
    public readonly AbstractWorld world;
    public readonly TilePos Pos;
    public TileEntity(AbstractWorld world, TilePos pos)
    {
        this.world = world;
        Pos = pos;
    }

    public abstract void Save(DataSet data);

    public abstract void Read(DataSet data);
}
