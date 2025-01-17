using Galaxies.Client;
using Galaxies.Core.World.Tiles;
using LiteNetLib.Utils;
using Microsoft.Xna.Framework;
using System;

namespace Galaxies.Util;
public class Utils
{
    private static readonly TileLayer[] tileLayers = [TileLayer.Background, TileLayer.Main];
    public static readonly Random Random = new Random();
    public static double Rand(double u, double d)
    {
        double u1, u2, z, x;
        if (d <= 0)
        {
            return u;
        }
        u1 = Random.NextDouble();
        u2 = Random.NextDouble();
        z = Math.Sqrt(-2 * Math.Log(u1)) * Math.Sin(2 * Math.PI * u2);
        x = u + d * z;
        return x;
    }
    public static int ToGridPos(int worldPos)
    {
        return Floor(worldPos / (double)GameConstants.ChunkWidth);
    }
    public static int Floor(double value)
    {
        int i = (int)value;
        return value < i ? i - 1 : i;
    }
    public static Color MultiplyNoA(Color value, float scale)
    {
        return new Color((int)(value.R * scale), (int)(value.G * scale), (int)(value.B * scale), value.A);
    }
    public static Color MultiplyNoA(Color value, Color scale)
    {
        return new Color(value.R * scale.R / 255, value.G * scale.G / 255, value.B * scale.B / 255, value.A);
    }
    public static Color Multiply(Color value, Color scale)
    {
        return new Color(value.R * scale.R / 255, value.G * scale.G / 255, value.B * scale.B / 255, value.A * value.B / 255);
    }
    public static int Ceil(double value)
    {
        int i = (int)value;
        return value > i ? i + 1 : i;
    }
    public static TileLayer[] GetAllLayers()
    {
        return tileLayers;
    }

    internal static void Lerp(ref Vector3 from, Vector3 to, float deltaTime)
    {
        //if (from.X - to.X > 192 || from.X - to.X < -192 || from.Y - to.Y > 40 || from.Y - to.Y < -40)
        //    from = to;
        from = new Vector3(from.X + (to.X - from.X) * deltaTime * 10, from.Y + (to.Y - from.Y) * deltaTime * 10, 0);
    }
    public static void PutIntArray(NetDataWriter writer, int[] data)
    {
        writer.Put(data.Length);
        foreach (int item in data)
        {
            writer.Put(item);
        }
    }
    public static void GetIntArray(NetDataReader reader, out int[] data)
    {
        data = new int[reader.GetInt()];
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = reader.GetInt();
        }
    }
    public static void PutByteArray(NetDataWriter writer, byte[] data)
    {
        writer.Put(data.Length);
        foreach (byte item in data)
        {
            writer.Put(item);
        }
    }
    public static void GetByteArray(NetDataReader reader, out byte[] data)
    {
        data = new byte[reader.GetInt()];
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = reader.GetByte();
        }
    }
    public static float NextFloat(float min, float max)
    {
        return Lerp(min, max, (float)Random.NextDouble());
    }
    public static float Lerp(float from, float to, float amount)
    {
        return (1f - amount) * from + amount * to;
    }
}
