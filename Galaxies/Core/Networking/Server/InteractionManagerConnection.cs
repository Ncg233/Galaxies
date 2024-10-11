using Galaxies.Core.World;
using Galaxies.Core.World.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.Networking.Server;
public class InteractionManagerConnection : InteractionManager
{
    public InteractionManagerConnection(AbstractWorld world, AbstractPlayerEntity player) : base(world, player)
    {

    }
    public override void Update(float dTime)
    {
        
    }
}
