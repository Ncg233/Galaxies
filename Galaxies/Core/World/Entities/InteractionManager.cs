using Galaxies.Client;
using Galaxies.Client.Key;
using Galaxies.Client.Render;
using Galaxies.Core.Networking;
using Galaxies.Core.Networking.Packet.C2S;
using Galaxies.Core.World;
using Galaxies.Core.World.Entities.Monsters;
using Galaxies.Core.World.Tiles;
using Galaxies.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Galaxies.Core.World.Entities;
public class InteractionManager
{
    private bool buttonRealased;
    private AbstractWorld world;
    AbstractPlayerEntity player;
    private int currentItem;
    public InteractionManager(AbstractWorld world, AbstractPlayerEntity player)
    {
        this.world = world;
        this.player = player;
    }

    public virtual void Update(float dTime)
    {
        var state = Mouse.GetState();
        if (state.LeftButton == ButtonState.Pressed)
        {

            Main.GetMosueTilePos(out int x, out int y);
            var tileState = world.GetTileState(TileLayer.Main, x, y);
            var item = player.GetItemOnHand();
            if (item.IsDiggingTool())
            {
                if (!tileState.IsAir())
                {
                    PlayerDigging(world, x, y);
                }
            }else
            {
                PlayerUseItem(world, x, y);
            }

        }
        else if (state.RightButton == ButtonState.Pressed)
        {
            Main.GetMosueTilePos(out int x, out int y);
            //SyncHeldItem();
            if (buttonRealased)
            {
                var tileState = world.GetTileState(TileLayer.Main, x, y);
                tileState.OnUse(world,player, x, y);
            }
            //PlayerUseItem(world, x, y);
            buttonRealased = false;

        }
        buttonRealased = state.RightButton == ButtonState.Released;

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

        float px = player.X;
        float py = player.Y + 2;
        Main.GetMosueWorldPos(out float mx, out float my);
        int angle = (int)(MathHelper.ToDegrees((float)Math.Atan2(mx - px,my - py)) + 180);
        player.SetCurrentAngle(angle);
    }
    public void SyncHeldItem()
    {
        if (world.IsClient)
        {
            int i = player.Inventory.onHand;
            if (i != currentItem)
            {
                currentItem = i;
                NetPlayManager.SendToServer(new C2SSyncHeldItemPacket(i));
            }
        }
    }
    public void PlayerDigging(AbstractWorld world, int x, int y)
    {
        SyncHeldItem();
        world.DestoryTile(x, y);
        if (world.IsClient)
        {
            NetPlayManager.SendToServer(new C2SPlayerDiggingPacket(C2SPlayerDiggingPacket.Action.CreativeBreak, x, y));
        }

    }
    public void PlayerUseItem(AbstractWorld world, int x, int y)
    {
        SyncHeldItem();
        var pile = player.GetItemOnHand();
        if (pile != null && !pile.IsEmpty())
        {
            if (pile.Use(world, player, x, y))
            {
                pile.SetCount(pile.GetCount() - 1);
            }
        }

        if (world.IsClient)
        {
            NetPlayManager.SendToServer(new C2SUseItemPacket(x, y));
        }
    }
}
