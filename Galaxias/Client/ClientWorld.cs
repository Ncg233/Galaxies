using Galaxias.Core.World;
using Galaxias.Core.World.Particle;
using System.Collections.Generic;

namespace Galaxias.Client;
public class ClientWorld : AbstractWorld
{
    private readonly List<ParticleType> particles = [];
    public ClientWorld() : base(true)
    {
    }
}
