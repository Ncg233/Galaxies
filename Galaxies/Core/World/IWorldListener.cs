using Galaxies.Core.World.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.World;
public interface IWorldListener
{
    public void OnNotifyNeighbor(TileLayer layer, int x , int y, TileState state, TileState changeTile);
}
