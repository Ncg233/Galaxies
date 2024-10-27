using Galaxies.Core.World;
using Galaxies.Core.World.Tiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Client.Render;
public interface IStateInfo
{
    public Rectangle GetRenderRect(byte id);
    TileRenderInfo UpdateAdjacencies(AbstractWorld world, TileLayer layer, int x, int y);
    TileRenderInfo DefaultInfo();
}
