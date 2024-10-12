using Galaxies.Core.World.Entities;
using Galaxies.Core.World.Items;
using Galaxies.Util;
using System;
using System.Collections.Generic;

namespace Galaxies.Core.World.Tiles;
public class Tile
{
    public static readonly IntIdentityDictionary<TileState> TileStateId = [];
    public readonly StateHandler stateHandler;
    private readonly TileSettings settings;
    public Tile(TileSettings settings)
    {
        this.settings = settings;
        stateHandler = new StateHandler(this);
    }
    public TileState GetDefaultState()
    {
        return stateHandler.GetDefaultState();
    }
    public virtual void AddProp(StateHandler handler)
    {
    }
    internal float GetTranslucentModifier(AbstractWorld chunk, int x, int y, TileLayer layer, bool isSky)
    {
        if (!IsFullTile() && isSky)
        {
            return 1F;
        }
        else
        {
            return layer == TileLayer.Background ? 0.8F : 0.75F;
        }
    }
    public virtual TileRenderType GetRenderType()
    {
        return TileRenderType.Center;
    }
    public virtual bool IsFullTile()
    {
        return settings.IsFullTile;
    }
    public virtual bool CanCollide()
    {
        return settings.CanCollide;
    }

    public virtual int GetLight(TileState tileState)
    {
        return 0;
    }
    public bool IsAir()
    {
        return settings.IsAir;
    }

    public virtual void OnNeighborChanged(TileState tileState, AbstractWorld world, int x, int y, Tile changedTile)
    {
        if (!world.IsClient && !CanStay(world, x, y))
        {

            DestoryTile(world, x, y);
        }
    }
    public virtual bool CanStay(AbstractWorld world, int x, int y)
    {
        return true;
    }
    public virtual void DestoryTile(AbstractWorld world, int x, int y)
    {
        if (!world.IsClient)
        {
            world.DestoryTile(x, y);
        }
    }
    //public TileState GetState(string prop)
    //{
    //    return stateHandler.GetState(prop);
    //}

    public List<TileState> GetAllState()
    {
        return stateHandler.GetAllState();
    }

    public virtual void OnDestoryed(TileState state, AbstractWorld world, int x, int y)
    {
        //drop item
        var pile = GetDropItem();
        world.AddEntity(new ItemEntity(world, pile, x, y));

    }
    public virtual ItemPile GetDropItem()
    {
        return ItemPile.Empty; 
    }

    public virtual TileState GetPlaceState(AbstractWorld world, AbstractPlayerEntity player, int x, int y)
    {
        return GetDefaultState();
    }

    public class TileSettings
    {
        public bool IsAir { get; private set; }
        public bool IsFullTile { get; private set; } = true;
        public bool CanCollide { get; private set; } = true;
        public TileSettings SetAir()
        {
            IsAir = true;
            return this;
        }
        public TileSettings SetCanCollide(bool canCollide)
        {
            CanCollide = canCollide;
            return this;
        }
        public TileSettings SetFullTile(bool isfulltile)
        {
            IsFullTile = isfulltile;
            return this;
        }
    }
}
