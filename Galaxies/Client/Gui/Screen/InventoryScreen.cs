using Galaxies.Client.Render;
using Galaxies.Core.World.Inventory;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Client.Gui.Screen;
public class InventoryScreen : AbstractScreen
{
    private bool isOpen;
    private PlayerInventory playerInventory;
    public InventoryScreen()
    {
        galaxias = Main.GetInstance();
    }
    public override void Render(IntegrationRenderer renderer, double mouseX, double mouseY)
    {
        base.Render(renderer, mouseX, mouseY);
        var i = galaxias.GetPlayer().Inventory;
        int col = isOpen ? 4 : 1;
        for (int y = 0; y < col; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                renderer.Draw("Textures/Gui/slot", Width / 2 - 90 + x * 20, y * 20, Color.White);
                ItemRenderer.RenderInGui(renderer, i.Hotbar[y * 9 + x], Width / 2 - 90 + x * 20 + 10, y * 20 + 10, Color.White);

            }
        }
        if (!isOpen)
        {
            renderer.Draw("Textures/Gui/slot_onHand", Width / 2 - 90 + i.onHand * 20, 0 * 20, Color.White);
        }

    }

    public void Toggle()
    {
        isOpen = !isOpen;
    }

    protected override void OnInit()
    {

    }
}
