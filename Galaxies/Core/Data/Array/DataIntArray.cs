using Galaxies.Util;
using SharpDX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.Data.Array;
public class DataIntArray(int[] data) : BasicDataPart<int[]>(data)
{
    public override byte GetId()
    {
        return DataUtils.IntArray;
    }

    public override void Read(BinaryReader reader)
    {
        byte[] bytes = new byte[reader.ReadInt32()];
        reader.Read(bytes);
        Data = new int[bytes.Length / sizeof(int)];
        Buffer.BlockCopy(bytes, 0, Data, 0, bytes.Length);
    }

    public override void Write(BinaryWriter writer)
    {

        byte[] result = new byte[Data.Length * sizeof(int)];
        Buffer.BlockCopy(Data, 0, result, 0, result.Length);
        writer.Write(result.Length);
        writer.Write(result);
    }
}
