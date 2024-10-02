using Galaxies.Core.Networking.Server;
using LiteNetLib;
using LiteNetLib.Utils;
using System;

namespace Galaxies.Core.Networking.Packet.C2S;
public class C2SLoginGamePacket : C2SPacket
{
    public C2SLoginGamePacket() { }
    public override void Process(ServerManager server)
    {
        server.ProcessLoginGame(this);

    }
    public override void Deserialize(NetDataReader reader)
    {

    }
    public override void Serialize(NetDataWriter writer)
    {

    }
}
