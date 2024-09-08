using System;
using Galaxias.Client.Gui.Screen;
using Galaxias.Client.Key;
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
    ClientPlayer player;
    private int currentItem;
    public InteractionManagerClient(ClientWorld world, ClientPlayer player)
    {
        this.world = world;
        this.player = player;
    }

    public void Update(GalaxiasClient galaxias, Camera camera, float dTime)
    {
        if (galaxias.IsActive)
        {
            SyncHeldItem();
            var state = Mouse.GetState();
            if (state.LeftButton == ButtonState.Pressed)
            {
                GetMosuePos(camera, out int x, out int y);
                var tileState = world.GetTileState(TileLayer.Main, x, y);
                if (!tileState.IsAir())
                {
                    NetPlayManager.SendToServer(new C2SPlayerDiggingPacket(C2SPlayerDiggingPacket.Action.CreativeBreak, x, y));
                }

            }
            else if (state.RightButton == ButtonState.Pressed)
            {
                GetMosuePos(camera, out int x, out int y);
                NetPlayManager.SendToServer(new C2SUseItemPacket(x, y));
            }
            
        }
    }
    public void GetMosuePos(Camera camera, out int x, out int y)
    {
        Vector2 p = camera.ScreenToWorldSpace(Mouse.GetState().Position);
        x = Utils.Floor(p.X / 8);
        y = Utils.Floor(-p.Y / 8);
    }
    public void SyncHeldItem()
    {
        int i = player.Inventory.onHand;
        if (i != currentItem) { 
            currentItem = i;
            NetPlayManager.SendToServer(new C2SSyncHeldItemPacket(i));
        }
    }
}
