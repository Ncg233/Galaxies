using Galaxies.Client.Gui.Widget;
using Galaxies.Client.Render;
using Galaxies.Core.World.Items;
using Galaxies.Core.World.Menu;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Client.Gui.Screen.Menu;
public abstract class MenuScreen<T> : AbstractScreen where T : InventoryMenu
{
    protected readonly T menu;
    public MenuScreen(T menu)
    {
        this.menu = menu; 
    }
    protected override void OnInit()
    {
        galaxias.inGameHud.invOpen = true;
        LeftPos = (Width - xSize) / 2;
        foreach (var slot in menu)
        {
            AddWidget(new SlotWidget(menu, slot, LeftPos + slot.X, slot.Y));

        }

    }
    public override void Render(IntegrationRenderer renderer, double mouseX, double mouseY)
    {
        base.Render(renderer, mouseX, mouseY);

        ItemPile draggedPile = menu.draggedPile;
        if (draggedPile != null && !draggedPile.IsEmpty())
        {
            ItemRenderer.RenderInGui(renderer, draggedPile, (float)mouseX, (float)mouseY, Color.White);
        }
    }
    public override void Hid()
    {
        galaxias.inGameHud.invOpen = false;
    }
}
