using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Core.Networking.Packet.S2C;
public interface S2CPacket : IPacket
{
    public void Process(Client client);
}

