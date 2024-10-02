using Galaxies.Core.World.Items;

namespace Galaxies.Core.World.Inventory;
public class PlayerInventory
{
    public ItemPile[] Hotbar = new ItemPile[36];
    public int onHand = 0;
    public PlayerInventory()
    {
        Hotbar[0] = new ItemPile(AllItems.Dirt, 99);
        Hotbar[1] = new ItemPile(AllItems.GoldIngot, 99);
        Hotbar[2] = new ItemPile(AllItems.Torch, 99);
    }

}