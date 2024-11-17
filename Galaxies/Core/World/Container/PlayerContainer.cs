using Galaxies.Core.World.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.World.Container;
public class PlayerContainer : ItemContainer
{
    public PlayerContainer(AbstractPlayerEntity player) : base(player)
    {
        AddPlayerInventory(player, 0, 0);
    }

    
}
