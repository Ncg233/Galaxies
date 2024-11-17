using Galaxies.Client.Render;
using Galaxies.Core.World.Container;
using Galaxies.Core.World.Entities;
using Galaxies.Core.World.Inventory;
using Galaxies.Core.World.Items;
using Galaxies.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Client.Gui.Screen;
public class InventoryScreen : AbstractScreen
{
    private PlayerContainer playerContainer;
    private ItemPile pile;
    public InventoryScreen(AbstractPlayerEntity player)
    {
        playerContainer = player.container;
        xSize = 180;
        
    }
    public override void Render(IntegrationRenderer renderer, double mouseX, double mouseY)
    {
        base.Render(renderer, mouseX, mouseY);
        foreach (var slot in playerContainer)
        {
            RenderSlot(renderer, slot);
            if(IsMouseOver(slot, mouseX, mouseY))
            {
                renderer.Draw("Textures/Gui/slot_onHand", LeftPos + slot.X, slot.Y, Color.White);
            }
        }
        if(pile != null && !pile.IsEmpty())
        {
            ItemRenderer.RenderInGui(renderer, pile, (float)mouseX, (float)mouseY, Color.White);
        }
    }
    public bool IsMouseOver(Slot slot, double mouseX, double mouseY)
    {
        return LeftPos + slot.X <= mouseX && mouseX <= LeftPos + slot.X + 18 && slot.Y <= mouseY && mouseY <= slot.Y + 18;
    }

    private void RenderSlot(IntegrationRenderer renderer, Slot slot)
    {
        renderer.Draw("Textures/Gui/slot", LeftPos + slot.X, slot.Y, Color.White);
        ItemRenderer.RenderInGui(renderer, slot.GetItem(), LeftPos + slot.X + 10, slot.Y + 10, Color.White);
    }

    public override bool MouseClicked(double mouseX, double mouseY, MouseType pressedKey)
    {
        if( base.MouseClicked(mouseX, mouseY, pressedKey))
        {
            return true;
        }
        int x = Utils.Floor((mouseX - LeftPos) / 20);
        int y = (int)(mouseY / 20);
        if (x >= 0 && x < 9 && y >= 0 && y < 4)
        {
            int index = y * 9 + x;
            pile = playerContainer.GetSlot(index).GetItem();
            return true;
        }
        return false;//todo

    }

    protected override void OnInit()
    {
        LeftPos = (Width - xSize) / 2;
    }
    public override void Hid()
    {
        galaxias.inGameHud.invOpen = false;
    }
}
