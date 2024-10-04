using System.Collections.Generic;
using static Galaxies.Core.World.Tiles.Tile;

namespace Galaxies.Core.World.Tiles;
public static class AllTiles
{
    public static readonly Dictionary<string, Tile> tileRegister = new Dictionary<string, Tile>();
    public static readonly Tile Air = Register("air", new AirTile());
    public static readonly Tile Dirt = Register("dirt", new DirtTile(new TileSettings()));
    public static readonly Tile GrassTile = Register("grass_tile", new Tile(new TileSettings()));
    public static readonly Tile IronOre = Register("iron_ore", new Tile(new TileSettings()));
    public static readonly Tile Stone = Register("stone", new Tile(new TileSettings()));
    public static readonly Tile Log = Register("log", new LogTile(new TileSettings()));
    public static readonly Tile Leaves = Register("leaves", new LeavesTile(new TileSettings()));
    public static readonly Tile Torch = Register("torch", new TorchTile(new TileSettings()));
    public static readonly Tile Grass = Register("grass", new GrassPlantTile(new TileSettings().SetFullTile(false).SetCanCollide(false)));

    public static void Init()
    {
        foreach (var item in tileRegister)
        {
            TileStateId.Add(item.Value.GetDefaultState());
        }
    }
    private static Tile Register(string name, Tile tile)
    {
        tileRegister.Add(name, tile);
        return tile;
    }
}
