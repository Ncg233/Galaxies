using Galaxies.Client.Gui.Screen;
using Galaxies.Client.Render;
using Galaxies.Core.World.Container;
using Galaxies.Core.World.Items;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Client.Gui.Widget;
internal class SlotWidget : IWidget
{
    private readonly ItemContainer container;
    private readonly Slot slot;
    private readonly string textureName;
    private readonly string highLightTexture;
    private int x;
    private int y;

    public SlotWidget(ItemContainer container,Slot slot, int x, int y, string textureName, string highLightTexture)
    {
        this.container = container;
        this.textureName = textureName;
        this.highLightTexture = highLightTexture;
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
