using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaxies.Core.World;
using Galaxies.Core.World.Tiles;
using System.Windows.Forms;

namespace Galaxies.Client.Render;
internal class AnimationStateInfo : IStateInfo
{
    private List<Rectangle> sourceRect;
    public AnimationStateInfo(List<Rectangle> sourceRect)
    {
        this.sourceRect = sourceRect;
    }
    public Rectangle GetRenderRect(byte id)
    {
        long runningTime = DateTime.UtcNow.Ticks / 1000 % (sourceRect.Count * 1000);

        long accum = 0;
        for (int i = 0; i < sourceRect.Count; i++)
        {
            accum += 1000;
            if (accum >= runningTime)
            {
                return sourceRect[i];
            }
        }
        return sourceRect[0];
    }

    public TileRenderInfo UpdateAdjacencies(AbstractWorld world, TileLayer layer, int x, int y)
    {
        return new TileRenderInfo();
    }
}
