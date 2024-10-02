using Galaxies.Core.World;

namespace Galaxies.Core.World.Tiles;
public class TileState
{
    private readonly Tile tile;
    public string state;
    public TileState(Tile tile, string state)
    {
        this.tile = tile;
        this.state = state;
    }
    public Tile GetTile()
    {
        return tile;
    }
    public int GetLight()
    {
        return tile.GetLight(this);
    }

    public bool IsFullTile()
    {
        return tile.IsFullTile();
    }
    public bool IsAir()
    {
        return tile.IsAir();
    }

    public void OnNeighborChanged(AbstractWorld abstractWorld, int x, int y, Tile changedTile)
    {
        tile.OnNeighborChanged(this, abstractWorld, x, y, changedTile);
    }

    public string GetState()
    {
        return state;
    }

    public void OnDestroyed(AbstractWorld world, int x, int y)
    {
        tile.OnDestoryed(this, world, x, y);
    }
}
