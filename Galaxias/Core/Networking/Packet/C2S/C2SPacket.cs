using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Core.Networking.Packet.C2S;
internal interface C2SPacket : IPacket 
{
    public void Process(Server server);
}
