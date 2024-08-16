using Galaxias.Core.Networking.Packet.S2C;
using LiteNetLib;
using LiteNetLib.Utils;
using System;

namespace Galaxias.Core.Networking.Packet.C2S;
public class C2SLoginGamePacket : IPacket
{
    public C2SLoginGamePacket() { }
    public void Process(NetPeer sender)
    {
        Console.WriteLine("Join Game!");
        NetPlayManager.Instance.SendToClient(new S2CJoinWorldPacket(), sender);

    }
    public void Deserialize(NetDataReader reader)
    {

    }
    public void Serialize(NetDataWriter writer)
    {
        
    }
}
