using System;

namespace Galaxies.Core.World.Tiles;
public class GameConstants
{
    public static readonly int TileSize = 8;
    public static readonly int ChunkWidth = 32;
    public static readonly int ChunkHeight = 256;
    public static readonly byte MaxLight = byte.MaxValue;
    internal static readonly float Gravity = 2;
}