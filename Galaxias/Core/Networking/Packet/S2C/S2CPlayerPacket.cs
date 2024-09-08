using Galaxias.Server;
using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Core.Networking.Packet.S2C;
public class S2CPlayerPacket : S2CPacket
{
    public S2CPlayerPacket()
    {

    }
    public S2CPlayerPacket(ServerPlayer player)
    {

    }
    public override void Deserialize(NetDataReader reader)
    {
        throw new NotImplementedException();
    }

    public override void Process(Client client)
    {
        throw new NotImplementedException();
    }

    public override void Serialize(NetDataWriter writer)
    {
        throw new NotImplementedException();
    }
}
