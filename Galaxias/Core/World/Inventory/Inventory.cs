using Galasias.Core.World.Items;

namespace Galasias.Core.World.Inventory;
public class Inventory{
    public ItemPile[] Hotbar = new ItemPile[9];
    public ItemPile[] Bag = new ItemPile[27];
    public int onHand = 0;
    public Inventory() {
        for (int i = 0; i < 4; i++) {
            Hotbar[i] = new ItemPile(AllItems.GoldIngot);
        }
        for (int i = 4; i < 9; i++)
        {
            Hotbar[i] = new ItemPile(AllItems.Dirt);
        }
    }

}