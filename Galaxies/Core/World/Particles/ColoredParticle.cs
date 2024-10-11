using Galaxies.Client.Render;
using Galaxies.Client.Resource;
using Galaxies.Core.World;
using Galaxies.Core.World.Tiles;
using Galaxies.Util;
using Microsoft.Xna.Framework;

namespace Galaxies.Core.World.Particles;
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
    public override void Render(IntegrationRenderer renderer, Color color)
    {
        renderer.Draw(TextureManager.BlankTexture, x * GameConstants.TileSize, -y * GameConstants.TileSize, color);
    }
}