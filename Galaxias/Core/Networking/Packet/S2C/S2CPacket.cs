using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Core.Networking.Packet.S2C;
public abstract class S2CPacket : IPacket
{
    public abstract void Deserialize(NetDataReader reader);
    public abstract void Process(Client client);
    public abstract void Serialize(NetDataWriter writer);
}

