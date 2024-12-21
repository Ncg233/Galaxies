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
    public int InnerX;
    public int InnerY;
    public MultiState(TileState baseState, int x, int y, int innerX, int innerY) : base(baseState.GetTile(), baseState.GetState(), baseState.GetFacing())
    {
        baseTileState = baseState;
        X = x;
        Y = y;  
        InnerX = innerX;
        InnerY = innerY;
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
    public override bool CanStay(AbstractWorld world, TileLayer layer, int x, int y)
    {
        return world.GetTileState(TileLayer.Main, X, Y) == baseTileState;
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
    public override void RandomTick(AbstractWorld abstractWorld, int x, int y, Random rand)
    {
        
    }
}
