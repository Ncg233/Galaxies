using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Galaxies.Core.World;
using Galaxies.Core.World.Tiles;

namespace Galaxies.Client.Render;
public class StateInfo : IStateInfo
{
    private Rectangle sourceRect;
    public StateInfo(Rectangle sourceRect)
    {
        this.sourceRect = sourceRect;
    }

    public Rectangle GetRenderRect(byte id)
    {
        return sourceRect;
    }

    public byte UpdateAdjacencies(AbstractWorld world, TileLayer layer, int x, int y)
    {
        return 0;
    }
}
