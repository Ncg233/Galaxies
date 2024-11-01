using Galaxies.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.Data;
public class DataUtils
{
    public static void WriteDataSet(DataSet set, string writeFile)
    {
        try
        {
            BinaryWriter writer = new BinaryWriter(new GZipStream(new FileStream(writeFile, FileMode.OpenOrCreate, FileAccess.Write), CompressionMode.Compress));
            //write data set size
            writer.Write(set.Datas.Count);
            foreach (var valuePair in set.Datas)
            {
                writer.Write(valuePair.Value.GetId());
                writer.Write(valuePair.Key);
                valuePair.Value.Write(writer);
            }
            writer.Close();
        }
        catch (Exception e) { 
            Log.Error("Can't write data set", e);
        }
    }
    public static void ReadDataSet(out DataSet set, string readFile)
    {
        set = new DataSet();
        try
        {
            BinaryReader reader = new BinaryReader(new GZipStream(new FileStream(readFile, FileMode.OpenOrCreate, FileAccess.Read), CompressionMode.Decompress));
            int size = reader.ReadInt32();
            for (int i = 0; i < size; i++) { 
                byte partId = reader.ReadByte();
                IDataPart part = GetDataPart(partId);
                string key = reader.ReadString();
                part.Read(reader);
                set.Datas.Add(key, part);
            }
            reader.Close();
        }
        catch (Exception e)
        {
            Log.Error("Can't write data set", e);
        }
    }
    private static IDataPart GetDataPart(int partId)
    {
        switch (partId) { 
            case 0: return null;
            case 1: return new DataInt();
            case 2: return new DataFloat();
            default: return null;
        }
    }
}
