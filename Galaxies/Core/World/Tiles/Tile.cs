using Galaxies.Core.World.Entities;
using Galaxies.Core.World.Items;
using Galaxies.Core.World.Tiles.State;
using Galaxies.Util;
using SharpDX;
using System;
using System.Collections.Generic;

namespace Galaxies.Core.World.Tiles;
public class Tile
{
    public static readonly IntIdentityDictionary<TileState> TileStateId = [];
    public static readonly HitBox defaultHitbox = new HitBox(0, 0, 1, 1);
    public static readonly List<HitBox> defaultHitboxes = [defaultHitbox];
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
    internal float GetTranslucentModifier(AbstractWorld world, int x, int y, TileLayer layer, bool isSky)
    {
        if (!IsFullTile() && isSky)
        {
            return 1F;
        }
        else
        {
            return layer == TileLayer.Background ? 0.8F : 0.65F;
        }
    }
    public virtual TileRenderType GetRenderType()
    {
        return TileRenderType.Center;
    }
    public virtual bool ShouleRender(TileState state)
    {
        return GetRenderType() != TileRenderType.Invisible;
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
    public virtual void RandomTick(TileState state, AbstractWorld world, int x, int y, Random random)
    {

    }
    public virtual void OnNeighborChanged(TileState tileState, AbstractWorld world, TileLayer layer, int x, int y, TileState changedTile)
    {
        if (!world.IsClient && !tileState.CanStay(world, layer, x, y))
        {
            DestoryTile(world, x, y);
        }
    }
    public virtual bool CanStay(TileState state, AbstractWorld world, TileLayer layer, int x, int y)
    {
        return true;
    }
    public virtual bool CanPlaceThere(TileState state, AbstractWorld world, TileLayer placeLayer, int x, int y)
    {
        if (placeLayer == TileLayer.Main)
        {
            foreach (var d in Direction.Adjacent)
            {
                if (world.GetTileState(TileLayer.Main, x + d.X, y + d.Y).IsFullTile()) return true;
            }
            if (world.GetTileState(TileLayer.Background, x, y).IsFullTile()) return true;
        }else
        {
            foreach (var d in Direction.Adjacent)
            {
                if (world.GetTileState(TileLayer.Background, x + d.X, y + d.Y).IsFullTile()) return true;
            }
            if (world.GetTileState(TileLayer.Main, x, y).IsFullTile()) return true;
        }
        
        return false;    
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
    public virtual void OnTilePlaced(TileState tileState, AbstractWorld world, AbstractPlayerEntity player, int x, int y)
    {

    }
    public virtual void OnDestoryed(TileState state, AbstractWorld world, int x, int y)
    {
        //drop item
        var pile = GetDropItem();
        world.AddEntity(new ItemEntity(world, pile, x + Utils.Random.NextFloat(0, 0.5f), y + Utils.Random.NextFloat(0, 0.5f)));
    }
    public virtual void OnUse(TileState tileState, AbstractWorld world, AbstractPlayerEntity player, int x, int y)
    {

    }
    public virtual ItemPile GetDropItem()
    {
        return ItemPile.Empty; 
    }

    public virtual TileState GetPlaceState(AbstractWorld world, AbstractPlayerEntity player, int x, int y)
    {
        return GetDefaultState();
    }

    public virtual List<HitBox> GetHitBoxes(TileState tileState)
    {
        return defaultHitboxes;
    }
    public virtual HitBox GetHitBox(TileState tileState)
    {
        return defaultHitbox;
    }

    public virtual int GetRenderWidth(TileState tileState)
    {
        return 8;
    }
    public virtual int GetRenderHeight(TileState tileState)
    {
        return 8;
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
