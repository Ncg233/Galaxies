using Galaxias.Core.Networking.Server;
using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Core.Networking.Packet.C2S;
public class C2SUseItemPacket : C2SPacket
{
    public int X, Y;
    public C2SUseItemPacket(int x, int y)
    {
        X = x; Y = y; 
    }
    public C2SUseItemPacket()
    {
    }
    public override void Deserialize(NetDataReader reader)
    {
        
    }

    public override void Process(ServerManager server)
    {
        server.ProcessUseItem(this);
    }

    public override void Serialize(NetDataWriter writer)
    {
        
    }
}
