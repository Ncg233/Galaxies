using Galaxias.Core.World;
using Galaxias.Core.World.Entities;

namespace Galasias.Core.World.Items;
public class Item{
    public virtual Item use(AbstractWorld world, Player player, Item item){
        return item;
    }
    public string GetTexture(){
        string location = "Assets/Textures/Items";
        return location;
    }
    public Item GetItem(){
        return this;
    }
}