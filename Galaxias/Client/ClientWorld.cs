using Galaxias.Client.Main;
using Galaxias.Client.Render;
using Galaxias.Core.World;
using Galaxias.Core.World.Particle;
using System;
using System.Collections.Generic;

namespace Galaxias.Client;
public class ClientWorld : AbstractWorld
{
    private WorldRenderer renderer;
    //private readonly List<ParticleType> particles = [];
    public ClientWorld(WorldRenderer renderer) : base(true)
    {
        this.renderer = renderer;
    }

    
}
