using Galaxies.Client.Gui.Screen;
using Galaxies.Client.Render;
using Galaxies.Core.World.Inventory;
using Galaxies.Core.World.Items;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;


namespace Galaxies.Core.World.Menu;
public class Slot
{
    private readonly IInventory inventory;
    private readonly int slotId;
    public readonly int X;
    public readonly int Y;
    public readonly string TextureName;
    public readonly string HighLightTexture;
    public Slot(IInventory inventory, int slotId, int x, int y, string textureName = "Textures/Gui/slot", string highLightTexture = "Textures/Gui/slot_onHand")
    {
        this.inventory = inventory;
        this.slotId = slotId;
        X = x;
        Y = y;
        TextureName = textureName;
        HighLightTexture = highLightTexture;
    }
    public Slot(IInventoryProvider invProvider, int slotId, int x, int y, string textureName = "Textures/Gui/slot", string highLightTexture = "Textures/Gui/slot_onHand")
    {
        inventory = invProvider.GetInventory();
        this.slotId = slotId;
        X = x;
        Y = y;
        TextureName = textureName;
        HighLightTexture = highLightTexture;
    }

    public ItemPile GetItem()
    {
        return inventory.GetItemPile(slotId);
    }

    public void SetItem(ItemPile pile)
    {
        inventory.Set(slotId, pile);
    }

    public void OnClicked(InventoryMenu container, MouseType type)
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
