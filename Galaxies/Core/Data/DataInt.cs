using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.Data;
public class DataInt : IDataPart
{
    private int _data;
    public DataInt(int data)
    {
        _data = data;
    }
    public DataInt()
    {
    }
    public byte GetId()
    {
        return 1;
    }
    public int GetData() { 
        return _data;
    }
    public void Write(BinaryWriter writer)
    {
        writer.Write(_data);
    }

    public void Read(BinaryReader reader)
    {
        _data = reader.ReadInt32();
    }
}
