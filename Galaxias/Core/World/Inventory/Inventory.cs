using Galasias.Core.World.Items;

namespace Galasias.Core.World.Inventory;
public class Inventory{
    public Item[] QuickBar = new Item[9+1];
    public Item[] Bag = new Item[27+1];
    public int onHand = 1;
    public Inventory(){
        for (int i = 1;i<=9;i++){
            QuickBar[i] = AllItems.dirt;
        }
        for (int i = 1;i<=27;i++){
            Bag[i] = AllItems.dirt;
        }
    }
}