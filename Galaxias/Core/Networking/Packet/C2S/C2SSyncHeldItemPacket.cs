using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Core.Networking.Packet.C2S;
public class C2SSyncHeldItemPacket : C2SPacket
{
    public int CurrentItem;
    public C2SSyncHeldItemPacket(int item)
    {
        CurrentItem = item; 
    }
    public C2SSyncHeldItemPacket()
    {
    }
    public override void Deserialize(NetDataReader reader)
    {
        throw new NotImplementedException();
    }

    public override void Process(Server server)
    {
        server.ProcessSyncHeldItem(this);
    }

    public override void Serialize(NetDataWriter writer)
    {
        throw new NotImplementedException();
    }
}

