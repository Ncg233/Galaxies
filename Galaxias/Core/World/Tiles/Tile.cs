using Galasias.Core.World.Entities;
using System;

namespace Galaxias.Core.World.Tiles;
public class Tile
{
    private readonly StateHandler stateHandler;
    public Tile()
    {
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
            return layer == TileLayer.Background ? 0.9F : 0.8F;
        }
    }
    public virtual TileRenderType GetRenderType()
    {
        return TileRenderType.Bottom;
    }
    public virtual bool IsFullTile()
    {
        return true;
    }
    public virtual bool CanCollide()
    {
        return true;
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
}
