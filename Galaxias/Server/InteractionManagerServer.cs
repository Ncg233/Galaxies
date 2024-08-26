using Galaxias.Core.World.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Server;
public class InteractionManagerServer
{
    private ServerWorld _world;
    public InteractionManagerServer(ServerWorld world)
    {
        _world = world; 
    }

    public void DestroyTile(int x, int y)
    {
        _world.SetTileState(TileLayer.Main, x, y, AllTiles.Air.GetDefaultState());
    }
}
