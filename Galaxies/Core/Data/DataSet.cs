using Galaxies.Client.Resource;
using Galaxies.Core.Data.Array;
using Galaxies.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.Data;
public class DataSet : BasicDataPart<DataSet>
{
    public readonly Dictionary<string, IDataPart> Datas = [];

    public DataSet(DataSet data) : base(data)
    {
    }
    public DataSet()
    {
    }
    public void PutByte(string key, byte value)
    {
        Datas.Add(key, new DataByte(value));
    }
    public void PutInt(string key, int value)
    {
        Datas.Add(key, new DataInt(value));
    }
    public void PutFloat(string key, float value)
    {
        Datas.Add(key, new DataFloat(value));
    }
    public void PutByteArray(string key, byte[] value)
    {
        Datas.Add(key, new DataByteArray(value));
    }
    public void PutIntArray(string key, int[] value)
    {
        Datas.Add(key, new DataIntArray(value));
    }
    public void PutList(string key, List<DataSet> value)
    {
        Datas.Add(key, new DataList(value));
    }
    public T GetData<T>(string key)
    {
        return ((BasicDataPart<T>)Datas[key]).GetData();
    }

    public override byte GetId()
    {
        return DataUtils.DataSet;
    }

    public override void Read(BinaryReader reader)
    {
        int size = reader.ReadInt32();
        for (int i = 0; i < size; i++)
        {
            byte partId = reader.ReadByte();
            IDataPart part = DataUtils.GetDataPart(partId);
            string key = reader.ReadString();
            part.Read(reader);
            Datas.Add(key, part);
        }
    }

    public override void Write(BinaryWriter writer)
    {
        writer.Write(Datas.Count);
        foreach (var valuePair in Datas)
        {
            writer.Write(valuePair.Value.GetId());
            writer.Write(valuePair.Key);
            valuePair.Value.Write(writer);
        }
    }
    public override DataSet GetData()
    {
        return this;
    }

    
}
