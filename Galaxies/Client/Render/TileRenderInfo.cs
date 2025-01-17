using Galaxies.Util;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Client.Render;
public class TileRenderInfo
{
    public byte TextureId;
    public short RotationD;
    public Facing Facing = Facing.None;
    public SpriteEffects Effects;
    public TileRenderInfo WithRotation(byte textureId, short rotationD)
    {
        TextureId = textureId;
        RotationD = rotationD;
        return this;
    }
    public TileRenderInfo WithFacing(byte textureId, Facing facing)
    {
        TextureId = textureId;
        Facing = facing;
        return this;
    }
}
