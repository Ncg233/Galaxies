using System.Collections.Generic;
using Galaxies.Core.World.Tiles;

namespace Galaxies.Core.World.Items;
public class AllItems
{
    public static readonly Dictionary<string, Item> itemRegister = new Dictionary<string, Item>();
    public readonly static Item Air = Register("air", new Item());
    public readonly static Item Dirt = FromTile("dirt", AllTiles.Dirt);
    public readonly static Item GoldIngot = Register("gold_ingot", new Item());
    public readonly static Item Torch = FromTile("torch", AllTiles.Torch);
    private static Item Register(string name, Item item)
    {
        itemRegister.Add(name, item);
        return item;
    }
    private static Item FromTile(string name, Tile tile)
    {
        Item item = Register(name, new TileItem(tile));
        return item;
    }
}