using LiteNetLib;
using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.Networking.Packet.S2C;
public class S2CJoinWorldPacket : S2CPacket
{
    public override void Deserialize(NetDataReader reader)
    {

    }

    public override void Process(Client client)
    {
        client.ProcessJoinWorld(this);

    }

    public override void Serialize(NetDataWriter writer)
    {

    }
}
