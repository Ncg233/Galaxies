using System.Collections.Generic;
using static Galaxias.Core.World.Tiles.Tile;

namespace Galaxias.Core.World.Tiles;
public class AllTiles
{
    public static readonly Dictionary<string, Tile> tileRegister = new Dictionary<string, Tile>();
    public static readonly Tile Air = Register("air", new AirTile());
    public static readonly Tile Dirt = Register("dirt", new Tile(new TileSettings()));
    public static readonly Tile GrassTile = Register("grass_tile", new Tile(new TileSettings()));
    public static readonly Tile IronOre = Register("iron_ore", new Tile(new TileSettings()));
    public static readonly Tile Stone = Register("stone", new Tile(new TileSettings()));
    public static readonly Tile Log = Register("log", new LogTile(new TileSettings()));
    public static readonly Tile Leaves = Register("leaves", new LeavesTile(new TileSettings()));
    public static readonly Tile Torch = Register("torch", new TorchTile(new TileSettings()));

    private static Tile Register(string name, Tile tile)
    {
        tileRegister.Add(name, tile);
        return tile;
    }
}
