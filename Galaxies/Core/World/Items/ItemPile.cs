using Galaxies.Core.World.Entities;
using System;

namespace Galaxies.Core.World.Items;
public class ItemPile
{
    private Item item;
    private int count = 0;
    public static ItemPile Empty = new ItemPile(AllItems.Air);

    public ItemPile(Item item, int count)
    {
        this.item = item;
        this.count = count;
    }
    public ItemPile(Item item)
    {
        this.item = item;
        count = 1;
    }
    public bool isEmpty()
    {
        return item == null || item == AllItems.Air || count == 0;
    }
    public Item GetItem()
    {
        return item;
    }
    public int GetCount()
    {
        return count;
    }
    public void SetCount(int count)
    {
        this.count = count;
    }

    public bool UseOnTile(AbstractWorld world, AbstractPlayerEntity player, int x, int y)
    {
        return item.Use(world, player, x, y);
    }
}