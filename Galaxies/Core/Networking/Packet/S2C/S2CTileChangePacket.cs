using Galaxies.Core.World.Tiles;
using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.Networking.Packet.S2C;
public class S2CTileChangePacket : S2CPacket
{
    public int x, y;
    public TileState state;
    public S2CTileChangePacket(int x, int y, TileState state)
    {
        this.x = x;
        this.y = y;
        this.state = state;
    }
    public S2CTileChangePacket()
    {

    }
    public override void Deserialize(NetDataReader reader)
    {
        x = reader.GetInt();
        y = reader.GetInt();
        state = Tile.TileStateId.Get(reader.GetInt());
    }

    public override void Process(Client client)
    {
        client.ProcessTileChange(this);
    }

    public override void Serialize(NetDataWriter writer)
    {
        writer.Put(x);
        writer.Put(y);
        writer.Put(Tile.TileStateId.Get(state));
    }
}
