using Galaxias.Core.World.Items;
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
    private ServerPlayer _player;
    public InteractionManagerServer(ServerWorld world, ServerPlayer player)
    {
        _world = world;
        _player = player;
    }

    public void DestroyTile(int x, int y)
    {
        _world.SetTileState(TileLayer.Main, x, y, AllTiles.Air.GetDefaultState());
    }
    public void UseItem(ItemPile pile, int x, int y) {
        pile.UseOnTile(_world, _player, x, y);
    }
}
