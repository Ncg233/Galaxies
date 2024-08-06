using System.Collections.Generic;
using System.Drawing;

namespace Galaxias.Core.World.Particle;
public class AllParticle
{
    public static readonly Dictionary<string, ParticleType> particleRegister = new Dictionary<string, ParticleType>();
    public static readonly ParticleType White = Register("white", new ParticleType(Color.White));
    private static ParticleType Register(string name, ParticleType particle)
    {
        particleRegister.Add(name, particle);
        return particle;
    }
}
