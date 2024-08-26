using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Core.Networking.Packet.C2S;
public abstract class C2SPacket : IPacket 
{
    public int _id;

    public abstract void Deserialize(NetDataReader reader);
    public abstract void Process(Server server);
    public abstract void Serialize(NetDataWriter writer);
}
