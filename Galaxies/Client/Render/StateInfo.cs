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

    public TileRenderInfo DefaultInfo()
    {
        return new TileRenderInfo();
    }

    public Rectangle GetRenderRect(byte id)
    {
        return sourceRect;
    }

    public TileRenderInfo UpdateAdjacencies(AbstractWorld world, TileLayer layer, int x, int y)
    {
        return new TileRenderInfo();
    }
}
