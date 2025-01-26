using Galaxies.Core.Data;
using Galaxies.Core.World.Items;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.World.Inventory;
public interface IInventory
{
    public ItemPile GetItemPile(int index);
    void Set(int slotId, ItemPile pile);
    public void SaveInventory(DataSet set);
    public void LoadInventory(DataSet set);
}

