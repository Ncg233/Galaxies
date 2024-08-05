namespace Galasias.Core.World.Items;
public class ItemPile{
    private Item item;
    private int count = 1;
    public ItemPile(Item item){
        this.item = item;
    }
    public bool isEmpty(){
        if(item == AllItems.Air){
            return true;
        }
        return false;
    }
    public Item GetItem(){
        return this.item;
    }
    public int GetCount(){
        return count;
    }
    public void SetCount(int count){
        this.count = count;
    }
}