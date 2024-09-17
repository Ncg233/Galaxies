using Galaxias.Client;
using Galaxias.Core.Networking.Packet.C2S;
using Galaxias.Core.Networking.Packet.S2C;
using Galaxias.Core.World;
using Galaxias.Core.World.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Core.Networking;
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
                nextSyncTime += 0.04f;
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
