using Galaxies.Core.Data.Array;
using Galaxies.Util;
using System;
using System.IO;
using System.IO.Compression;

namespace Galaxies.Core.Data;
public class DataUtils
{
    public const byte Byte = 0;
    public const byte Short = 1;
    public const byte Int = 2;
    public const byte Long = 3;
    public const byte Float = 4;
    public const byte Double = 5;
    public const byte String = 6;
    public const byte ByteArray = 7;
    public const byte IntArray = 8;
    public const byte LongArray = 9;
    public const byte List = 10;
    public const byte DataSet = 11;
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
            case Int: return new DataInt(0);
            case Float: return new DataFloat(0);
            case ByteArray: return new DataByteArray(null);
            case IntArray: return new DataIntArray(null);
            default: return null;
        }
    }
}
