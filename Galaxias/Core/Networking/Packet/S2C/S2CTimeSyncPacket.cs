using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Core.Networking.Packet.S2C;
public class S2CTimeSyncPacket : S2CPacket
{
    public float CurrentTime;
    public S2CTimeSyncPacket()
    {

    }
    public S2CTimeSyncPacket(float currentTime)
    {
        CurrentTime = currentTime;
    }

    public override void Deserialize(NetDataReader reader)
    {
        CurrentTime = reader.GetFloat();
    }

    public override void Process(Client client)
    {
        client.ProcessTimeSync(this);
    }

    public override void Serialize(NetDataWriter writer)
    {
        writer.Put(CurrentTime);
    }
}
