using Galaxias.Core.World.Tiles;
using Microsoft.Xna.Framework;
using System;

namespace Galaxias.Util;
public class Utils
{
    private static readonly TileLayer[] tileLayers = [TileLayer.Main, TileLayer.Background];
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
    public static int Ceil(double value)
    {
        int i = (int)value;
        return value > (double)i ? i + 1 : i;
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
}
