using Galaxias.Client.Render;
using Galaxias.Client.Resource;
using Galaxias.Core.World.Tiles;
using Galaxias.Util;
using Microsoft.Xna.Framework;

namespace Galaxias.Core.World.Particles;
public class ColoredParticle : Particle
{
    private string textureLocation;
    private Color color;

    public ColoredParticle(AbstractWorld world, float x, float y, float motionX, float motionY, int maxLife) : base(world, x, y, motionX, motionY, maxLife)
    {

    }
    protected void SetColor(Color color)
    {
        this.color = color;
    }
    public override void Render(IntegrationRenderer renderer, Color light)
    {
        renderer.Draw(TextureManager.BlankTexture, x * GameConstants.TileSize, -y * GameConstants.TileSize, Utils.Multiply(color, light));
    }
}