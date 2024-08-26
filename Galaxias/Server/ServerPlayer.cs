using Galaxias.Core.Networking;
using Galaxias.Core.Networking.Packet;
using Galaxias.Core.World;
using Galaxias.Core.World.Entities;
using LiteNetLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Server;
public class ServerPlayer : Player
{
    private readonly NetPeer _connetionClient;
    public readonly InteractionManagerServer interactionManager; 

    public ServerPlayer(ServerWorld world, NetPeer peer) : base(world)
    {
        _connetionClient = peer;
        interactionManager = new InteractionManagerServer(world);
    }
    public void SendPacket(IPacket packet)
    {
        NetPlayManager.SendToClient(packet, _connetionClient);
    }
}
