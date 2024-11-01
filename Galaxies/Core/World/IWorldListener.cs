using Galaxies.Core.World.Tiles;
using Galaxies.Core.World.Tiles.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.World;
public interface IWorldListener
{
    void AddParticle(TileState state,TileLayer layer, int x, int y);
    public void OnNotifyNeighbor(TileLayer layer, int x , int y, TileState state, TileState changeTile);
}
