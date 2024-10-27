using Galaxies.Core.World.Entities;
using Galaxies.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Core.World.Tiles.State;
public class MultiState : TileState
{
    public TileState baseTileState;
    public int X;
    public int Y;   
    public MultiState(TileState baseState, int x, int y) : base(baseState.GetTile(), baseState.GetState(), baseState.GetFacing())
    {
        baseTileState = baseState;
        X = x;
        Y = y;  
    }
    public override void OnUse(AbstractWorld world, int x, int y)
    {
        if (world.GetTileState(TileLayer.Main, X, Y) == baseTileState)
        {
            baseTileState.OnUse(world, X, Y);
        }
    }
    public override void OnDestroyed(AbstractWorld world, int x, int y)
    {
        if (world.GetTileState(TileLayer.Main, X, Y) == baseTileState)
        {
            world.DestoryTile(X, Y);
        }
    }
    public override void OnTilePlaced(AbstractWorld world, AbstractPlayerEntity player, int x, int y)
    {
        
    }
    public override bool ShouldRender()
    {
        return false;
    }
    public override bool IsMulti()
    {
        return true;
    }
}
