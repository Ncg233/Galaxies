
using System.Drawing;

namespace Galaxias.Core.World.Particle;
public class TextureParticle : ParticleType
{
    private string textureLocation;
    public TextureParticle(Color color, string textureLocation) : base(color)
    {
        this.textureLocation = textureLocation;
    }
}