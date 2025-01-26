using Galaxies.Util;
using SharpDX.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.Data.Array;
public class DataList : BasicDataPart<List<DataSet>>
{
    public DataList()
    {
    }
    public DataList(List<DataSet> data) { 
        Data = data;
    }
    public override byte GetId()
    {
        return DataUtils.List;
    }

    public override void Read(BinaryReader reader)
    {
        
        int count = reader.ReadInt32();
        Data = [];
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                DataSet set = new();
                set.Read(reader);
                Data.Add(set);
            }
        }
    }

    public override void Write(BinaryWriter writer)
    {
        writer.Write(Data.Count);

        if (Data.Count > 0)
        {
            for (int i = 0; i < Data.Count; i++)
            {
                DataSet part = Data[i];
                part.Write(writer);
            }
        }
        
    }
}
