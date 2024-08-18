using Galaxias.Client.Main;
using Galaxias.Util;
using LiteNetLib;
using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Core.Networking.Packet.S2C;
public class S2CJoinWorldPacket : S2CPacket
{
    public void Deserialize(NetDataReader reader)
    {
        
    }

    public void Process(Client client)
    {
        client.ProcessJoinWorld(this);
        
    }

    public void Serialize(NetDataWriter writer)
    {
        
    }
}
