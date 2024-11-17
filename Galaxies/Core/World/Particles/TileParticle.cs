using Galaxies.Client.Render;
using Galaxies.Core.World;
using Galaxies.Core.World.Tiles;
using Galaxies.Core.World.Tiles.State;
using Galaxies.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX;

namespace Galaxies.Core.World.Particles;
public class TileParticle : Particle
{
    private Texture2D texture;
    private Microsoft.Xna.Framework.Rectangle sourceRect;
    private int size = 2;
    private float renderSize = 1.2f;
    public TileParticle(TileState state, AbstractWorld world, float x, float y, float motionX, float motionY, float maxLife) : base(world, x, y, motionX, motionY, maxLife)
    {
        texture = SpriteManager.GetSpriteMap(state).SourceTexture;
        height = width = Utils.Random.NextFloat(0.15f, 0.2f);
        var rect = SpriteManager.GetStateInfo(state).GetRenderRect(0);
        sourceRect = new(Utils.Random.Next(rect.X, rect.X + rect.Width - size), Utils.Random.Next(rect.Y, rect.Y + rect.Height - size), size, size);

    }

    public override void Render(IntegrationRenderer renderer, Microsoft.Xna.Framework.Color color)
    {
        var renderColor = color * (life / maxLife);
        renderer.Draw(texture, GetRenderX(), GetRenderY() - height * GameConstants.TileSize, renderColor, width * GameConstants.TileSize, height * GameConstants.TileSize, source: sourceRect);
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

