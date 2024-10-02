using Galaxies.Client.Key;
using Galaxies.Core.Networking.Packet.S2C;
using Galaxies.Core.World;
using Galaxies.Core.World.Entities;
using Microsoft.Xna.Framework.Input;
namespace Galaxies.Client;
public class PlayerEntity : AbstractPlayerEntity
{
    public PlayerEntity(AbstractWorld world) : base(world)
    {

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

        int Offset = -Mouse.GetState().ScrollWheelValue / 12;
        while (Offset > 8)
            Offset -= 9;
        while (Offset < 0)
            Offset += 9;
        GetInventory().onHand = Offset;
    }
    public override void SendToClient(S2CPacket packet)
    {

    }
}
