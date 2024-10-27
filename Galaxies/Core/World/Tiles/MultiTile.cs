using Galaxies.Core.World.Entities;
using Galaxies.Core.World.Tiles.State;
using Galaxies.Util;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.World.Tiles;
public abstract class MultiTile : Tile
{
    private int width;
    private int height;
    public MultiTile(int width, int height, TileSettings settings) : base(settings)
    {
        this.width = width;
        this.height = height;
    }
    public override void OnDestoryed(TileState state, AbstractWorld world, int x, int y)
    {
        if (!state.IsMulti())
        {
            for (int i = x; i < x + width; i++)
            {
                for (int j = y; j < y + height; j++)
                {
                    if (i != x || j != y)
                    {
                        world.DestoryTile(i, j);
                    }
                }
            }
        }
    }
    public override void OnTilePlaced(TileState tileState, AbstractWorld world, AbstractPlayerEntity player, int x, int y)
    {
        if(!tileState.IsMulti())
        {
            var multiState = new MultiState(tileState, x, y);
            for (int i = x; i < x + width; i++)
            {
                for (int j = y; j < y + height; j++)
                {
                    if (i != x || j != y)
                    {
                        world.SetTileState(TileLayer.Main, i, j, multiState);
                    }
                }
            }
        }
        
    }
}
