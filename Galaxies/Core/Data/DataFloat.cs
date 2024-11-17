using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.Data;
public class DataFloat(float data) : BasicDataPart<float>(data)
{
    public override byte GetId()
    {
        return DataUtils.Float;
    }
    public override void Write(BinaryWriter writer)
    {
        writer.Write(Data);
    }

    public override void Read(BinaryReader reader)
    {
        Data = reader.ReadSingle();
    }
}
