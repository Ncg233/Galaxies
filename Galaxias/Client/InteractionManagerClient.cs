using System;
using Galaxias.Client.Main;
using Galaxias.Client.Render;
using Galaxias.Core.Networking;
using Galaxias.Core.Networking.Packet.C2S;
using Galaxias.Core.World;
using Galaxias.Core.World.Entities;
using Galaxias.Core.World.Items;
using Galaxias.Core.World.Tiles;
using Galaxias.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Galaxias.Client;
public class InteractionManagerClient
{
    private ClientWorld world;
    public InteractionManagerClient(ClientWorld world)
    {
        this.world = world;
    }

    public void Update(GalaxiasClient galaxias, Camera camera, float dTime)
    {
        if (galaxias.IsActive)
        {
            var state = Mouse.GetState();
            Vector2 p = camera.ScreenToWorldSpace(state.Position);
            int x = Utils.Floor(p.X / 8);
            int y = Utils.Floor(-p.Y / 8);
            Player player = galaxias.GetPlayer();
            player.HitX = x;
            player.HitY = y;
            if (state.LeftButton == ButtonState.Pressed)
            {
                var tileState = world.GetTileState(TileLayer.Main, x, y);
                if (!tileState.IsAir())
                {
                    NetPlayManager.SendToServer(new C2SPlayerDiggingPacket(C2SPlayerDiggingPacket.Action.CreativeBreak, x, y));
                }

            }
            else if (state.RightButton == ButtonState.Pressed)
            {
                var tileState = world.GetTileState(TileLayer.Main, x, y);
                var item = player.GetItemOnHand().GetItem();
                if (item is TileItem)
                {
                    item.UseOnTile(galaxias.GetWorld(), player, tileState, x, y);
                }
            }

        }
    }
}
