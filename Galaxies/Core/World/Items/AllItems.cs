using System.Collections.Generic;
using Galaxies.Core.World.Tiles;

namespace Galaxies.Core.World.Items;
public class AllItems
{
    public static readonly Dictionary<string, Item> itemRegister = new Dictionary<string, Item>();
    public readonly static Item Air = Register("air", new Item());
    public readonly static Item Dirt = FromTile("dirt", AllTiles.Dirt);
    public readonly static Item DirtWall = FromTile("dirt_wall", AllTiles.Dirt, TileLayer.Background);
    public readonly static Item GoldIngot = Register("gold_ingot", new Item());
    public readonly static Item Torch = FromTile("torch", AllTiles.Torch);
    public readonly static Item ChairTile = FromTile("chair_tile", AllTiles.ChairTile);
    public readonly static Item Table = FromTile("table", AllTiles.Table);
    private static Item Register(string name, Item item)
    {
        itemRegister.Add(name, item);
        return item;
    }
    private static Item FromTile(string name, Tile tile, TileLayer layer)
    {
        Item item = Register(name, new TileItem(tile, layer));
        return item;
    }
    private static Item FromTile(string name, Tile tile)
    {
        return FromTile(name, tile, TileLayer.Main);
    }
}