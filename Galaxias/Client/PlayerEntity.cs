using Galaxias.Client.Key;
using Galaxias.Core.Networking.Packet.S2C;
using Galaxias.Core.World;
using Galaxias.Core.World.Entities;
using Galaxias.Util;
namespace Galaxias.Client;
public class PlayerEntity : AbstractPlayerEntity
{
    public PlayerEntity(AbstractWorld world) : base(world)
    {
    }

    protected override void HandleMovement(float dTime)
    {
        if (KeyBind.D1.IsKeyDown()) GetInventory().onHand = 0;
        if (KeyBind.D2.IsKeyDown()) GetInventory().onHand = 1;
        if (KeyBind.D3.IsKeyDown()) GetInventory().onHand = 2;
        if (KeyBind.D4.IsKeyDown()) GetInventory().onHand = 3;
        if (KeyBind.D5.IsKeyDown()) GetInventory().onHand = 4;
        if (KeyBind.D6.IsKeyDown()) GetInventory().onHand = 5;
        if (KeyBind.D7.IsKeyDown()) GetInventory().onHand = 6;
        if (KeyBind.D8.IsKeyDown()) GetInventory().onHand = 7;
        if (KeyBind.D9.IsKeyDown()) GetInventory().onHand = 8;
    }
    public override void SendToClient(S2CPacket packet)
    {

    }
}
