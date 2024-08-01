using System.Collections.Generic;

namespace Galaxias.Core.World.Tiles;
public class AllTiles
{
    public static readonly Dictionary<string, Tile> tileRegister = new Dictionary<string, Tile>();
    public static readonly Tile Air = Register("air", new AirTile());
    public static readonly Tile Dirt = Register("dirt", new Tile());
    public static readonly Tile GrassTile = Register("grass_tile", new Tile());
    public static readonly Tile IronOre = Register("iron_ore", new Tile());
    public static readonly Tile Stone = Register("stone", new Tile());
    public static readonly Tile Log = Register("log", new LogTile());
    public static readonly Tile Leaves = Register("leaves", new LeavesTile());

    private static Tile Register(string name, Tile tile)
    {
        tileRegister.Add(name, tile);
        return tile;
    }
}
