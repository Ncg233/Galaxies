using Galaxies.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.Data.Array;
public class DataByteArray(byte[] data) : BasicDataPart<byte[]>(data)
{
    public override byte GetId()
    {
        return DataUtils.ByteArray;
    }

    public override void Read(BinaryReader reader)
    {
        int size = reader.ReadInt32();
        Data = reader.ReadBytes(size);
    }

    public override void Write(BinaryWriter writer)
    {
        writer.Write(Data.Length);
        writer.Write(Data);
    }
}
