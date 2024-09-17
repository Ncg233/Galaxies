using Galaxias.Core.World;
using Galaxias.Util;
using LiteNetLib.Utils;

namespace Galaxias.Core.Networking.Packet.S2C;
public class S2CWorldDataPacket : S2CPacket
{
    public int[] tileData;
    public byte[] skyLight;
    public byte[] tileLight;
    public S2CWorldDataPacket()
    {

    }
    public S2CWorldDataPacket(AbstractWorld world)
    {
        world.WriteTileData(out tileData, out skyLight, out tileLight); 
    }
    public override void Deserialize(NetDataReader reader)
    {
        Utils.GetIntArray(reader, out tileData);
        Utils.GetByteArray(reader, out skyLight);
        Utils.GetByteArray(reader, out tileLight);
    }

    public override void Process(Client client)
    {
        client.ProcessWorldData(this);
    }

    public override void Serialize(NetDataWriter writer)
    {
        Utils.PutIntArray(writer, tileData);
        Utils.PutByteArray(writer, skyLight);
        Utils.PutByteArray(writer, tileLight);
    }
}
