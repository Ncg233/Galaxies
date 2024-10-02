using Galaxies.Client.Render;
using Galaxies.Core.World;
using Galaxies.Core.World.Tiles;
using Galaxies.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.Core.World.Particles;
public class TileParticle : Particle
{
    private Texture2D texture;
    private Rectangle sourceRect;
    private int size = 2;
    private float renderSize = 1.2f;
    public TileParticle(TileState state, AbstractWorld world, float x, float y, float motionX, float motionY, float maxLife) : base(world, x, y, motionX, motionY, maxLife)
    {
        texture = TileSpriteManager.GetSpriteMap(state).SourceTexture;
        height = width = 0.25f;
        sourceRect = new(Utils.Random.Next(texture.Width - size), Utils.Random.Next(texture.Height - size), size, size);

    }

    public override void Render(IntegrationRenderer renderer, Color light)
    {
        var color = light * (life / maxLife);
        renderer.Draw(texture, GetRenderX(), GetRenderY() - height * GameConstants.TileSize, color, width * GameConstants.TileSize, height * GameConstants.TileSize, source: sourceRect);
    }
    public override float GetWidth()
    {
        return width;
    }
    public override float GetHeight()
    {
        return height;
    }
}

