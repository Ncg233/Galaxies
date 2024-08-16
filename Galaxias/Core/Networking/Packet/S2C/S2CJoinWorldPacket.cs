using Galaxias.Client.Main;
using LiteNetLib;
using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Core.Networking.Packet.S2C;
public class S2CJoinWorldPacket : IPacket
{
    public void Deserialize(NetDataReader reader)
    {
        
    }

    public void Process(NetPeer sender)
    {
        GalaxiasClient.GetInstance().JoinWorld();
    }

    public void Serialize(NetDataWriter writer)
    {
        
    }
}
