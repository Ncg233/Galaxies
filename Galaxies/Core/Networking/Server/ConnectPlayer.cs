using Galaxies.Core.Networking;
using Galaxies.Core.Networking.Packet.S2C;
using Galaxies.Core.World;
using Galaxies.Core.World.Entities;
using Galaxies.Core.World.Menu;
using Galaxies.Core.World.Tiles.Entity;
using LiteNetLib;
using System;

namespace Galaxies.Core.Networking.Server;
//the connect player only used for server
public class ConnectPlayer : AbstractPlayerEntity
{
    private NetPeer peer;

    public ConnectPlayer(AbstractWorld serverWorld, NetPeer peer, Guid guid) : base(serverWorld, guid)
    {
        this.peer = peer;
        InteractionManager = new InteractionManagerConnection(world, this);
    }

    public override bool OpenInventoryMenu(IMenuProvider entity)
    {
        throw new NotImplementedException();
    }

    public override void SendToClient(S2CPacket packet)
    {
        NetPlayManager.SendToClient(packet, peer);
    }
}