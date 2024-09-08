using Galaxias.Core.World.Entities;
using Galaxias.Util;
using System;

namespace Galaxias.Core.World.Tiles;
public class Tile
{
    public static readonly IntIdentityDictionary<TileState> TileStateId = [];
    private readonly StateHandler stateHandler;
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
    internal float GetTranslucentModifier(AbstractWorld chunk, int x, int y, TileLayer layer, bool isSky)
    {
        if (!IsFullTile() && isSky)
        {
            return 1F;
        }
        else
        {
            return layer == TileLayer.Background ? 0.9F : 0.75F;
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
    public virtual bool OnBreak(AbstractWorld world, int x, int y, TileState state)
    {
        return true;
    }
    public virtual bool OnPlace(AbstractWorld world, int x, int y, TileState state)
    {
        return true;
    }
    public virtual bool OnUse(AbstractWorld world, int x, int y, TileState state, LivingEntity entity)
    {
        return true;
    }

    public virtual int GetLight(TileState tileState)
    {
        return 0;
    }
    public bool IsAir()
    {
        return settings.IsAir;
    }
    public class TileSettings
    {
        public bool IsAir { get; private set; }
        public bool IsFullTile{ get; private set; } = true;
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
