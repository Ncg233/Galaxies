using Galaxies.Core.World;
using Galaxies.Util;

namespace Galaxies.Core.World.Tiles;
public class TileState
{
    private readonly Tile Tile;
    public string State { get; private set; }
    public Facing Facing { get; private set; }
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

    public void OnNeighborChanged(AbstractWorld abstractWorld, int x, int y, Tile changedTile)
    {
        Tile.OnNeighborChanged(this, abstractWorld, x, y, changedTile);
    }

    public string GetState()
    {
        return State;
    }
    public TileState ChangeState(string state) {
        return Tile.stateHandler.GetState(state, Facing);
    }
    public TileState ChangeFacing(Facing facing)
    {
        return Tile.stateHandler.GetState(State, facing);
    }
    public void OnDestroyed(AbstractWorld world, int x, int y)
    {
        Tile.OnDestoryed(this, world, x, y);
    }
}
