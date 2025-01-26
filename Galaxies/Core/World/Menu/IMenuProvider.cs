using Galaxies.Core.World.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.World.Menu;
public interface IMenuProvider
{
    InventoryMenu CreateMenu(PlayerInventory inventory);
}
