using Galasias.Core.World.Items;

namespace Galasias.Core.World.Inventory;
public class Inventory{
    public ItemPile[] Hotbar = new ItemPile[9+1];
    public ItemPile[] Bag = new ItemPile[27+1];
    public int onHand = 1;
    public Inventory() {
        for (int i = 1; i <= 4; i++) {
            Hotbar[i] = new ItemPile(AllItems.GoldIngot);
        }
        for (int i = 5; i <= 9; i++)
        {
            Hotbar[i] = new ItemPile(AllItems.Dirt);
        }
    }

}