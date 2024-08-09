using Galaxias.Core.World.Tiles;
using Microsoft.Xna.Framework;

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
        return new Color((int)(value.R * scale), (int)(value.G * scale), (int)(value.B * scale), 255);
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
}
