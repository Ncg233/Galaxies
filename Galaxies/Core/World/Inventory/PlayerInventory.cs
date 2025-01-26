using Galaxies.Core.World.Items;
using System;

namespace Galaxies.Core.World.Inventory;
public class PlayerInventory : SimpleInventory
{
    public int onHand = 0;
    public ItemPile holdingItem = null;
    public PlayerInventory() : base(36)
    {
        Set(0, new ItemPile(AllItems.Dirt, 99));
        Set(1, new ItemPile(AllItems.GoldIngot, 99));
        Set(2, new ItemPile(AllItems.Torch, 99));
        Set(3, new ItemPile(AllItems.ChairTile, 99));
        Set(4, new ItemPile(AllItems.Table, 99));
        Set(5, new ItemPile(AllItems.DirtWall, 99));
        Set(6, new ItemPile(AllItems.Door, 99));
        Set(7, new ItemPile(AllItems.Wood, 99));
        Set(8, new ItemPile(AllItems.WoodWall, 99));
        Set(9, new ItemPile(AllItems.Chest, 99));
    }
    public ItemPile GetItemOnHand()
    {
        return GetItemPile(onHand);
    }
}