using System.Collections.Generic;
using Galaxies.Core.World.Tiles;
using Galaxies.Core.World.Tiles.State;
using Galaxies.Util;

namespace Galaxies.Core.World.Items;
public class AllItems
{

    public static readonly Dictionary<string, Item> itemRegister = new Dictionary<string, Item>();
    public static readonly IntIdentityDictionary<Item> ItemId = [];
    public readonly static Item Air = Register("air", new Item());
    public readonly static Item Dirt = FromTile("dirt", AllTiles.Dirt);
    public readonly static Item DirtWall = FromTile("dirt_wall", AllTiles.Dirt, TileLayer.Background);
    public readonly static Item GoldIngot = Register("gold_ingot", new Item());
    public readonly static Item Torch = FromTile("torch", AllTiles.Torch);
    public readonly static Item ChairTile = FromTile("chair_tile", AllTiles.ChairTile);
    public readonly static Item Table = FromTile("table", AllTiles.Table);
    public readonly static Item Door = FromTile("door", AllTiles.Door);
    public readonly static Item Wood = FromTile("wood", AllTiles.Wood);
    public readonly static Item WoodWall = FromTile("wood_wall", AllTiles.Wood, TileLayer.Background);
    public readonly static Item Chest = FromTile("chest", AllTiles.Chest);
    public readonly static Item StonePickaxe = Register("stone_pickaxe", new Item(new Item.Properties().SetDiggingTool()));
    private static Item Register(string name, Item item)
    {
        itemRegister.Add(name, item);
        ItemId.Add(item);
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