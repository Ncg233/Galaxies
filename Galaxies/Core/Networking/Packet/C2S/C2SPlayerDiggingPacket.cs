using Galaxies.Core.Networking.Server;
using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.Networking.Packet.C2S;
public class C2SPlayerDiggingPacket : C2SPacket
{
    public Action DiggingAction { get; private set; }
    public int x { get; private set; }
    public int y { get; private set; }
    public C2SPlayerDiggingPacket()
    {
    }
    public C2SPlayerDiggingPacket(Action action, int x, int y)
    {
        DiggingAction = action;
        this.x = x;
        this.y = y;
    }
    public override void Deserialize(NetDataReader reader)
    {
        x = reader.GetInt();
        y = reader.GetInt();
    }

    public override void Process(ServerManager server)
    {
        server.ProcessDigging(this);
    }

    public override void Serialize(NetDataWriter writer)
    {
        writer.Put(x);
        writer.Put(y);
    }
    public enum Action
    {
        CreativeBreak = 0
    }
}
