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
    private ItemPile draggedPile = ItemPile.Empty;
    public InventoryScreen(AbstractPlayerEntity player)
    {
        playerEntity = player; 
        playerContainer = player.container;
        xSize = 180;
        
    }
    public override void Render(IntegrationRenderer renderer, double mouseX, double mouseY)
    {
        base.Render(renderer, mouseX, mouseY);

        if(draggedPile != null && !draggedPile.IsEmpty())
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
        if (draggedPile.IsEmpty())
        {
            int x = Utils.Floor((mouseX - LeftPos) / 20);
            int y = (int)(mouseY / 20);
            if (x >= 0 && x < 9 && y >= 0 && y < 4)
            {
                int index = y * 9 + x;
                draggedPile = playerContainer.GetSlot(index).GetItem();
                return true;
            }
        }else
        {

        }
        
        return false;//todo

    }

    protected override void OnInit()
    {
        LeftPos = (Width - xSize) / 2;
        foreach (var slot in playerContainer)
        {
            AddWidget(new SlotWidget(playerContainer, "Textures/Gui/slot" ,slot, LeftPos + slot.X, slot.Y));
            
        }

    }
    public override void Hid()
    {
        galaxias.inGameHud.invOpen = false;
    }
}
