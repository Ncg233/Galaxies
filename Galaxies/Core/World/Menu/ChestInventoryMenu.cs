using Galaxies.Client.Gui.Screen;
using Galaxies.Client.Gui.Screen.Menu;
using Galaxies.Core.World.Entities;
using Galaxies.Core.World.Inventory;
using Galaxies.Core.World.Tiles.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Galaxies.Core.World.Menu;
public class ChestInventoryMenu : InventoryMenu
{
    private readonly ChestTileEntity tileEntity;
    public ChestInventoryMenu(PlayerInventory inventory, ChestTileEntity entity) : base(inventory)
    {
        tileEntity = entity;
        AddPlayerInventory(inventory, 0, 0);

        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                AddSlot(new Slot(entity,y * 9 + x, x * 20, 90 + y * 20, "Textures/Gui/chest_slot",  "Textures/Gui/chest_slot_highlight"));
            }
        }
    }

    

    public override AbstractScreen CreateScreen()
    {
        return new ChestMenuScreen(this);
    }
}