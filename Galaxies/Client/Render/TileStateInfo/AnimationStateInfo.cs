using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaxies.Core.World;
using Galaxies.Core.World.Tiles;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Galaxies.Util;

namespace Galaxies.Client.Render.TileStateInfo;
internal class AnimationStateInfo : IStateInfo
{
    private List<Rectangle> rectList;
    public AnimationStateInfo()
    {
    }

    public TileRenderInfo DefaultInfo()
    {
        return new TileRenderInfo();
    }
    public void Deserialize(JObject prop, Rectangle[,] sourceRect)
    {
        var rect = JsonUtils.GetValue<int[]>(prop, "renderRect");
        rectList = [];
        for (int y = rect[0] - 1; y <= rect[2] - 1; y++)
        {
            for (int x = rect[1] - 1; x <= rect[3] - 1; x++)
            {
                rectList.Add(sourceRect[y, x]);
            }
        }
    }
    public Rectangle GetRenderRect(byte id)
    {
        long runningTime = DateTime.UtcNow.Ticks / 1000 % (rectList.Count * 1000);

        long accum = 0;
        for (int i = 0; i < rectList.Count; i++)
        {
            accum += 1000;
            if (accum >= runningTime)
            {
                return rectList[i];
            }
        }
        return rectList[0];
    }

    public TileRenderInfo UpdateAdjacencies(AbstractWorld world, TileLayer layer, int x, int y)
    {
        return new TileRenderInfo();
    }
}
