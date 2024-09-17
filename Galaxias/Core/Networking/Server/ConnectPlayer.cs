using Galaxias.Core.Networking.Packet.S2C;
using Galaxias.Core.World;
using Galaxias.Core.World.Entities;
using LiteNetLib;

namespace Galaxias.Core.Networking.Server;
//the connect player only used for server
public class ConnectPlayer : AbstractPlayerEntity
{
    private NetPeer peer;

    public ConnectPlayer(AbstractWorld serverWorld, NetPeer peer) : base(serverWorld)
    {
        this.peer = peer;
    }

    public override void SendToClient(S2CPacket packet)
    {
        NetPlayManager.SendToClient(packet, peer);
    }
}