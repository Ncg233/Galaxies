using Galaxies.Client.Gui.Widget;
using Galaxies.Client.Render;
using Galaxies.Core.World.Entities;
using Galaxies.Core.World.Items;
using Galaxies.Core.World.Menu;
using Galaxies.Util;
using Microsoft.Xna.Framework;

namespace Galaxies.Client.Gui.Screen.Menu;
public class InventoryScreen : MenuScreen<PlayerInventoryMenu>
{
    private AbstractPlayerEntity playerEntity;
    public InventoryScreen(AbstractPlayerEntity player) : base(player.container)
    {
        playerEntity = player;
        xSize = 180;

    }

    public override bool MouseClicked(double mouseX, double mouseY, MouseType pressedKey)
    {
        if (base.MouseClicked(mouseX, mouseY, pressedKey))
        {
            return true;
        }

        return false;//todo

    }
    
}
