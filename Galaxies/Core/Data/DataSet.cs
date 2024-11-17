using Galaxies.Client.Resource;
using Galaxies.Core.Data.Array;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.Data;
public class DataSet
{
    public readonly Dictionary<string, IDataPart> Datas = [];
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
    public T GetData<T>(string key)
    {
        return ((BasicDataPart<T>)Datas[key]).GetData();
    }
}
