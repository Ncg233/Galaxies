using System.Collections.Generic;
using Galaxias.Core.World.Tiles;

namespace Galasias.Core.World.Items;
public class AllItems{
    public static readonly Dictionary<string, Item> itemRegister = new Dictionary<string, Item>();
    public readonly static Item dirt = FromTile("dirt",AllTiles.Dirt);
    public readonly static Item air = Register("air",new Item());
    private static Item Register(string name, Item item)
    {
        itemRegister.Add(name, item);
        return item;
    }
    private static Item FromTile(string name, Tile tile){
        Item item = Register(name,new TileItem(tile));
        return item;
    }
}