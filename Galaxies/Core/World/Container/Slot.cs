using Galaxies.Client.Gui.Screen;
using Galaxies.Client.Render;
using Galaxies.Core.World.Inventory;
using Galaxies.Core.World.Items;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;


namespace Galaxies.Core.World.Container;
public class Slot
{
    private readonly IInventory inventory;
    private readonly int slotId;
    public readonly int X;
    public readonly int Y;
    public Slot(IInventory inventory, int slotId, int x, int y)
    {
        this.inventory = inventory;
        this.slotId = slotId;
        X = x;
        Y = y;
    }
    
    public ItemPile GetItem()
    {
        return inventory.GetItemPile(slotId);
    }

    public void SetItem(ItemPile pile)
    {
        inventory.Set(slotId, pile);
    }

    public void OnClicked(ItemContainer container, MouseType type)
    {
        ItemPile pile = GetItem();
        if (container.draggedPile.IsEmpty())
        {
            if (!pile.IsEmpty())
            {
                container.draggedPile = pile;
                SetItem(ItemPile.Empty);
            }
        }
        else
        {
            SetItem(container.draggedPile);
            container.draggedPile = ItemPile.Empty;
        }
    }

    //client
}
