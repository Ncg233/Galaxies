using Galaxias.Client.Render;
using Galaxias.Client.Resource;
using Galaxias.Core.World.Tiles;
using Galaxias.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxias.Core.World.Particles;
public class TileParticle : Particle
{
    private Texture2D texture;
    private Rectangle sourceRect;
    private int size = 2;
    private float renderSize = 1.2f;
    public TileParticle(TileState state, AbstractWorld world, float x, float y, float motionX, float motionY, float maxLife) : base(world, x, y, motionX, motionY, maxLife)
    {
        texture = TileRenderer.stateToSprite[state].SourceTexture;
        sourceRect = new(Utils.Random.Next(texture.Width - size), Utils.Random.Next(texture.Height - size), size, size);
    }

    public override void Render(IntegrationRenderer renderer, Color light)
    {
        renderer.Draw(texture, GetRenderX(), GetRenderY() - renderSize, light, renderSize, renderSize, source: sourceRect);
    }
    public override float GetWidth()
    {
        return 0.25f;
    }
    public override float GetHeight()
    {
        return 0.25f;
    }
}

