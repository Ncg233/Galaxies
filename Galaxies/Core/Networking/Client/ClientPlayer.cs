using Galaxies.Client;
using Galaxies.Core.Networking.Packet.C2S;
using Galaxies.Core.Networking.Packet.S2C;
using Galaxies.Core.World;
using Galaxies.Core.World.Entities;
using System;

namespace Galaxies.Core.Networking.Client;
public class ClientPlayer : AbstractPlayerEntity
{
    public ClientPlayer(AbstractWorld world, Guid id) : base(world, id)
    {
        InteractionManager = new InteractionManager(world, this);
    }
    public override void Update(float dTime)
    {
        base.Update(dTime);
        if (this == Main.GetInstance().GetPlayer() && world.IsClient && existedTime >= nextSyncTime)//sync player pos
        {
            if (lastSyncX != X || lastSyncY != Y)
            {
                nextSyncTime += 1 / 60f;
                NetPlayManager.SendToServer(new C2SPlayerMovePacket(X, Y, vx, vy, direction == Util.Direction.Right));
                lastSyncX = X;
                lastSyncY = Y;
            }
        }
    }

    public override void SendToClient(S2CPacket packet)
    {
    }
}
