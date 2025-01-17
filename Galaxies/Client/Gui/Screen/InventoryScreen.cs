using Galaxies.Client.Gui.Widget;
using Galaxies.Client.Render;
using Galaxies.Core.World.Container;
using Galaxies.Core.World.Entities;
using Galaxies.Core.World.Items;
using Galaxies.Util;
using Microsoft.Xna.Framework;

namespace Galaxies.Client.Gui.Screen;
public class InventoryScreen : AbstractScreen
{
    private PlayerContainer playerContainer;
    private AbstractPlayerEntity playerEntity;
    public InventoryScreen(AbstractPlayerEntity player)
    {
        playerEntity = player; 
        playerContainer = player.container;
        xSize = 180;
        
    }
    public override void Render(IntegrationRenderer renderer, double mouseX, double mouseY)
    {
        base.Render(renderer, mouseX, mouseY);

        ItemPile draggedPile = playerContainer.draggedPile;
        if (draggedPile != null && !draggedPile.IsEmpty())
        {
            ItemRenderer.RenderInGui(renderer, draggedPile, (float)mouseX, (float)mouseY, Color.White);
        }
    }

    public override bool MouseClicked(double mouseX, double mouseY, MouseType pressedKey)
    {
        if(base.MouseClicked(mouseX, mouseY, pressedKey))
        {
            return true;
        }
        
        return false;//todo

    }

    protected override void OnInit()
    {
        LeftPos = (Width - xSize) / 2;
        foreach (var slot in playerContainer)
        {
            AddWidget(new SlotWidget(playerContainer ,slot, LeftPos + slot.X, slot.Y, "Textures/Gui/slot", "Textures/Gui/slot_onHand"));
            
        }

    }
    public override void Hid()
    {
        galaxias.inGameHud.invOpen = false;
    }
}
