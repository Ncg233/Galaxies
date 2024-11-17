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

    //client
}
