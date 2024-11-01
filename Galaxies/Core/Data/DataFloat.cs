using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.Data;
public class DataFloat : IDataPart
{

    private float _data;
    public DataFloat(float data)
    {
        _data = data;
    }
    public DataFloat()
    {
    }
    public byte GetId()
    {
        return 2;
    }
    public float GetData()
    {
        return _data;
    }
    public void Write(BinaryWriter writer)
    {
        writer.Write(_data);
    }
    public void Read(BinaryReader reader)
    {
        _data = reader.ReadSingle();
    }
}
