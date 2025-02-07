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
            set.Write(writer);
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
            set.Read(reader);
            reader.Close();
        }
        catch (Exception e)
        {
            Log.Error("Can't read data set", e);
        }
    }
    public static IDataPart GetDataPart(int partId)
    {
        switch (partId) { 
            case Byte: return new DataByte(0);
            case Int: return new DataInt(0);
            case Float: return new DataFloat(0);
            case ByteArray: return new DataByteArray(null);
            case IntArray: return new DataIntArray(null);
            case DataSet: return new DataSet();
            case List: return new DataList();
            default: return null;
        }
    }
}
