using Galaxies.Client.Gui.Screen;
using Galaxies.Client.Render;
using Galaxies.Core.World.Container;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Client.Gui.Widget;
internal class SlotWidget : IWidget
{
    private readonly ItemContainer _container;
    private readonly Slot slot;
    private string textureName;
    private int x;
    private int y;

    public SlotWidget(ItemContainer container, string textureName, Slot slot, int x, int y)
    {
        this.textureName = textureName;
        this.x = x;
        this.y = y;
        this.slot = slot;
    }

    public bool MouseClicked(double mouseX, double mouseY, MouseType type)
    {
        return false;
    }

    public void Render(IntegrationRenderer renderer, double mouseX, double mouseY)
    {
        renderer.Draw(textureName, x, y, Color.White);
        ItemRenderer.RenderInGui(renderer, slot.GetItem(), x + 10, y + 10, Color.White);
        if (IsMouseOver(slot, mouseX, mouseY))
        {
            renderer.Draw("Textures/Gui/slot_onHand", x, y, Color.White);
        }

    }

    private bool IsMouseOver(Slot slot, double mouseX, double mouseY)
    {
        return x <= mouseX && mouseX <= x + 18 && y <= mouseY && mouseY <= y + 18;
    }
}
