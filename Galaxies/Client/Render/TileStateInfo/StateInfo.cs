using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Galaxies.Core.World;
using Galaxies.Core.World.Tiles;
using Galaxies.Util;
using SharpDX.Direct2D1.Effects;
using Newtonsoft.Json.Linq;

namespace Galaxies.Client.Render.TileStateInfo;
public class StateInfo : IStateInfo
{
    private Rectangle sourceRect;
    public StateInfo(Rectangle sourceRect)
    {
        this.sourceRect = sourceRect;
    }
    public StateInfo()
    {
    }

    public TileRenderInfo DefaultInfo()
    {
        return new TileRenderInfo();
    }

    public Rectangle GetRenderRect(byte id)
    {
        return sourceRect;
    }
    public void Deserialize(JObject prop, Rectangle[,] sourceRects)
    {
        var rect = JsonUtils.GetValue<int[]>(prop, "renderRect");
        sourceRect = sourceRects[rect[0] - 1, rect[1] - 1];

    }

    public TileRenderInfo UpdateAdjacencies(AbstractWorld world, TileLayer layer, int x, int y)
    {
        return new TileRenderInfo();
    }
}
