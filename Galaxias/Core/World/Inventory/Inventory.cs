using Galasias.Core.World.Items;

namespace Galasias.Core.World.Inventory;
public class Inventory{
    public Item[] Hotbar = new Item[9];
    public Item[] Bag = new Item[27];
    public int onHand = 0;
    public Inventory() {
        for (int i = 0; i < 4; i++) {
            Hotbar[i] = AllItems.GoldIngot;
        }
        for (int i = 4; i < 9; i++)
        {
            Hotbar[i] = AllItems.Dirt;
        }
    }

}