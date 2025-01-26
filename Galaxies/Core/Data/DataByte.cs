using System.IO;

namespace Galaxies.Core.Data;
public class DataByte(byte data) : BasicDataPart<byte>(data)
{
    public override byte GetId()
    {
        return DataUtils.Byte;
    }
    public override void Write(BinaryWriter writer)
    {
        writer.Write(Data);
    }

    public override void Read(BinaryReader reader)
    {
        Data = reader.ReadByte();
    }
}

