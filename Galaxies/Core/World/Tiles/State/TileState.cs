using Galaxies.Core.World.Entities;
using Galaxies.Util;
using System;
using System.Collections.Generic;

namespace Galaxies.Core.World.Tiles.State;
public class TileState
{
    public readonly Tile Tile;
    private string State;
    private Facing Facing;
    public TileState(Tile tile, string state, Facing facing)
    {
        Tile = tile;
        State = state;
        Facing = facing;
    }
    public Tile GetTile()
    {
        return Tile;
    }
    public int GetLight()
    {
        return Tile.GetLight(this);
    }

    public bool IsFullTile()
    {
        return Tile.IsFullTile();
    }
    public bool IsAir()
    {
        return Tile.IsAir();
    }
    public int GetRenderWidth()
    {
        return Tile.GetRenderWidth(this);
    }
    public int GetRenderHeight()
    {
        return Tile.GetRenderHeight(this);
    }
    public virtual List<HitBox> GetHitBoxes()
    {
        return Tile.GetHitBoxes(this);
    }

    public void OnNeighborChanged(AbstractWorld abstractWorld, TileLayer layer, int x, int y, TileState changedTile)
    {
        Tile.OnNeighborChanged(this, abstractWorld, layer, x, y, changedTile);
    }
    public virtual string GetState()
    {
        return State;
    }
    public virtual Facing GetFacing()
    {
        return Facing;
    }
    public TileState ChangeState(string state)
    {
        return Tile.stateHandler.GetState(state, Facing);
    }
    public TileState ChangeFacing(Facing facing)
    {
        return Tile.stateHandler.GetState(State, facing);
    }
    public virtual void OnDestroyed(AbstractWorld world, int x, int y)
    {
        Tile.OnDestoryed(this, world, x, y);
    }

    public virtual void OnUse(AbstractWorld world, AbstractPlayerEntity player, int x, int y)
    {
        Tile.OnUse(this, world, player, x, y);
    }

    public virtual void OnTilePlaced(AbstractWorld world, AbstractPlayerEntity player, int x, int y)
    {
        Tile.OnTilePlaced(this, world, player, x, y);
    }
    public virtual bool CanStay(AbstractWorld world, TileLayer layer, int x, int y)
    {
        return Tile.CanStay(this, world, layer, x, y);
    }
    public bool CanPlaceThere(AbstractWorld world, TileLayer placeLayer, int x, int y)
    {
        return Tile.CanPlaceThere(this, world, placeLayer,  x, y);
    }

    public virtual bool ShouldRender()
    {
        return Tile.ShouleRender(this);
    }
    public virtual bool IsMulti()
    {
        return false;
    }

    internal TileRenderType GetRenderType()
    {
        return Tile.GetRenderType();
    }

    public virtual void RandomTick(AbstractWorld abstractWorld, int x, int y, Random rand)
    {
        Tile.RandomTick(this, abstractWorld, x, y, rand);
    }

    public bool HasTileEntity()
    {
        return Tile is ITileEntityProvider;
    }
}

