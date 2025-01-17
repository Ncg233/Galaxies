using System.Collections.Generic;
using static Galaxies.Core.World.Tiles.Tile;

namespace Galaxies.Core.World.Tiles;
public static class AllTiles
{
    public static readonly Dictionary<string, Tile> tileRegister = [];
    public static readonly Tile Air = Register("air", new AirTile());
    public static readonly Tile Dirt = Register("dirt", new DirtTile(new TileSettings()));
    public static readonly Tile GrassTile = Register("grass_tile", new GrassTile(new TileSettings()));
    public static readonly Tile IronOre = Register("iron_ore", new Tile(new TileSettings()));
    public static readonly Tile Stone = Register("stone", new Tile(new TileSettings()));
    public static readonly Tile Log = Register("log", new LogTile(new TileSettings()));
    public static readonly Tile Leaves = Register("leaves", new LeavesTile(new TileSettings()));
    public static readonly Tile Torch = Register("torch", new TorchTile(new TileSettings()));
    public static readonly Tile Grass = Register("grass", new GrassPlantTile(new TileSettings().SetFullTile(false).SetCanCollide(false)));
    public static readonly Tile ChairTile = Register("chair_tile", new FurnitureTile(1, 4));
    public static readonly Tile Table = Register("table", new FurnitureTile(3, 2));
    public static readonly Tile Door = Register("door", new DoorTile());
    public static readonly Tile Wood = Register("wood", new Tile(new TileSettings()));
    public static readonly Tile Chest = Register("chest", new ChestTile(new TileSettings()));

    public static void Init()
    {
        foreach (var item in tileRegister)
        {
            item.Value.stateHandler.GetAllState().ForEach(TileStateId.Add);  
        }
    }
    private static Tile Register(string name, Tile tile)
    {
        tileRegister.Add(name, tile);
        return tile;
    }
}
