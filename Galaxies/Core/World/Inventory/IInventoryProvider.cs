using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.World.Inventory;
public interface IInventoryProvider
{
    IInventory GetInventory();
}
