using Galaxies.Core.World.Items;
using System;

namespace Galaxies.Core.World.Inventory;
public class PlayerInventory : IInventory
{
    public ItemPile[] Hotbar = new ItemPile[36];
    public int onHand = 0;
    public ItemPile holdingItem = null;
    public PlayerInventory()
    {
        Array.Fill(Hotbar, ItemPile.Empty);
        Hotbar[0] = new ItemPile(AllItems.Dirt, 99);
        Hotbar[1] = new ItemPile(AllItems.GoldIngot, 99);
        Hotbar[2] = new ItemPile(AllItems.Torch, 99);
        Hotbar[3] = new ItemPile(AllItems.ChairTile, 99);
        Hotbar[4] = new ItemPile(AllItems.Table, 99);
        Hotbar[5] = new ItemPile(AllItems.DirtWall, 99);
        Hotbar[6] = new ItemPile(AllItems.Door, 99);
        Hotbar[7] = new ItemPile(AllItems.Wood, 99);
        Hotbar[8] = new ItemPile(AllItems.WoodWall, 99);
    }
    public ItemPile GetItemPile(int index)
    {
        return Hotbar[index];
    }
    public int GetCount()
    {
        return Hotbar.Length;
    }
}