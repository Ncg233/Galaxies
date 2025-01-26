using Galaxies.Core.World.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Client.Gui.Screen.Menu;
public class ChestMenuScreen : MenuScreen<ChestInventoryMenu>
{
    public ChestMenuScreen(ChestInventoryMenu menu) : base(menu)
    {
        xSize = 180;
    }
}
