using Galaxies.Client.Resource;
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
    public int GetInt(string key) { 
        return ((DataInt)Datas[key]).GetData();
    }
    public void PutFloat(string key, float value)
    {
        Datas.Add(key, new DataFloat(value));
    }
    public float GetFloat(string key)
    {
        return ((DataFloat)Datas[key]).GetData();
    }
}
