using System;
using Galaxias.Client.Gui.Screen;
using Galaxias.Client.Key;
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
using SharpDX.Direct2D1;

namespace Galaxias.Client;
public class InteractionManager
{
    private AbstractWorld world;
    AbstractPlayerEntity player;
    private int currentItem;
    public InteractionManager(AbstractWorld world, AbstractPlayerEntity player)
    {
        this.world = world;
        this.player = player;
    }

    public void Update(Main galaxias, Camera camera, float dTime)
    {
         
        var state = Mouse.GetState();
        if (state.LeftButton == ButtonState.Pressed)
        {
            
            GetMosuePos(camera, out int x, out int y);
            var tileState = world.GetTileState(TileLayer.Main, x, y);
            if (!tileState.IsAir())
            {
                //NetPlayManager.SendToServer(new C2SPlayerDiggingPacket(C2SPlayerDiggingPacket.Action.CreativeBreak, x, y));
                PlayerDigging(world, player, x, y);
            }

        }
        else if (state.RightButton == ButtonState.Pressed)
        {
            //SyncHeldItem();
            GetMosuePos(camera, out int x, out int y);
            PlayerUseItem(world, player, x, y);
            //NetPlayManager.SendToServer(new C2SUseItemPacket(x, y));
        }

        if (KeyBind.Left.IsKeyDown())
        {
            player.Move(Direction.Left, dTime);
            
        }
        else if (KeyBind.Right.IsKeyDown())
        {
            player.Move(Direction.Right, dTime);

        }
        if (KeyBind.Jump.IsKeyPressed())
        {
            player.Move(Direction.Up, dTime);
        }


    }
    public void GetMosuePos(Camera camera, out int x, out int y)
    {
        Vector2 p = camera.ScreenToWorldSpace(Mouse.GetState().Position);
        x = Utils.Floor(p.X / 8);
        y = Utils.Floor(-p.Y / 8);
    }
    public void SyncHeldItem(AbstractPlayerEntity player)
    {
        int i = player.Inventory.onHand;
        if (i != currentItem) { 
            currentItem = i;
            NetPlayManager.SendToServer(new C2SSyncHeldItemPacket(i));
        }
    }
    public static void PlayerDigging(AbstractWorld world ,AbstractPlayerEntity player, int x, int y)
    {
        //SyncHeldItem();
        world.DestoryTile(x, y);
    }
    public static void PlayerUseItem(AbstractWorld world, AbstractPlayerEntity player, int x, int y)
    {
        //SyncHeldItem();
        var pile = player.GetItemOnHand();
        pile.UseOnTile(world, player, x, y);
    }
}
