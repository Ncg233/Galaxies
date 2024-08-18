using Galaxias.Core.Networking.Packet.S2C;
using Galaxias.Util;
using LiteNetLib;
using LiteNetLib.Utils;
using System;

namespace Galaxias.Core.Networking.Packet.C2S;
public class C2SLoginGamePacket : C2SPacket
{
    public int _id;
    public C2SLoginGamePacket() { }
    public C2SLoginGamePacket(int remoteId) {
        _id = remoteId;
    }
    public void Process(Server server)
    {
        server.ProcessLoginGame(this);

    }
    public void Deserialize(NetDataReader reader)
    {
        _id = reader.GetInt();
    }
    public void Serialize(NetDataWriter writer)
    {
        writer.Put(_id);
    }
}
