using Galaxias.Core.World;
using Galaxias.Core.World.Entities;

namespace Galasias.Core.World.Items;
public class Item{
    public virtual Item use(AbstractWorld world, Player player, Item item){
        return item;
    }
}