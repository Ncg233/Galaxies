using Galaxies.Core.Networking.Server;
using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.Networking.Packet.C2S;
public class C2SPlayerMovePacket : C2SPacket
{
    public float x;
    public float y;
    public float vx;
    public float vy;
    public bool isRight;

    public C2SPlayerMovePacket(float x, float y, float vx, float vy, bool isRight)
    {
        this.x = x;
        this.y = y;
        this.vx = vx;
        this.vy = vy;
        this.isRight = isRight;
    }
    public C2SPlayerMovePacket()
    {
    }
    public override void Deserialize(NetDataReader reader)
    {
        x = reader.GetFloat();
        y = reader.GetFloat();
        vx = reader.GetFloat();
        vy = reader.GetFloat();
        isRight = reader.GetBool();
        //Console.WriteLine("vx:" + vx + " vy:" + vy);
    }

    public override void Process(ServerManager server)
    {
        server.ProcessPlayerMove(this);
    }

    public override void Serialize(NetDataWriter writer)
    {
        writer.Put(x);
        writer.Put(y);
        writer.Put(vx);
        writer.Put(vy);
        writer.Put(isRight);
    }
}
