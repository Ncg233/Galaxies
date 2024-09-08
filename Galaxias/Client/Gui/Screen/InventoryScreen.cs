using Galaxias.Client.Main;
using Galaxias.Client.Render;
using Galaxias.Core.Networking;
using Galaxias.Core.World.Inventory;
using Galaxias.Core.World.Items;
using Microsoft.Xna.Framework;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Client.Gui.Screen;
public class InventoryScreen : AbstractScreen
{
    private bool isOpen;
    private PlayerInventory playerInventory;
    public InventoryScreen()
    {
        galaxias = GalaxiasClient.GetInstance();
    }
    public override void Render(IntegrationRenderer renderer, double mouseX, double mouseY)
    {
        base.Render(renderer, mouseX, mouseY);
        int col = isOpen ? 4 : 1;
        for(int y = 0; y < col; y ++)
        {
            for (int x = 0; x < 9; x++)
            {
                renderer.Draw("Textures/Gui/slot", Width / 2 - 90 + x * 20, y * 20, Color.White);
                galaxias.GetItemRenderer().RenderInGui(renderer, galaxias.GetPlayer().Inventory.Hotbar[x], Width / 2 - 90 + x * 20 + 10,  y * 20 + 10, Color.White);
            }
        }
        
    }

    public void Toggle()
    {
        isOpen = !isOpen;
    }
}
