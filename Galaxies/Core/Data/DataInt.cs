using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.Data;
public class DataInt(int data) : BasicDataPart<int>(data)
{
    public override byte GetId()
    {
        return DataUtils.Int;
    }
    public override void Write(BinaryWriter writer)
    {
        writer.Write(Data);
    }

    public override void Read(BinaryReader reader)
    {
        Data = reader.ReadInt32();
    }
}
