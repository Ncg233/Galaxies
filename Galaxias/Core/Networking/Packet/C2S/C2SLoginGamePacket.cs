using Galaxias.Core.Networking.Packet.S2C;
using Galaxias.Core.Networking.Server;
using Galaxias.Util;
using LiteNetLib;
using LiteNetLib.Utils;
using System;

namespace Galaxias.Core.Networking.Packet.C2S;
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
