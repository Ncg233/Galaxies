using Galaxias.Core.World.Items;

namespace Galaxias.Core.World.Inventory;
public class PlayerInventory
{
    public ItemPile[] Hotbar = new ItemPile[9];
    public ItemPile[] Bag = new ItemPile[27];
    public int onHand = 0;
    public PlayerInventory()
    {
        Hotbar[0] = new ItemPile(AllItems.Dirt);
        Hotbar[1] = new ItemPile(AllItems.GoldIngot);
        Hotbar[2] = new ItemPile(AllItems.Torch);
    }

}