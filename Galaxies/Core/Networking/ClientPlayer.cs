using Galaxies.Client;
using Galaxies.Core.Networking.Packet.C2S;
using Galaxies.Core.Networking.Packet.S2C;
using Galaxies.Core.World;
using Galaxies.Core.World.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.Networking;
public class ClientPlayer : AbstractPlayerEntity
{
    public ClientPlayer(AbstractWorld world) : base(world)
    {

    }
    public override void Update(float dTime)
    {
        base.Update(dTime);
        if (this == Main.GetInstance().GetPlayer() && world.IsClient && existedTime >= nextSyncTime)//sync player pos
        {
            if (lastSyncX != x || lastSyncY != y)
            {
                nextSyncTime += 1 / 60f;
                NetPlayManager.SendToServer(new C2SPlayerMovePacket(x, y, vx, vy, direction == Util.Direction.Right));
                lastSyncX = x;
                lastSyncY = y;
            }
        }
    }

    public override void SendToClient(S2CPacket packet)
    {
    }
}
