using Galaxies.Client.Gui.Screen;
using Galaxies.Client.Render;
using Galaxies.Core.World.Items;
using Galaxies.Core.World.Menu;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Client.Gui.Widget;
internal class SlotWidget : IWidget
{
    private readonly InventoryMenu container;
    private readonly Slot slot;
    private readonly string textureName;
    private readonly string highLightTexture;
    private int x;
    private int y;

    public SlotWidget(InventoryMenu container,Slot slot, int x, int y)
    {
        this.container = container;
        textureName = slot.TextureName;
        highLightTexture = slot.HighLightTexture;
        this.x = x;
        this.y = y;
        this.slot = slot;
    }

    public bool MouseClicked(double mouseX, double mouseY, MouseType type)
    {
        if(IsMouseOver(mouseX, mouseY))
        {
            slot.OnClicked(container, type);
            return true;
        }
        return false;
    }

    public void Render(IntegrationRenderer renderer, double mouseX, double mouseY)
    {
        renderer.Draw(textureName, x, y, Color.White);
        ItemRenderer.RenderInGui(renderer, slot.GetItem(), x + 10, y + 10, Color.White);
        if (IsMouseOver(mouseX, mouseY))
        {
            renderer.Draw(highLightTexture, x, y, Color.White);
        }

    }

    private bool IsMouseOver(double mouseX, double mouseY)
    {
        return x <= mouseX && mouseX <= x + 18 && y <= mouseY && mouseY <= y + 18;
    }
}
