using Galaxies.Client.Gui.Screen;
using Galaxies.Core.World.Entities;
using Galaxies.Core.World.Inventory;

namespace Galaxies.Core.World.Menu;
public class PlayerInventoryMenu : InventoryMenu
{
    public PlayerInventoryMenu(PlayerInventory inventory) : base(inventory)
    {
        AddPlayerInventory(inventory , 0, 0);
    }

    public override AbstractScreen CreateScreen()
    {
        return null;
    }
}
