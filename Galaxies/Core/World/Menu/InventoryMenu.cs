using Galaxies.Client.Gui.Screen;
using Galaxies.Client.Gui.Screen.Menu;
using Galaxies.Core.World.Entities;
using Galaxies.Core.World.Inventory;
using Galaxies.Core.World.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.World.Menu;
public abstract class InventoryMenu : IEnumerable<Slot>
{
    private readonly List<Slot> slots = [];
    internal ItemPile draggedPile = ItemPile.Empty;

    public InventoryMenu(PlayerInventory inventory)
    {
    }
    public void AddPlayerInventory(PlayerInventory inventory, int startX, int startY, string textureName = "Textures/Gui/slot", string highLightTexture = "Textures/Gui/slot_onHand")
    {
        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                AddSlot(new Slot(inventory, y * 9 + x, x * 20, y * 20, textureName, highLightTexture));
            }
        }
    }
    protected void AddSlot(Slot slot)
    {
        slots.Add(slot);
    }
    public Slot GetSlot(int index)
    {
        return slots[index];
    }

    public IEnumerator<Slot> GetEnumerator()
    {
        return slots.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public abstract AbstractScreen CreateScreen();
}
