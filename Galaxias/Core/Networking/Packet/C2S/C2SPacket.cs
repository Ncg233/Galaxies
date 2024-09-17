using Galaxias.Core.Networking.Server;
using LiteNetLib.Utils;

namespace Galaxias.Core.Networking.Packet.C2S;
public abstract class C2SPacket : IPacket 
{
    public int _id;

    public abstract void Deserialize(NetDataReader reader);
    public abstract void Process(ServerManager server);
    public abstract void Serialize(NetDataWriter writer);
}
