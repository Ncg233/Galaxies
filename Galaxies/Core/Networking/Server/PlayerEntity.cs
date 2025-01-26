using Galaxies.Client;
using Galaxies.Client.Key;
using Galaxies.Core.Networking.Packet.S2C;
using Galaxies.Core.World;
using Galaxies.Core.World.Entities;
using Galaxies.Core.World.Menu;
using Galaxies.Core.World.Tiles.Entity;
using Microsoft.Xna.Framework.Input;
using System;
namespace Galaxies.Core.Networking.Server;
//single player
public class PlayerEntity : AbstractPlayerEntity
{
    private int lastOffset = 0;
    public PlayerEntity(AbstractWorld world, Guid id) : base(world, id)
    {
        InteractionManager = new InteractionManager(world, this);
    }
    public override void Update(float dTime)
    {
        base.Update(dTime);
        if (KeyBind.D1.IsKeyDown()) GetInventory().onHand = 0;
        if (KeyBind.D2.IsKeyDown()) GetInventory().onHand = 1;
        if (KeyBind.D3.IsKeyDown()) GetInventory().onHand = 2;
        if (KeyBind.D4.IsKeyDown()) GetInventory().onHand = 3;
        if (KeyBind.D5.IsKeyDown()) GetInventory().onHand = 4;
        if (KeyBind.D6.IsKeyDown()) GetInventory().onHand = 5;
        if (KeyBind.D7.IsKeyDown()) GetInventory().onHand = 6;
        if (KeyBind.D8.IsKeyDown()) GetInventory().onHand = 7;
        if (KeyBind.D9.IsKeyDown()) GetInventory().onHand = 8;
        //Log.Info((Mouse.GetState().ScrollWheelValue % 9).ToString());

        int Offset = Mouse.GetState().ScrollWheelValue;
        if (lastOffset != Offset)
        {
            var inv = GetInventory();
            var a = lastOffset - Offset;
            lastOffset = Offset;
            inv.onHand = inv.onHand + (a > 0 ? 1 : -1);
            if (inv.onHand > 8)
            {
                inv.onHand -= 9;
            }
            else if (inv.onHand < 0) { inv.onHand += 9; }

        }
    }
    public override void SendToClient(S2CPacket packet)
    {

    }

    public override bool OpenInventoryMenu(IMenuProvider provider)
    {
        var menu = provider.CreateMenu(Inventory);

        currentInvMenu = menu;

        Main.GetInstance().SetCurrentScreen(currentInvMenu.CreateScreen());

        return true;
    }
}
